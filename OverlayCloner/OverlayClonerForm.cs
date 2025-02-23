/*
 * Overlay Package Cloner : a tool to quickly create new overlay packages for the Sims 2.
 *
 * Created by Roxane Morin, "Crisps" in the Sims 2 community.
 * https://github.com/RoxaneMorin
 * https://crispsandkerosene.tumblr.com/
 *
 * This code uses libraries by William Howard.
 * https://github.com/whoward69
 * https://www.picknmixmods.com/
 */


#region Usings
using System;
using System.IO;
using System.Text.RegularExpressions;

using Sims2Tools;
using Sims2Tools.DBPF;
using Sims2Tools.DBPF.Data;
using Sims2Tools.DBPF.Package;
using Sims2Tools.DBPF.SceneGraph.BINX;
using Sims2Tools.DBPF.SceneGraph.COLL;
using Sims2Tools.DBPF.SceneGraph.GZPS;
using Sims2Tools.DBPF.SceneGraph.IDR;
using Sims2Tools.DBPF.SceneGraph.TXMT;
using Sims2Tools.DBPF.SceneGraph.TXTR;
using Sims2Tools.DBPF.SceneGraph.XTOL;
using Sims2Tools.DBPF.STR;
#endregion


namespace CrispsOverlayCloner
{
    public partial class OverlayClonerForm : Form
    {
        // VARIABLES
        OverlaySourcePackage sourcePackage;
        OverlayCloneData clonePackageData;
        DBPFFile clonePackage;

        private bool packageNameOk;
        private bool overlayNameOk;
        private bool toReplaceOk = true;
        private bool replaceByOk = true;

        List<string> log;


        // METHODS

        // INIT
        public OverlayClonerForm()
        {
            InitializeComponent();

            // Sadly the tooltip is still being a fuck.
            toolTipGeneric.Active = true;
        }


        // USER EVENTS
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (selectPackageDialog.ShowDialog() == DialogResult.OK)
            {
                ClearPreviousData();

                LoadSourcePackage(selectPackageDialog.FileName);
            }
        }

        private void buttonRedoPackageData_Click(object sender, EventArgs e)
        {
            ClearCloneParameters();
            ClearCloneOutput();

            CreateClonePackageData();
        }

        private void buttonClone_Click(object sender, EventArgs e)
        {
            savePackageDialog.InitialDirectory = sourcePackage.PackageDir;
            savePackageDialog.FileName = clonePackageData.PackageNameNoExtn;

            if (savePackageDialog.ShowDialog() == DialogResult.OK)
            {
                Clone(savePackageDialog.FileName);
            }
        }

        private void textInputPackageName_TextChanged(object sender, EventArgs e)
        {
            if (sourcePackage != null)
            {
                if (string.IsNullOrEmpty(textInputPackageName.Text))
                {
                    packageNameOk = false;
                    TextInputPackageNameEmpty();
                }
                else if (textInputPackageName.Text == sourcePackage.PackageNameNoExtn)
                {
                    packageNameOk = false;
                    TextInputPackageNameInvalid();
                }
                else
                {
                    packageNameOk = true;
                    TextInputPackageNameValid();
                }
            }
        }

        private void textInputOverlayName_TextChanged(object sender, EventArgs e)
        {
            if (sourcePackage != null)
            {
                if (string.IsNullOrEmpty(textInputOverlayName.Text))
                {
                    overlayNameOk = false;
                    TextInputOverlayNameEmpty();
                }
                else if (textInputOverlayName.Text == sourcePackage.InternalOverlayName)
                {
                    overlayNameOk = false;
                    TextInputOverlayNameInvalid();
                }
                else
                {
                    overlayNameOk = true;
                    TextInputOverlayNameValid();
                }
            }
        }

