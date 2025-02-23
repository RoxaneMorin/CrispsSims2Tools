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


using System;
using System.Security.Cryptography;
using System.Text;

using Sims2Tools.DBPF;
using Sims2Tools.DBPF.Utils;


namespace CrispsOverlayCloner
{
    public class OverlayCloneData
    {
        // VARIABLES
        private string packageNameNoExtn;
        private string overlayName;
        private string toReplace;
        private string replaceBy;
        private bool doStringSubstitution;

        private TypeGroupID groupID;
        private TypeInstanceID instanceID;
        private string familyID = "00000000-0000-0000-0000-000000000000";

        private string finalPackageName;


        // PROPERTIES
        public string PackageNameNoExtn 
        { 
            get { return packageNameNoExtn; }  
            set { packageNameNoExtn = value; } 
        }
        public string OverlayName 
        { 
            get { return overlayName; } 
            set { overlayName = value; } 
        }
        public string ToReplace
        {
            get { return toReplace; }
            set { toReplace = value; }
        }
        public string ReplaceBy
        {
            get { return replaceBy; }
            set { replaceBy = value; }
        }

        public TypeGroupID GroupID => groupID;
        public TypeInstanceID InstanceID => instanceID;
        public string FamilyID => familyID;

        public bool DoStringSubstitution => doStringSubstitution;

        public string FinalPackageName
        {
            get { return finalPackageName; }
            set { finalPackageName = value; }
        }



        // CONSTRUCTOR & SETUP
        public static OverlayCloneData Create(OverlaySourcePackage sourcePackageData)
        {
            // Potentially add guardian measures here.
            return new OverlayCloneData(sourcePackageData);
        }

        private OverlayCloneData(OverlaySourcePackage sourcePackageData)
        {
            this.packageNameNoExtn = sourcePackageData.PackageNameNoExtn;
            this.overlayName = sourcePackageData.InternalOverlayName;
        }

        public void UpdateCloningData()
        {
            groupID = Hashes.GroupIDHash(packageNameNoExtn);
            instanceID = (TypeInstanceID)groupID.AsUInt();
            familyID = GUIDHash(packageNameNoExtn);

            doStringSubstitution = !string.IsNullOrEmpty(toReplace);
        }


        // UTILITY

        // Source: https://medium.com/vestigen-ltd/hashing-a-string-into-a-guid-5a12c984112
        private string GUIDHash(string name)
        {
            var bytes = Encoding.UTF8.GetBytes(name);
            var hash = MD5.Create().ComputeHash(bytes);
            var guid = new Guid(hash);

            return guid.ToString();
        }
    }
}
