using Sims2Tools.DBPF;

namespace CrispsOverlayCloner
{
    partial class OverlayClonerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        // COSMETIC EVENTS
        private void containerSourcePackageStatus_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, containerSourcePackageStatus.ClientRectangle, SystemColors.ControlLight, ButtonBorderStyle.Solid);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverlayClonerForm));
            selectPackageDialog = new OpenFileDialog();
            labelSourcePackage = new Label();
            overlaySourcePackageBindingSource = new BindingSource(components);
            containerCloneData = new GroupBox();
            buttonRedoPackageData = new Button();
            containerCloneButton = new Panel();
            buttonClone = new Button();
            containerToReplace = new GroupBox();
            labelReplaceBy = new Label();
            textInputReplaceBy = new TextBox();
            overlayCloneDataBindingSource = new BindingSource(components);
            textInputToReplace = new TextBox();
            containerOverlayName = new GroupBox();
            textInputOverlayName = new TextBox();
            containerPackageName = new GroupBox();
            textInputPackageName = new TextBox();
            labelDotPackage = new Label();
            containerIDs = new SplitContainer();
            containerGroupID = new GroupBox();
            textInputGroupID = new TextBox();
            containerFamilyID = new GroupBox();
            textInputFamilyID = new TextBox();
            containerTotalResourceCount = new GroupBox();
            labelTotalResourceCount = new Label();
            containerSourcePackageInfo = new GroupBox();
            dictDisplayResourceCountByType = new SortedDictDisplayStringInt();
            containerSourcePackageStatus = new Panel();
            labelSourcePackageStatus = new Label();
            containerSourceOverlayName = new GroupBox();
            labelSourceOverlayName = new Label();
            toolTipGeneric = new ToolTip(components);
            errorProviderGeneric = new ErrorProvider(components);
            containerCloningInformation = new GroupBox();
            containerLog = new GroupBox();
            textBoxLog = new RichTextBox();
            savePackageDialog = new SaveFileDialog();
            containerHeader = new Panel();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)overlaySourcePackageBindingSource).BeginInit();
            containerCloneData.SuspendLayout();
            containerCloneButton.SuspendLayout();
            containerToReplace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)overlayCloneDataBindingSource).BeginInit();
            containerOverlayName.SuspendLayout();
            containerPackageName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)containerIDs).BeginInit();
            containerIDs.Panel1.SuspendLayout();
            containerIDs.Panel2.SuspendLayout();
            containerIDs.SuspendLayout();
            containerGroupID.SuspendLayout();
            containerFamilyID.SuspendLayout();
            containerTotalResourceCount.SuspendLayout();
            containerSourcePackageInfo.SuspendLayout();
            containerSourcePackageStatus.SuspendLayout();
            containerSourceOverlayName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProviderGeneric).BeginInit();
            containerCloningInformation.SuspendLayout();
            containerLog.SuspendLayout();
            containerHeader.SuspendLayout();
            SuspendLayout();
            // 
            // selectPackageDialog
            // 
            selectPackageDialog.FileName = "selectPackageDialog";
            selectPackageDialog.Filter = "Sims 2 Package Files (*.package)|*.package";
            // 
            // labelSourcePackage
            // 
            labelSourcePackage.BackColor = Color.Transparent;
            labelSourcePackage.DataBindings.Add(new Binding("Text", overlaySourcePackageBindingSource, "PackageName", true));
            labelSourcePackage.Dock = DockStyle.Right;
            labelSourcePackage.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelSourcePackage.ImageAlign = ContentAlignment.MiddleRight;
            labelSourcePackage.Location = new Point(161, 5);
            labelSourcePackage.Margin = new Padding(0);
            labelSourcePackage.Name = "labelSourcePackage";
            labelSourcePackage.Size = new Size(608, 25);
            labelSourcePackage.TabIndex = 1;
            labelSourcePackage.Text = "No Source Package Loaded";
            labelSourcePackage.TextAlign = ContentAlignment.MiddleRight;
            // 
            // overlaySourcePackageBindingSource
            // 
            overlaySourcePackageBindingSource.DataSource = typeof(OverlaySourcePackage);
            overlaySourcePackageBindingSource.CurrentChanged += overlaySourcePackageBindingSource_CurrentChanged;
            // 
            // containerCloneData
            // 
            containerCloneData.Controls.Add(buttonRedoPackageData);
            containerCloneData.Controls.Add(containerCloneButton);
            containerCloneData.Controls.Add(containerToReplace);
            containerCloneData.Controls.Add(containerOverlayName);
            containerCloneData.Controls.Add(containerPackageName);
            containerCloneData.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerCloneData.Location = new Point(5, 43);
            containerCloneData.Margin = new Padding(0);
            containerCloneData.Name = "containerCloneData";
            containerCloneData.Padding = new Padding(4, 5, 4, 5);
            containerCloneData.Size = new Size(518, 199);
            containerCloneData.TabIndex = 3;
            containerCloneData.TabStop = false;
            containerCloneData.Text = "Clone Parameters";
            // 
            // buttonRedoPackageData
            // 
            buttonRedoPackageData.Enabled = false;
            buttonRedoPackageData.Location = new Point(493, 0);
            buttonRedoPackageData.Name = "buttonRedoPackageData";
            buttonRedoPackageData.Size = new Size(22, 22);
            buttonRedoPackageData.TabIndex = 6;
            buttonRedoPackageData.Text = "⟳";
            toolTipGeneric.SetToolTip(buttonRedoPackageData, "Clear all cloning data.");
            buttonRedoPackageData.UseVisualStyleBackColor = true;
            buttonRedoPackageData.Click += buttonRedoPackageData_Click;
            // 
            // containerCloneButton
            // 
            containerCloneButton.AutoSize = true;
            containerCloneButton.Controls.Add(buttonClone);
            containerCloneButton.Dock = DockStyle.Top;
            containerCloneButton.Location = new Point(4, 151);
            containerCloneButton.Margin = new Padding(0);
            containerCloneButton.MinimumSize = new Size(0, 44);
            containerCloneButton.Name = "containerCloneButton";
            containerCloneButton.Padding = new Padding(5);
            containerCloneButton.Size = new Size(510, 44);
            containerCloneButton.TabIndex = 6;
            // 
            // buttonClone
            // 
            buttonClone.Dock = DockStyle.Fill;
            buttonClone.Enabled = false;
            buttonClone.Location = new Point(5, 5);
            buttonClone.Margin = new Padding(0);
            buttonClone.Name = "buttonClone";
            buttonClone.Size = new Size(500, 34);
            buttonClone.TabIndex = 0;
            buttonClone.Text = "Clone and Save";
            buttonClone.UseVisualStyleBackColor = true;
            buttonClone.Click += buttonClone_Click;
            // 
            // containerToReplace
            // 
            containerToReplace.AutoSize = true;
            containerToReplace.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            containerToReplace.Controls.Add(labelReplaceBy);
            containerToReplace.Controls.Add(textInputReplaceBy);
            containerToReplace.Controls.Add(textInputToReplace);
            containerToReplace.Dock = DockStyle.Top;
            containerToReplace.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerToReplace.Location = new Point(4, 107);
            containerToReplace.Margin = new Padding(0);
            containerToReplace.MaximumSize = new Size(0, 44);
            containerToReplace.MinimumSize = new Size(140, 44);
            containerToReplace.Name = "containerToReplace";
            containerToReplace.Padding = new Padding(6, 4, 6, 4);
            containerToReplace.Size = new Size(510, 44);
            containerToReplace.TabIndex = 5;
            containerToReplace.TabStop = false;
            containerToReplace.Text = "Internal String Substitution";
            // 
            // labelReplaceBy
            // 
            labelReplaceBy.Anchor = AnchorStyles.Top;
            labelReplaceBy.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelReplaceBy.Location = new Point(245, 16);
            labelReplaceBy.Margin = new Padding(0);
            labelReplaceBy.Name = "labelReplaceBy";
            labelReplaceBy.Padding = new Padding(0, 2, 0, 0);
            labelReplaceBy.RightToLeft = RightToLeft.No;
            labelReplaceBy.Size = new Size(20, 20);
            labelReplaceBy.TabIndex = 7;
            labelReplaceBy.Text = "→";
            labelReplaceBy.TextAlign = ContentAlignment.TopCenter;
            // 
            // textInputReplaceBy
            // 
            textInputReplaceBy.DataBindings.Add(new Binding("Text", overlayCloneDataBindingSource, "ReplaceBy", true));
            textInputReplaceBy.Enabled = false;
            textInputReplaceBy.Location = new Point(266, 17);
            textInputReplaceBy.Margin = new Padding(0);
            textInputReplaceBy.Name = "textInputReplaceBy";
            textInputReplaceBy.Size = new Size(238, 20);
            textInputReplaceBy.TabIndex = 0;
            textInputReplaceBy.TabStop = false;
            toolTipGeneric.SetToolTip(textInputReplaceBy, resources.GetString("textInputReplaceBy.ToolTip"));
            textInputReplaceBy.TextChanged += textInputReplaceBy_TextChanged;
            // 
            // overlayCloneDataBindingSource
            // 
            overlayCloneDataBindingSource.DataSource = typeof(OverlayCloneData);
            overlayCloneDataBindingSource.CurrentItemChanged += overlayCloneDataBindingSource_CurrentItemChanged;
            // 
            // textInputToReplace
            // 
            textInputToReplace.DataBindings.Add(new Binding("Text", overlayCloneDataBindingSource, "ToReplace", true));
            textInputToReplace.Dock = DockStyle.Left;
            textInputToReplace.Enabled = false;
            textInputToReplace.Location = new Point(6, 17);
            textInputToReplace.Margin = new Padding(0);
            textInputToReplace.Name = "textInputToReplace";
            textInputToReplace.Size = new Size(238, 20);
            textInputToReplace.TabIndex = 0;
            toolTipGeneric.SetToolTip(textInputToReplace, resources.GetString("textInputToReplace.ToolTip"));
            textInputToReplace.TextChanged += textInputToReplace_TextChanged;
            // 
            // containerOverlayName
            // 
            containerOverlayName.AutoSize = true;
            containerOverlayName.Controls.Add(textInputOverlayName);
            containerOverlayName.Dock = DockStyle.Top;
            containerOverlayName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerOverlayName.Location = new Point(4, 63);
            containerOverlayName.Margin = new Padding(0);
            containerOverlayName.MinimumSize = new Size(0, 44);
            containerOverlayName.Name = "containerOverlayName";
            containerOverlayName.Padding = new Padding(6, 4, 6, 4);
            containerOverlayName.Size = new Size(510, 44);
            containerOverlayName.TabIndex = 2;
            containerOverlayName.TabStop = false;
            containerOverlayName.Text = "Overlay Name";
            // 
            // textInputOverlayName
            // 
            textInputOverlayName.DataBindings.Add(new Binding("Text", overlayCloneDataBindingSource, "OverlayName", true));
            textInputOverlayName.Dock = DockStyle.Left;
            textInputOverlayName.Enabled = false;
            textInputOverlayName.Location = new Point(6, 17);
            textInputOverlayName.Margin = new Padding(0);
            textInputOverlayName.Name = "textInputOverlayName";
            textInputOverlayName.Size = new Size(498, 20);
            textInputOverlayName.TabIndex = 0;
            toolTipGeneric.SetToolTip(textInputOverlayName, "The cloned package's internal overlay name, stored in its Str resource.");
            textInputOverlayName.TextChanged += textInputOverlayName_TextChanged;
            // 
            // containerPackageName
            // 
            containerPackageName.AutoSize = true;
            containerPackageName.Controls.Add(textInputPackageName);
            containerPackageName.Controls.Add(labelDotPackage);
            containerPackageName.Dock = DockStyle.Top;
            containerPackageName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerPackageName.Location = new Point(4, 19);
            containerPackageName.Margin = new Padding(0);
            containerPackageName.MinimumSize = new Size(0, 44);
            containerPackageName.Name = "containerPackageName";
            containerPackageName.Padding = new Padding(6, 4, 6, 4);
            containerPackageName.Size = new Size(510, 44);
            containerPackageName.TabIndex = 1;
            containerPackageName.TabStop = false;
            containerPackageName.Text = "Package Name";
            // 
            // textInputPackageName
            // 
            textInputPackageName.DataBindings.Add(new Binding("Text", overlayCloneDataBindingSource, "PackageNameNoExtn", true));
            textInputPackageName.Dock = DockStyle.Left;
            textInputPackageName.Enabled = false;
            textInputPackageName.Location = new Point(6, 17);
            textInputPackageName.Margin = new Padding(0);
            textInputPackageName.Name = "textInputPackageName";
            textInputPackageName.Size = new Size(441, 20);
            textInputPackageName.TabIndex = 0;
            toolTipGeneric.SetToolTip(textInputPackageName, "The clone package's filename.");
            textInputPackageName.TextChanged += textInputPackageName_TextChanged;
            // 
            // labelDotPackage
            // 
            labelDotPackage.AutoSize = true;
            labelDotPackage.Dock = DockStyle.Right;
            labelDotPackage.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelDotPackage.Location = new Point(447, 17);
            labelDotPackage.Margin = new Padding(0);
            labelDotPackage.Name = "labelDotPackage";
            labelDotPackage.Padding = new Padding(0, 2, 0, 0);
            labelDotPackage.RightToLeft = RightToLeft.No;
            labelDotPackage.Size = new Size(57, 17);
            labelDotPackage.TabIndex = 6;
            labelDotPackage.Text = ".package";
            labelDotPackage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // containerIDs
            // 
            containerIDs.Dock = DockStyle.Top;
            containerIDs.IsSplitterFixed = true;
            containerIDs.Location = new Point(3, 19);
            containerIDs.Name = "containerIDs";
            // 
            // containerIDs.Panel1
            // 
            containerIDs.Panel1.Controls.Add(containerGroupID);
            // 
            // containerIDs.Panel2
            // 
            containerIDs.Panel2.Controls.Add(containerFamilyID);
            containerIDs.Size = new Size(512, 44);
            containerIDs.SplitterDistance = 177;
            containerIDs.TabIndex = 5;
            // 
            // containerGroupID
            // 
            containerGroupID.AutoSize = true;
            containerGroupID.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            containerGroupID.Controls.Add(textInputGroupID);
            containerGroupID.Dock = DockStyle.Fill;
            containerGroupID.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerGroupID.Location = new Point(0, 0);
            containerGroupID.Margin = new Padding(0);
            containerGroupID.MinimumSize = new Size(140, 44);
            containerGroupID.Name = "containerGroupID";
            containerGroupID.Padding = new Padding(6, 4, 6, 4);
            containerGroupID.Size = new Size(177, 44);
            containerGroupID.TabIndex = 5;
            containerGroupID.TabStop = false;
            containerGroupID.Text = "Group ID";
            // 
            // textInputGroupID
            // 
            textInputGroupID.BackColor = SystemColors.ButtonFace;
            textInputGroupID.Dock = DockStyle.Fill;
            textInputGroupID.Enabled = false;
            textInputGroupID.Location = new Point(6, 17);
            textInputGroupID.Margin = new Padding(0);
            textInputGroupID.Name = "textInputGroupID";
            textInputGroupID.ReadOnly = true;
            textInputGroupID.Size = new Size(165, 20);
            textInputGroupID.TabIndex = 0;
            toolTipGeneric.SetToolTip(textInputGroupID, "The cloned package's new resource group ID.");
            // 
            // containerFamilyID
            // 
            containerFamilyID.AutoSize = true;
            containerFamilyID.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            containerFamilyID.Controls.Add(textInputFamilyID);
            containerFamilyID.Dock = DockStyle.Fill;
            containerFamilyID.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerFamilyID.Location = new Point(0, 0);
            containerFamilyID.Margin = new Padding(0);
            containerFamilyID.MinimumSize = new Size(0, 44);
            containerFamilyID.Name = "containerFamilyID";
            containerFamilyID.Padding = new Padding(6, 4, 6, 4);
            containerFamilyID.Size = new Size(331, 44);
            containerFamilyID.TabIndex = 6;
            containerFamilyID.TabStop = false;
            containerFamilyID.Text = "Family";
            // 
            // textInputFamilyID
            // 
            textInputFamilyID.BackColor = SystemColors.ButtonFace;
            textInputFamilyID.Dock = DockStyle.Fill;
            textInputFamilyID.Enabled = false;
            textInputFamilyID.Location = new Point(6, 17);
            textInputFamilyID.Margin = new Padding(0);
            textInputFamilyID.Name = "textInputFamilyID";
            textInputFamilyID.ReadOnly = true;
            textInputFamilyID.Size = new Size(319, 20);
            textInputFamilyID.TabIndex = 0;
            toolTipGeneric.SetToolTip(textInputFamilyID, "The cloned package's new family ID, used in XTOL and GZPS resources.");
            // 
            // containerTotalResourceCount
            // 
            containerTotalResourceCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            containerTotalResourceCount.Controls.Add(labelTotalResourceCount);
            containerTotalResourceCount.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerTotalResourceCount.Location = new Point(195, 67);
            containerTotalResourceCount.Margin = new Padding(4, 3, 4, 3);
            containerTotalResourceCount.Name = "containerTotalResourceCount";
            containerTotalResourceCount.Padding = new Padding(4, 3, 4, 3);
            containerTotalResourceCount.RightToLeft = RightToLeft.No;
            containerTotalResourceCount.Size = new Size(46, 36);
            containerTotalResourceCount.TabIndex = 2;
            containerTotalResourceCount.TabStop = false;
            containerTotalResourceCount.Text = "Total";
            // 
            // labelTotalResourceCount
            // 
            labelTotalResourceCount.BackColor = Color.Transparent;
            labelTotalResourceCount.DataBindings.Add(new Binding("Text", overlaySourcePackageBindingSource, "TotalResourceCount", true));
            labelTotalResourceCount.Dock = DockStyle.Fill;
            labelTotalResourceCount.Location = new Point(4, 16);
            labelTotalResourceCount.Margin = new Padding(0);
            labelTotalResourceCount.Name = "labelTotalResourceCount";
            labelTotalResourceCount.Padding = new Padding(0, 0, 0, 2);
            labelTotalResourceCount.Size = new Size(38, 17);
            labelTotalResourceCount.TabIndex = 1;
            labelTotalResourceCount.Text = "0";
            labelTotalResourceCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // containerSourcePackageInfo
            // 
            containerSourcePackageInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            containerSourcePackageInfo.Controls.Add(containerTotalResourceCount);
            containerSourcePackageInfo.Controls.Add(dictDisplayResourceCountByType);
            containerSourcePackageInfo.Controls.Add(containerSourcePackageStatus);
            containerSourcePackageInfo.Controls.Add(containerSourceOverlayName);
            containerSourcePackageInfo.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerSourcePackageInfo.Location = new Point(529, 43);
            containerSourcePackageInfo.Margin = new Padding(0);
            containerSourcePackageInfo.Name = "containerSourcePackageInfo";
            containerSourcePackageInfo.Padding = new Padding(4, 5, 4, 5);
            containerSourcePackageInfo.RightToLeft = RightToLeft.Yes;
            containerSourcePackageInfo.Size = new Size(250, 473);
            containerSourcePackageInfo.TabIndex = 4;
            containerSourcePackageInfo.TabStop = false;
            containerSourcePackageInfo.Text = "Source Package Info";
            // 
            // dictDisplayResourceCountByType
            // 
            dictDisplayResourceCountByType.AutoSize = true;
            dictDisplayResourceCountByType.DataSource = null;
            dictDisplayResourceCountByType.Dock = DockStyle.Top;
            dictDisplayResourceCountByType.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dictDisplayResourceCountByType.Location = new Point(4, 57);
            dictDisplayResourceCountByType.Margin = new Padding(0);
            dictDisplayResourceCountByType.MinimumSize = new Size(0, 52);
            dictDisplayResourceCountByType.Name = "dictDisplayResourceCountByType";
            dictDisplayResourceCountByType.RightToLeft = RightToLeft.No;
            dictDisplayResourceCountByType.Size = new Size(242, 52);
            dictDisplayResourceCountByType.TabIndex = 9;
            dictDisplayResourceCountByType.Text = "Resource Count by Type";
            // 
            // containerSourcePackageStatus
            // 
            containerSourcePackageStatus.Controls.Add(labelSourcePackageStatus);
            containerSourcePackageStatus.Dock = DockStyle.Bottom;
            containerSourcePackageStatus.Location = new Point(4, 438);
            containerSourcePackageStatus.Margin = new Padding(0);
            containerSourcePackageStatus.Name = "containerSourcePackageStatus";
            containerSourcePackageStatus.Size = new Size(242, 30);
            containerSourcePackageStatus.TabIndex = 8;
            containerSourcePackageStatus.Paint += containerSourcePackageStatus_Paint;
            // 
            // labelSourcePackageStatus
            // 
            labelSourcePackageStatus.BackColor = Color.Transparent;
            labelSourcePackageStatus.Dock = DockStyle.Fill;
            labelSourcePackageStatus.Location = new Point(0, 0);
            labelSourcePackageStatus.Margin = new Padding(0);
            labelSourcePackageStatus.Name = "labelSourcePackageStatus";
            labelSourcePackageStatus.RightToLeft = RightToLeft.No;
            labelSourcePackageStatus.Size = new Size(242, 30);
            labelSourcePackageStatus.TabIndex = 6;
            labelSourcePackageStatus.Text = "[no package loaded]";
            labelSourcePackageStatus.TextAlign = ContentAlignment.MiddleCenter;
            toolTipGeneric.SetToolTip(labelSourcePackageStatus, "A valid overlay package contains COLL and 3IDR resources with the groupd ID 0x4F184AA9.");
            // 
            // containerSourceOverlayName
            // 
            containerSourceOverlayName.AutoSize = true;
            containerSourceOverlayName.Controls.Add(labelSourceOverlayName);
            containerSourceOverlayName.Dock = DockStyle.Top;
            containerSourceOverlayName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            containerSourceOverlayName.Location = new Point(4, 19);
            containerSourceOverlayName.Margin = new Padding(0);
            containerSourceOverlayName.MinimumSize = new Size(0, 38);
            containerSourceOverlayName.Name = "containerSourceOverlayName";
            containerSourceOverlayName.Padding = new Padding(4, 3, 4, 3);
            containerSourceOverlayName.RightToLeft = RightToLeft.No;
            containerSourceOverlayName.Size = new Size(242, 38);
            containerSourceOverlayName.TabIndex = 4;
            containerSourceOverlayName.TabStop = false;
            containerSourceOverlayName.Text = "Overlay Name";
            // 
            // labelSourceOverlayName
            // 
            labelSourceOverlayName.DataBindings.Add(new Binding("Text", overlaySourcePackageBindingSource, "InternalOverlayName", true));
            labelSourceOverlayName.Dock = DockStyle.Top;
            labelSourceOverlayName.Location = new Point(4, 16);
            labelSourceOverlayName.Margin = new Padding(0);
            labelSourceOverlayName.Name = "labelSourceOverlayName";
            labelSourceOverlayName.RightToLeft = RightToLeft.No;
            labelSourceOverlayName.Size = new Size(234, 19);
            labelSourceOverlayName.TabIndex = 0;
            labelSourceOverlayName.Text = "[none]";
            labelSourceOverlayName.TextAlign = ContentAlignment.TopRight;
            // 
            // toolTipGeneric
            // 
            toolTipGeneric.AutoPopDelay = 5000;
            toolTipGeneric.BackColor = SystemColors.ControlLightLight;
            toolTipGeneric.InitialDelay = 500;
            toolTipGeneric.IsBalloon = true;
            toolTipGeneric.ReshowDelay = 100;
            toolTipGeneric.ShowAlways = true;
            // 
            // errorProviderGeneric
            // 
            errorProviderGeneric.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProviderGeneric.ContainerControl = this;
            // 
            // containerCloningInformation
            // 
            containerCloningInformation.Controls.Add(containerLog);
            containerCloningInformation.Controls.Add(containerIDs);
            containerCloningInformation.Location = new Point(5, 245);
            containerCloningInformation.Name = "containerCloningInformation";
            containerCloningInformation.Size = new Size(518, 271);
            containerCloningInformation.TabIndex = 5;
            containerCloningInformation.TabStop = false;
            containerCloningInformation.Text = "Output";
            // 
            // containerLog
            // 
            containerLog.Controls.Add(textBoxLog);
            containerLog.Dock = DockStyle.Bottom;
            containerLog.Font = new Font("Segoe UI", 8.25F);
            containerLog.Location = new Point(3, 70);
            containerLog.Name = "containerLog";
            containerLog.Padding = new Padding(10, 5, 10, 10);
            containerLog.Size = new Size(512, 198);
            containerLog.TabIndex = 6;
            containerLog.TabStop = false;
            containerLog.Text = "Log";
            // 
            // textBoxLog
            // 
            textBoxLog.BackColor = SystemColors.ButtonFace;
            textBoxLog.BorderStyle = BorderStyle.None;
            textBoxLog.Dock = DockStyle.Fill;
            textBoxLog.Font = new Font("Segoe UI", 8.2F);
            textBoxLog.Location = new Point(10, 20);
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            textBoxLog.Size = new Size(492, 168);
            textBoxLog.TabIndex = 0;
            textBoxLog.Text = "";
            // 
            // savePackageDialog
            // 
            savePackageDialog.DefaultExt = "package";
            savePackageDialog.Filter = "Sims 2 Package Files (*.package)|*.package";
            // 
            // containerHeader
            // 
            containerHeader.BackColor = SystemColors.ControlLight;
            containerHeader.Controls.Add(button1);
            containerHeader.Controls.Add(labelSourcePackage);
            containerHeader.Location = new Point(5, 5);
            containerHeader.Margin = new Padding(0);
            containerHeader.Name = "containerHeader";
            containerHeader.Padding = new Padding(5);
            containerHeader.Size = new Size(774, 35);
            containerHeader.TabIndex = 10;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlLightLight;
            button1.Dock = DockStyle.Left;
            button1.Font = new Font("Segoe UI", 9F);
            button1.Location = new Point(5, 5);
            button1.Name = "button1";
            button1.Size = new Size(153, 25);
            button1.TabIndex = 2;
            button1.Text = "📂 Load Source Package";
            button1.UseVisualStyleBackColor = false;
            button1.Click += buttonLoad_Click;
            // 
            // OverlayClonerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 521);
            Controls.Add(containerHeader);
            Controls.Add(containerCloningInformation);
            Controls.Add(containerSourcePackageInfo);
            Controls.Add(containerCloneData);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "OverlayClonerForm";
            Padding = new Padding(5);
            Text = "Overlay Package Cloner";
            ((System.ComponentModel.ISupportInitialize)overlaySourcePackageBindingSource).EndInit();
            containerCloneData.ResumeLayout(false);
            containerCloneData.PerformLayout();
            containerCloneButton.ResumeLayout(false);
            containerToReplace.ResumeLayout(false);
            containerToReplace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)overlayCloneDataBindingSource).EndInit();
            containerOverlayName.ResumeLayout(false);
            containerOverlayName.PerformLayout();
            containerPackageName.ResumeLayout(false);
            containerPackageName.PerformLayout();
            containerIDs.Panel1.ResumeLayout(false);
            containerIDs.Panel1.PerformLayout();
            containerIDs.Panel2.ResumeLayout(false);
            containerIDs.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)containerIDs).EndInit();
            containerIDs.ResumeLayout(false);
            containerGroupID.ResumeLayout(false);
            containerGroupID.PerformLayout();
            containerFamilyID.ResumeLayout(false);
            containerFamilyID.PerformLayout();
            containerTotalResourceCount.ResumeLayout(false);
            containerSourcePackageInfo.ResumeLayout(false);
            containerSourcePackageInfo.PerformLayout();
            containerSourcePackageStatus.ResumeLayout(false);
            containerSourceOverlayName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProviderGeneric).EndInit();
            containerCloningInformation.ResumeLayout(false);
            containerLog.ResumeLayout(false);
            containerHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog selectPackageDialog;
        private System.Windows.Forms.GroupBox containerCloneData;
        private System.Windows.Forms.TextBox textInputPackageName;
        private System.Windows.Forms.ToolStripLabel labelActivePackage;
        private System.Windows.Forms.Label labelSourcePackage;
        private System.Windows.Forms.GroupBox containerSourcePackageInfo;
        private System.Windows.Forms.GroupBox containerTotalResourceCount;
        private System.Windows.Forms.Label labelTotalResourceCount;
        private SortedDictDisplayStringInt dictDisplayResourceCountByType;
        private GroupBox containerSourceOverlayName;
        private Label labelSourceOverlayName;
        private GroupBox containerOverlayName;
        private TextBox textInputOverlayName;
        private GroupBox containerGroupID;
        private TextBox textInputGroupID;
        private GroupBox containerFamilyID;
        private TextBox textInputFamilyID;
        private Label labelSourcePackageStatus;
        private Label labelDotPackage;
        private Panel containerCloneButton;
        private SplitContainer containerIDs;
        private GroupBox containerPackageName;
        private Button buttonClone;
        private BindingSource overlayCloneDataBindingSource;
        private GroupBox containerToReplace;
        private TextBox textInputToReplace;
        private TextBox textInputReplaceBy;
        private Label labelReplaceBy;
        private Panel containerSourcePackageStatus;
        private ToolTip toolTipGeneric;
        private ErrorProvider errorProviderGeneric;
        private GroupBox containerCloningInformation;
        private BindingSource overlaySourcePackageBindingSource;
        private Button buttonRedoPackageData;
        private SaveFileDialog savePackageDialog;
        private GroupBox containerLog;
        private RichTextBox textBoxLog;
        private FlowLayoutPanel containerToolbar;
        private Panel containerHeader;
        private Button button1;
    }
}