        private void textInputToReplace_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidForSubstitution(textInputToReplace.Text))
            {
                toReplaceOk = false;
                TextInputToReplaceInvalid();
            }
            else
            {
                toReplaceOk = true;
                TextInputToReplaceValidOrNull();
            }
        }

        private void textInputReplaceBy_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidForSubstitution(textInputReplaceBy.Text))
            {
                replaceByOk = false;
                TextInputReplaceByInvalid();
            }
            else
            {
                replaceByOk = true;
                TextInputReplaceByValidOrNull();
            }
        }


        // AUTOMATIC EVENTS
        private void overlaySourcePackageBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (sourcePackage != null)
            {
                if (sourcePackage.IsValidOverlayPackage)
                {
                    PackageStatusIndicatorValid();
                    EnableCloneParameters();

                    CreateClonePackageData();
                }
                else
                {
                    PackageStatusIndicatorInvalid();
                    DisabledCloneParameters();
                }
            }
            else
            {
                PackageStatusIndicatorNone();
                DisabledCloneParameters();
            }
        }

        private void overlayCloneDataBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if ((sourcePackage != null) && packageNameOk && overlayNameOk && toReplaceOk && replaceByOk)
            {
                buttonClone.Enabled = true;
            }
            else
            {
                buttonClone.Enabled = false;
            }
        }


        // DATA HANDLING
        private void LoadSourcePackage(string packageFilepath)
        {
            try
            {
                sourcePackage = OverlaySourcePackage.Create(packageFilepath);

                overlaySourcePackageBindingSource.DataSource = sourcePackage;
                dictDisplayResourceCountByType.DataSource = sourcePackage.ResourceCountPerType;
            }
            catch (Exception e)
            {
                textBoxLog.Text = e.Message;
            }
        }
        
        private void CreateClonePackageData()
        {
            clonePackageData = OverlayCloneData.Create(sourcePackage);
            overlayCloneDataBindingSource.DataSource = clonePackageData;
        }

        private void ClearPreviousData()
        {
            if (sourcePackage != null)
            {
                sourcePackage.Close();
            }
            clonePackageData = null;

            ClearCloneParameters();
            ClearCloneOutput();
        }



        // THE MAIN FEATURE
        private void Clone(string filePath)
        {
            try
            {
                // Create the new package.
                clonePackageData.UpdateCloningData();
                clonePackage = new DBPFFile(filePath);
                clonePackageData.FinalPackageName = clonePackage.PackageName;


                // Data to keep track of.
                List<DBPFEntry> targetEntries;

                Dictionary<DBPFKey, DBPFKey> resourceKeyMapping = new Dictionary<DBPFKey, DBPFKey>();
                Dictionary<string, string> txtrResourceNameMapping = new Dictionary<string, string>();
                string newResourceNamePrefix = string.Format("##{0}!", clonePackageData.GroupID.ToString().ToLower());

                log = new List<string>();


                // Cloning time!

                // BINXs
                log.Add("↦ Processing BINX resource(s).");
                targetEntries = sourcePackage.GetEntriesByType(Binx.TYPE);
                foreach (DBPFEntry entry in targetEntries)
                {
                    Binx thisBinx = (Binx)sourcePackage.GetResourceByEntry(entry);
                    thisBinx.ChangeGroupID(clonePackageData.GroupID);

                    clonePackage.Commit(thisBinx, true);
                    resourceKeyMapping.Add(entry, thisBinx);
                }
                log.Add(string.Format("↦ {0} BINX resource(s) have been cloned.\n", targetEntries.Count));


                // TXTRs
                log.Add("↦ Processing TXTR resource(s).");
                targetEntries = sourcePackage.GetEntriesByType(Txtr.TYPE);
                foreach (DBPFEntry entry in targetEntries)
                {
                    Txtr thisTxtr = (Txtr)sourcePackage.GetResourceByEntry(entry);
                    thisTxtr.ChangeGroupID(clonePackageData.GroupID);

                    string newResourceName = AddOrReplaceResourceNamePrefix(thisTxtr.KeyName, newResourceNamePrefix);
                    if (clonePackageData.DoStringSubstitution)
                    {
                        newResourceName = ReplaceUserSubstring(newResourceName);

                        log.Add(string.Format("• '{0}' → '{1}'.", thisTxtr.ImageData.NameResource.FileName, newResourceName));
                    }
                    txtrResourceNameMapping.Add(StripResourceNameTypeSuffix(thisTxtr.KeyName), StripResourceNameTypeSuffix(newResourceName));

                    thisTxtr.ImageData.NameResource.FileName = newResourceName;
                    thisTxtr.FixTGIR(newResourceName);

                    clonePackage.Commit(thisTxtr, true);
                }
                log.Add(string.Format("↦ {0} TXTR resource(s) have been cloned.\n", targetEntries.Count));


                // TXMTs
                log.Add("↦ Processing TXMT resource(s).");
                targetEntries = sourcePackage.GetEntriesByType(Txmt.TYPE);
                foreach (DBPFEntry entry in targetEntries)
                {
                    Txmt thisTxmt = (Txmt)sourcePackage.GetResourceByEntry(entry);
                    thisTxmt.ChangeGroupID(clonePackageData.GroupID);

                    string newResourceName = AddOrReplaceResourceNamePrefix(thisTxmt.KeyName, newResourceNamePrefix);
                    if (clonePackageData.DoStringSubstitution)
                    {
                        newResourceName = ReplaceUserSubstring(newResourceName);

                        log.Add(string.Format("• '{0}' → '{1}'.", thisTxmt.MaterialDefinition.NameResource.FileName, newResourceName));
                    }
                    string newResourceNameNoSuffix = StripResourceNameTypeSuffix(newResourceName);

                    thisTxmt.MaterialDefinition.NameResource.FileName = newResourceName;
                    thisTxmt.MaterialDefinition.FileDescription = newResourceNameNoSuffix;
                    thisTxmt.FixTGIR(newResourceName);

                    string currentBaseTextureName = thisTxmt.GetProperty("stdMatBaseTextureName");
                    if (!txtrResourceNameMapping.TryGetValue(currentBaseTextureName, out string? correspondingBaseTextureName))
                    {
                        // Account for TXTR names with missing groupIDs.
                        currentBaseTextureName = StripResourceNamePrefix(currentBaseTextureName);
                        txtrResourceNameMapping.TryGetValue(currentBaseTextureName, out correspondingBaseTextureName);
                    }
                    if (correspondingBaseTextureName != null)
                    {
                        thisTxmt.MaterialDefinition.SetProperty("stdMatBaseTextureName", correspondingBaseTextureName);

                        thisTxmt.MaterialDefinition.ClearFiles();
                        thisTxmt.MaterialDefinition.AddFile(correspondingBaseTextureName);
                    }

                    clonePackage.Commit(thisTxmt, true);
                    resourceKeyMapping.Add(entry, thisTxmt);
                }
                log.Add(string.Format("↦{0} TXMT resource(s) have been cloned.\n", targetEntries.Count));


                // GZPSs
                log.Add("↦ Processing GZPS resource(s).");
                targetEntries = sourcePackage.GetEntriesByType(Gzps.TYPE);
                foreach (DBPFEntry entry in targetEntries)
                {
                    Gzps thisGzps = (Gzps)sourcePackage.GetResourceByEntry(entry);
                    thisGzps.ChangeGroupID(clonePackageData.GroupID);

                    string newName = AddOrReplaceResourceNamePrefix(thisGzps.GetItem("name").StringValue, newResourceNamePrefix);
                    if (clonePackageData.DoStringSubstitution)
                    {
                        newName = ReplaceUserSubstring(newName, false);

                        log.Add(string.Format("• '{0}' → '{1}'.", thisGzps.GetItem("name").StringValue, newName));
                    }
                    thisGzps.GetItem("name").StringValue = newName;
                    thisGzps.GetItem("family").StringValue = clonePackageData.FamilyID;

                    clonePackage.Commit(thisGzps, true);
                    resourceKeyMapping.Add(entry, thisGzps);
                }
                log.Add(string.Format("↦ {0} GZPS resource(s) have been cloned.\n", targetEntries.Count));


                // XTOLs
                log.Add("↦ Processing XTOL resource(s).");
                targetEntries = sourcePackage.GetEntriesByType(Xtol.TYPE);
                foreach (DBPFEntry entry in targetEntries)
                {
                    Xtol thisXtol = (Xtol)sourcePackage.GetResourceByEntry(entry);
                    thisXtol.ChangeGroupID(clonePackageData.GroupID);

                    string newName = AddOrReplaceResourceNamePrefix(thisXtol.GetItem("name").StringValue, newResourceNamePrefix);
                    if (clonePackageData.DoStringSubstitution)
                    {
                        newName = ReplaceUserSubstring(newName, false);

                        log.Add(string.Format("• '{0}' → '{1}'.", thisXtol.GetItem("name").StringValue, newName));
                    }
                    thisXtol.GetItem("name").StringValue = newName;
                    thisXtol.GetItem("family").StringValue = clonePackageData.FamilyID;

                    clonePackage.Commit(thisXtol, true);
                    resourceKeyMapping.Add(entry, thisXtol);
                }
                log.Add(string.Format("↦ {0} XTOL resource(s) have been cloned.\n", targetEntries.Count));


                // STR
                log.Add("↦ Processing the STR resource.");
                Str theStr = (Str)sourcePackage.GetResourceByEntry(sourcePackage.StrEntry);
                theStr.ChangeGroupID(clonePackageData.GroupID);

                int strItemCount = theStr.LanguageItems(MetaData.Languages.Default).Count;
                for (int i = 0; i < strItemCount; i++)
                {
                    if (i == 0)
                    {
                        theStr.LanguageItems(MetaData.Languages.Default)[i].Title = clonePackageData.OverlayName;
                    }
                    else // TODO: option of whether to bother with this.
                    {
                        string existingString = theStr.LanguageItems(MetaData.Languages.Default)[i].Title;
                        if (!string.IsNullOrEmpty(existingString))
                        {
                            string newString = AddOrReplaceResourceNamePrefix(existingString, newResourceNamePrefix);
                            if (clonePackageData.DoStringSubstitution)
                            {
                                newString = ReplaceUserSubstring(newString, false);

                                log.Add(string.Format("• '{0}' → '{1}'.", existingString, newString));
                            }
                            theStr.LanguageItems(MetaData.Languages.Default)[i].Title = newString;
                        }
                    }
                }

                clonePackage.Commit(theStr, true);
                resourceKeyMapping.Add(sourcePackage.StrEntry, theStr);
                log.Add(string.Format("↦ The STR resource has been cloned.\n"));


                // COLL
                log.Add("↦ Processing the COLL resource.");
                Coll theColl = (Coll)sourcePackage.GetResourceByEntry(sourcePackage.CollEntry);
                theColl.ChangeIR(clonePackageData.InstanceID, theColl.ResourceID);

                clonePackage.Commit(theColl, true);
                resourceKeyMapping.Add(sourcePackage.CollEntry, theColl);
                log.Add(string.Format("↦ The COLL resource has been cloned.\n"));


                // 3IDR
                log.Add("↦ Processing 3IDR resource(s).");
                targetEntries = sourcePackage.GetEntriesByType(Idr.TYPE);
                foreach (DBPFEntry entry in targetEntries)
                {
                    Idr this3idr = (Idr)sourcePackage.GetResourceByEntry(entry);

                    if (entry == sourcePackage.IdrEntry)
                    {
                        this3idr.ChangeIR(clonePackageData.InstanceID, this3idr.ResourceID);
                    }
                    else
                    {
                        this3idr.ChangeGroupID(clonePackageData.GroupID);
                    }

                    List<DBPFKey> keysContained = this3idr.Items.ToList();
                    int keyCount = this3idr.ItemCount;

                    for (uint i = 0; i < keyCount; i++)
                    {
                        DBPFKey existingKey = this3idr.GetItem(i);
                        if (resourceKeyMapping.ContainsKey(existingKey))
                        {
                            this3idr.SetItem(i, resourceKeyMapping[existingKey]);
                        }
                    }

                    clonePackage.Commit(this3idr, true);
                }
                log.Add(string.Format("↦ {0} 3IDR resource(s) have been cloned.\n", targetEntries.Count));


                // Update and close the clone.
                clonePackage.Update(true);
                clonePackage.Close();

                log.Add(string.Format("The package '{0}' ({1}) has successfully been created!", clonePackageData.PackageNameNoExtn, clonePackageData.FinalPackageName));
                UpdateCloneOutput(log.ToArray());
            }
            catch (Exception e)
            {
                if (clonePackage != null)
                {
                    clonePackage.Close();
                }

                log.Add(string.Format("\nError!\n'{0}'\nThe package will not be cloned.", e.Message));
                textBoxLog.Lines = log.ToArray();
            }
        }



        // UTILITY

        // STRING OPERATIONS
        private bool IsValidForSubstitution(string theString)
        {
            if (string.IsNullOrEmpty(theString))
            {
                return true;
            }

            theString = theString.ToLower();
            return Regex.IsMatch(theString, @"^[0-9A-Za-z\-_]+$");
        }

        private string AddOrReplaceResourceNamePrefix(string keyName, string newPrefix)
        {
            if (Regex.IsMatch(keyName, @"^##0x[0-9a-f]{8}!", RegexOptions.IgnoreCase))
            {
                // TODO: can we avoid doing the Regex testing twice?
                return Regex.Replace(keyName, @"^##0x[0-9a-f]{8}!", newPrefix, RegexOptions.IgnoreCase);
            }
            else
            {
                return keyName.Insert(0, newPrefix);
            }
        }

        private string StripResourceNamePrefix(string keyName)
        {
            return keyName.Substring(keyName.IndexOf('!') + 1);
        }

        private string StripResourceNameTypeSuffix(string keyName)
        {
            int lastIndex = keyName.LastIndexOf('_');
            return keyName.Substring(0, lastIndex);
        }

        private string ReplaceUserSubstring(string theString, bool expectSuffix = true)
        {
            int firstIndex = theString.IndexOf('!') + 1;

            if (expectSuffix)
            {
                int lastIndex = theString.LastIndexOf('_');
                int length = lastIndex - firstIndex;

                string coreString = theString.Substring(firstIndex, length);
                string updatedCoreString = coreString.Replace(clonePackageData.ToReplace, clonePackageData.ReplaceBy, true, System.Globalization.CultureInfo.InvariantCulture);

                return string.Format("{0}{1}{2}", theString.Substring(0, firstIndex), updatedCoreString, theString.Substring(lastIndex));
            }
            else
            {
                string coreString = theString.Substring(firstIndex);
                string updatedCoreString = coreString.Replace(clonePackageData.ToReplace, clonePackageData.ReplaceBy);

                return string.Format("{0}{1}", theString.Substring(0, firstIndex), updatedCoreString);
            }
        }


        // UI UPDATES
        private void PackageStatusIndicatorValid()
        {
            labelSourcePackageStatus.Text = "Valid Overlay Package"; // ✓
            containerSourcePackageStatus.BackColor = Color.LightGreen;
        }
        private void PackageStatusIndicatorInvalid()
        {
            labelSourcePackageStatus.Text = "Invalid Overlay Package"; // ⚠ 
            containerSourcePackageStatus.BackColor = Color.OrangeRed;
        }
        private void PackageStatusIndicatorNone()
        {
            labelSourcePackageStatus.Text = "[no package loaded]";
            containerSourcePackageStatus.BackColor = SystemColors.Control;
        }


        private void TextInputPackageNameValid()
        {
            textInputPackageName.ForeColor = Color.Black;
            textInputPackageName.Size = new Size(441, 20);
            errorProviderGeneric.SetError(textInputPackageName, string.Empty);
        }
        private void TextInputPackageNameInvalid()
        {
            textInputPackageName.ForeColor = Color.Crimson;
            textInputPackageName.Size = new Size(428, 20);
            errorProviderGeneric.SetError(textInputPackageName, "The cloned package's filename should be different from the original.");
        }
        private void TextInputPackageNameEmpty()
        {
            textInputPackageName.ForeColor = Color.Crimson;
            textInputPackageName.Size = new Size(428, 20);
            errorProviderGeneric.SetError(textInputPackageName, "The cloned package's filename cannot be null.");
        }


        private void TextInputOverlayNameValid()
        {
            textInputOverlayName.ForeColor = Color.Black;
            textInputOverlayName.Size = new Size(498, 20);
            errorProviderGeneric.SetError(textInputOverlayName, string.Empty);
        }
        private void TextInputOverlayNameInvalid()
        {
            textInputOverlayName.ForeColor = Color.Crimson;
            textInputOverlayName.Size = new Size(485, 20);
            errorProviderGeneric.SetError(textInputOverlayName, "The cloned package's overlay name should be different from the original.");
        }
        private void TextInputOverlayNameEmpty()
        {
            textInputOverlayName.ForeColor = Color.Crimson;
            textInputOverlayName.Size = new Size(485, 20);
            errorProviderGeneric.SetError(textInputOverlayName, "The cloned package's overlay name cannot be null.");
        }

        private void TextInputToReplaceValidOrNull()
        {
            textInputToReplace.ForeColor = Color.Black;
            textInputToReplace.Size = new Size(238, 20);
            errorProviderGeneric.SetError(textInputToReplace, string.Empty);
        }
        private void TextInputToReplaceInvalid()
        {
            textInputToReplace.ForeColor = Color.Crimson;
            textInputToReplace.Size = new Size(225, 20);
            errorProviderGeneric.SetError(textInputToReplace, "This string can only contain letters, numbers, underscores ('_') and dashes ('-').");
        }

        private void TextInputReplaceByValidOrNull()
        {
            textInputReplaceBy.ForeColor = Color.Black;
            textInputReplaceBy.Size = new Size(238, 20);
            errorProviderGeneric.SetError(textInputReplaceBy, string.Empty);
        }
        private void TextInputReplaceByInvalid()
        {
            textInputReplaceBy.ForeColor = Color.Crimson;
            textInputReplaceBy.Size = new Size(225, 20);
            errorProviderGeneric.SetError(textInputReplaceBy, "This string can only contain letters, numbers, underscores ('_') and dashes ('-').");
        }


        private void EnableCloneParameters(bool doButton = false)
        {
            buttonRedoPackageData.Enabled = true;

            textInputPackageName.Enabled = true;
            textInputOverlayName.Enabled = true;
            textInputToReplace.Enabled = true;
            textInputReplaceBy.Enabled = true;

            if (doButton)
            {
                buttonClone.Enabled = true;
            }
        }
        private void DisabledCloneParameters(bool doButton = true)
        {
            buttonRedoPackageData.Enabled = false;

            textInputPackageName.Enabled = false;
            textInputOverlayName.Enabled = false;
            textInputToReplace.Enabled = false;
            textInputReplaceBy.Enabled = false;

            ClearCloneParameters();

            if (doButton)
            {
                buttonClone.Enabled = false;
            }
        }
        private void ClearCloneParameters()
        {
            textInputPackageName.Text = null;
            textInputOverlayName.Text = null;
            textInputToReplace.Text = null;
            textInputReplaceBy.Text = null;

            TextInputPackageNameValid();
            TextInputOverlayNameValid();

            toReplaceOk = true;
            replaceByOk = true;
        }


        private void UpdateCloneOutput(string[] log)
        {
            textInputGroupID.Enabled = true;
            textInputFamilyID.Enabled = true;

            textInputGroupID.Text = clonePackageData.GroupID.Hex8String();
            textInputFamilyID.Text = clonePackageData.FamilyID;

            textBoxLog.Lines = log;
        }

        private void ClearCloneOutput()
        {
            textInputGroupID.Enabled = false;
            textInputFamilyID.Enabled = false;

            textInputGroupID.Text = null;
            textInputFamilyID.Text = null; ;

            textBoxLog.Text = null;
        }

    }
}
