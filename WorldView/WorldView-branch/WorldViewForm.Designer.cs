namespace WorldView
{
    partial class WorldViewForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldViewForm));
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.linkLabelHelp = new System.Windows.Forms.LinkLabel();
            this.checkBoxOpenImage = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawWalls = new System.Windows.Forms.CheckBox();
            this.checkBoxMarkers = new System.Windows.Forms.CheckBox();
            this.buttonDrawWorld = new System.Windows.Forms.Button();
            this.groupBoxImageOutput = new System.Windows.Forms.GroupBox();
            this.textBoxOutputFile = new System.Windows.Forms.TextBox();
            this.buttonBrowseOutput = new System.Windows.Forms.Button();
            this.groupBoxSelectWorld = new System.Windows.Forms.GroupBox();
            this.comboBoxWorldFilePath = new System.Windows.Forms.ComboBox();
            this.buttonBrowseWorld = new System.Windows.Forms.Button();
            this.tabPageWorldInformation = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.worldInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.worldPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.treeViewChestInformation = new System.Windows.Forms.TreeView();
            this.tabPageAdvancedSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSymbols = new System.Windows.Forms.GroupBox();
            this.checkedListBoxMarkers = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabelSelectAllAccessories = new System.Windows.Forms.LinkLabel();
            this.linkLabelSelectNoneAccessories = new System.Windows.Forms.LinkLabel();
            this.linkLabelInvertAccessories = new System.Windows.Forms.LinkLabel();
            this.checkedListBoxChestFilterWeapons = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxChestFilterAccessories = new System.Windows.Forms.CheckedListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabelSelectAllWeapons = new System.Windows.Forms.LinkLabel();
            this.linkLabelSelectNoneWeapons = new System.Windows.Forms.LinkLabel();
            this.linkLabelInvertWeapons = new System.Windows.Forms.LinkLabel();
            this.tabPageHelp = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSpecialThanks = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarDrawWorld = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControlSettings.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.groupBoxImageOutput.SuspendLayout();
            this.groupBoxSelectWorld.SuspendLayout();
            this.tabPageWorldInformation.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.worldInfoGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPageAdvancedSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxSymbols.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPageAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSettings.Controls.Add(this.tabPagePreview);
            this.tabControlSettings.Controls.Add(this.tabPageWorldInformation);
            this.tabControlSettings.Controls.Add(this.tabPageAdvancedSettings);
            this.tabControlSettings.Controls.Add(this.tabPageHelp);
            this.tabControlSettings.Controls.Add(this.tabPageAbout);
            this.tabControlSettings.Location = new System.Drawing.Point(3, 3);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(493, 517);
            this.tabControlSettings.TabIndex = 1;
            // 
            // tabPagePreview
            // 
            this.tabPagePreview.Controls.Add(this.groupBoxOptions);
            this.tabPagePreview.Controls.Add(this.buttonDrawWorld);
            this.tabPagePreview.Controls.Add(this.groupBoxImageOutput);
            this.tabPagePreview.Controls.Add(this.groupBoxSelectWorld);
            this.tabPagePreview.Location = new System.Drawing.Point(4, 22);
            this.tabPagePreview.Name = "tabPagePreview";
            this.tabPagePreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePreview.Size = new System.Drawing.Size(485, 491);
            this.tabPagePreview.TabIndex = 0;
            this.tabPagePreview.Text = "Main";
            this.tabPagePreview.UseVisualStyleBackColor = true;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.Controls.Add(this.linkLabelHelp);
            this.groupBoxOptions.Controls.Add(this.checkBoxOpenImage);
            this.groupBoxOptions.Controls.Add(this.checkBoxDrawWalls);
            this.groupBoxOptions.Controls.Add(this.checkBoxMarkers);
            this.groupBoxOptions.Location = new System.Drawing.Point(6, 210);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(473, 91);
            this.groupBoxOptions.TabIndex = 11;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // linkLabelHelp
            // 
            this.linkLabelHelp.AutoSize = true;
            this.linkLabelHelp.Location = new System.Drawing.Point(104, 20);
            this.linkLabelHelp.Name = "linkLabelHelp";
            this.linkLabelHelp.Size = new System.Drawing.Size(65, 13);
            this.linkLabelHelp.TabIndex = 11;
            this.linkLabelHelp.TabStop = true;
            this.linkLabelHelp.Text = "What\'s this?";
            this.linkLabelHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHelp_LinkClicked);
            // 
            // checkBoxOpenImage
            // 
            this.checkBoxOpenImage.AutoSize = true;
            this.checkBoxOpenImage.Checked = true;
            this.checkBoxOpenImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpenImage.Location = new System.Drawing.Point(6, 65);
            this.checkBoxOpenImage.Name = "checkBoxOpenImage";
            this.checkBoxOpenImage.Size = new System.Drawing.Size(224, 17);
            this.checkBoxOpenImage.TabIndex = 10;
            this.checkBoxOpenImage.Text = "Automatically open image after completion";
            this.checkBoxOpenImage.UseVisualStyleBackColor = true;
            // 
            // checkBoxDrawWalls
            // 
            this.checkBoxDrawWalls.AutoSize = true;
            this.checkBoxDrawWalls.Checked = true;
            this.checkBoxDrawWalls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrawWalls.Location = new System.Drawing.Point(6, 42);
            this.checkBoxDrawWalls.Name = "checkBoxDrawWalls";
            this.checkBoxDrawWalls.Size = new System.Drawing.Size(80, 17);
            this.checkBoxDrawWalls.TabIndex = 9;
            this.checkBoxDrawWalls.Text = "Draw Walls";
            this.checkBoxDrawWalls.UseVisualStyleBackColor = true;
            // 
            // checkBoxMarkers
            // 
            this.checkBoxMarkers.AutoSize = true;
            this.checkBoxMarkers.Checked = true;
            this.checkBoxMarkers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMarkers.Location = new System.Drawing.Point(6, 19);
            this.checkBoxMarkers.Name = "checkBoxMarkers";
            this.checkBoxMarkers.Size = new System.Drawing.Size(92, 17);
            this.checkBoxMarkers.TabIndex = 8;
            this.checkBoxMarkers.Text = "Draw Markers";
            this.checkBoxMarkers.UseVisualStyleBackColor = true;
            this.checkBoxMarkers.CheckedChanged += new System.EventHandler(this.checkBoxMarkers_CheckedChanged);
            // 
            // buttonDrawWorld
            // 
            this.buttonDrawWorld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDrawWorld.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDrawWorld.Location = new System.Drawing.Point(12, 432);
            this.buttonDrawWorld.Name = "buttonDrawWorld";
            this.buttonDrawWorld.Size = new System.Drawing.Size(457, 39);
            this.buttonDrawWorld.TabIndex = 10;
            this.buttonDrawWorld.Text = "Draw World";
            this.buttonDrawWorld.UseVisualStyleBackColor = true;
            this.buttonDrawWorld.Click += new System.EventHandler(this.buttonDrawWorld_Click);
            // 
            // groupBoxImageOutput
            // 
            this.groupBoxImageOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxImageOutput.Controls.Add(this.textBoxOutputFile);
            this.groupBoxImageOutput.Controls.Add(this.buttonBrowseOutput);
            this.groupBoxImageOutput.Location = new System.Drawing.Point(6, 108);
            this.groupBoxImageOutput.Name = "groupBoxImageOutput";
            this.groupBoxImageOutput.Size = new System.Drawing.Size(473, 96);
            this.groupBoxImageOutput.TabIndex = 9;
            this.groupBoxImageOutput.TabStop = false;
            this.groupBoxImageOutput.Text = "Image Output";
            // 
            // textBoxOutputFile
            // 
            this.textBoxOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputFile.Location = new System.Drawing.Point(6, 32);
            this.textBoxOutputFile.Name = "textBoxOutputFile";
            this.textBoxOutputFile.Size = new System.Drawing.Size(461, 20);
            this.textBoxOutputFile.TabIndex = 4;
            // 
            // buttonBrowseOutput
            // 
            this.buttonBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseOutput.Location = new System.Drawing.Point(349, 58);
            this.buttonBrowseOutput.Name = "buttonBrowseOutput";
            this.buttonBrowseOutput.Size = new System.Drawing.Size(118, 28);
            this.buttonBrowseOutput.TabIndex = 5;
            this.buttonBrowseOutput.Text = "Browse...";
            this.buttonBrowseOutput.UseVisualStyleBackColor = true;
            this.buttonBrowseOutput.Click += new System.EventHandler(this.buttonBrowseOutput_Click);
            // 
            // groupBoxSelectWorld
            // 
            this.groupBoxSelectWorld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSelectWorld.Controls.Add(this.comboBoxWorldFilePath);
            this.groupBoxSelectWorld.Controls.Add(this.buttonBrowseWorld);
            this.groupBoxSelectWorld.Location = new System.Drawing.Point(6, 6);
            this.groupBoxSelectWorld.Name = "groupBoxSelectWorld";
            this.groupBoxSelectWorld.Size = new System.Drawing.Size(473, 96);
            this.groupBoxSelectWorld.TabIndex = 5;
            this.groupBoxSelectWorld.TabStop = false;
            this.groupBoxSelectWorld.Text = "Select World";
            // 
            // comboBoxWorldFilePath
            // 
            this.comboBoxWorldFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWorldFilePath.FormattingEnabled = true;
            this.comboBoxWorldFilePath.Location = new System.Drawing.Point(6, 32);
            this.comboBoxWorldFilePath.Name = "comboBoxWorldFilePath";
            this.comboBoxWorldFilePath.Size = new System.Drawing.Size(461, 21);
            this.comboBoxWorldFilePath.TabIndex = 3;
            // 
            // buttonBrowseWorld
            // 
            this.buttonBrowseWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseWorld.Location = new System.Drawing.Point(349, 59);
            this.buttonBrowseWorld.Name = "buttonBrowseWorld";
            this.buttonBrowseWorld.Size = new System.Drawing.Size(118, 28);
            this.buttonBrowseWorld.TabIndex = 2;
            this.buttonBrowseWorld.Text = "Browse...";
            this.buttonBrowseWorld.UseVisualStyleBackColor = true;
            this.buttonBrowseWorld.Click += new System.EventHandler(this.buttonBrowseWorld_Click);
            // 
            // tabPageWorldInformation
            // 
            this.tabPageWorldInformation.Controls.Add(this.tableLayoutPanel1);
            this.tabPageWorldInformation.Location = new System.Drawing.Point(4, 22);
            this.tabPageWorldInformation.Name = "tabPageWorldInformation";
            this.tabPageWorldInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWorldInformation.Size = new System.Drawing.Size(485, 491);
            this.tabPageWorldInformation.TabIndex = 1;
            this.tabPageWorldInformation.Text = "World Information";
            this.tabPageWorldInformation.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.worldInfoGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(479, 485);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // worldInfoGroupBox
            // 
            this.worldInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.worldInfoGroupBox.Controls.Add(this.worldPropertyGrid);
            this.worldInfoGroupBox.Location = new System.Drawing.Point(3, 3);
            this.worldInfoGroupBox.Name = "worldInfoGroupBox";
            this.worldInfoGroupBox.Size = new System.Drawing.Size(233, 479);
            this.worldInfoGroupBox.TabIndex = 3;
            this.worldInfoGroupBox.TabStop = false;
            this.worldInfoGroupBox.Text = "World Information";
            // 
            // worldPropertyGrid
            // 
            this.worldPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldPropertyGrid.Location = new System.Drawing.Point(3, 16);
            this.worldPropertyGrid.Name = "worldPropertyGrid";
            this.worldPropertyGrid.Size = new System.Drawing.Size(227, 460);
            this.worldPropertyGrid.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.treeViewChestInformation);
            this.groupBox4.Location = new System.Drawing.Point(242, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(234, 479);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Chests";
            // 
            // treeViewChestInformation
            // 
            this.treeViewChestInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewChestInformation.Location = new System.Drawing.Point(3, 16);
            this.treeViewChestInformation.Name = "treeViewChestInformation";
            this.treeViewChestInformation.Size = new System.Drawing.Size(228, 460);
            this.treeViewChestInformation.TabIndex = 3;
            // 
            // tabPageAdvancedSettings
            // 
            this.tabPageAdvancedSettings.Controls.Add(this.tableLayoutPanel2);
            this.tabPageAdvancedSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvancedSettings.Name = "tabPageAdvancedSettings";
            this.tabPageAdvancedSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdvancedSettings.Size = new System.Drawing.Size(485, 491);
            this.tabPageAdvancedSettings.TabIndex = 2;
            this.tabPageAdvancedSettings.Text = "Advanced";
            this.tabPageAdvancedSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBoxSymbols, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(479, 485);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBoxSymbols
            // 
            this.groupBoxSymbols.Controls.Add(this.checkedListBoxMarkers);
            this.groupBoxSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymbols.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSymbols.Name = "groupBoxSymbols";
            this.groupBoxSymbols.Size = new System.Drawing.Size(233, 479);
            this.groupBoxSymbols.TabIndex = 6;
            this.groupBoxSymbols.TabStop = false;
            this.groupBoxSymbols.Text = "World Markers";
            // 
            // checkedListBoxMarkers
            // 
            this.checkedListBoxMarkers.CheckOnClick = true;
            this.checkedListBoxMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxMarkers.FormattingEnabled = true;
            this.checkedListBoxMarkers.IntegralHeight = false;
            this.checkedListBoxMarkers.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxMarkers.Name = "checkedListBoxMarkers";
            this.checkedListBoxMarkers.Size = new System.Drawing.Size(227, 460);
            this.checkedListBoxMarkers.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(242, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 479);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Only Show Chests that Contain";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.checkedListBoxChestFilterWeapons, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkedListBoxChestFilterAccessories, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(228, 460);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.linkLabelSelectAllAccessories);
            this.flowLayoutPanel2.Controls.Add(this.linkLabelSelectNoneAccessories);
            this.flowLayoutPanel2.Controls.Add(this.linkLabelInvertAccessories);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 233);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(222, 14);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // linkLabelSelectAllAccessories
            // 
            this.linkLabelSelectAllAccessories.AutoSize = true;
            this.linkLabelSelectAllAccessories.Location = new System.Drawing.Point(3, 0);
            this.linkLabelSelectAllAccessories.Name = "linkLabelSelectAllAccessories";
            this.linkLabelSelectAllAccessories.Size = new System.Drawing.Size(50, 13);
            this.linkLabelSelectAllAccessories.TabIndex = 0;
            this.linkLabelSelectAllAccessories.TabStop = true;
            this.linkLabelSelectAllAccessories.Text = "Select all";
            this.linkLabelSelectAllAccessories.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSelectAllAccessories_LinkClicked);
            // 
            // linkLabelSelectNoneAccessories
            // 
            this.linkLabelSelectNoneAccessories.AutoSize = true;
            this.linkLabelSelectNoneAccessories.Location = new System.Drawing.Point(59, 0);
            this.linkLabelSelectNoneAccessories.Name = "linkLabelSelectNoneAccessories";
            this.linkLabelSelectNoneAccessories.Size = new System.Drawing.Size(64, 13);
            this.linkLabelSelectNoneAccessories.TabIndex = 1;
            this.linkLabelSelectNoneAccessories.TabStop = true;
            this.linkLabelSelectNoneAccessories.Text = "Select none";
            this.linkLabelSelectNoneAccessories.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSelectNoneAccessories_LinkClicked);
            // 
            // linkLabelInvertAccessories
            // 
            this.linkLabelInvertAccessories.AutoSize = true;
            this.linkLabelInvertAccessories.Location = new System.Drawing.Point(129, 0);
            this.linkLabelInvertAccessories.Name = "linkLabelInvertAccessories";
            this.linkLabelInvertAccessories.Size = new System.Drawing.Size(34, 13);
            this.linkLabelInvertAccessories.TabIndex = 2;
            this.linkLabelInvertAccessories.TabStop = true;
            this.linkLabelInvertAccessories.Text = "Invert";
            this.linkLabelInvertAccessories.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelInvertAccessories_LinkClicked);
            // 
            // checkedListBoxChestFilterWeapons
            // 
            this.checkedListBoxChestFilterWeapons.CheckOnClick = true;
            this.checkedListBoxChestFilterWeapons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxChestFilterWeapons.FormattingEnabled = true;
            this.checkedListBoxChestFilterWeapons.IntegralHeight = false;
            this.checkedListBoxChestFilterWeapons.Location = new System.Drawing.Point(3, 23);
            this.checkedListBoxChestFilterWeapons.Name = "checkedListBoxChestFilterWeapons";
            this.checkedListBoxChestFilterWeapons.Size = new System.Drawing.Size(222, 204);
            this.checkedListBoxChestFilterWeapons.Sorted = true;
            this.checkedListBoxChestFilterWeapons.TabIndex = 5;
            // 
            // checkedListBoxChestFilterAccessories
            // 
            this.checkedListBoxChestFilterAccessories.CheckOnClick = true;
            this.checkedListBoxChestFilterAccessories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxChestFilterAccessories.FormattingEnabled = true;
            this.checkedListBoxChestFilterAccessories.IntegralHeight = false;
            this.checkedListBoxChestFilterAccessories.Location = new System.Drawing.Point(3, 253);
            this.checkedListBoxChestFilterAccessories.Name = "checkedListBoxChestFilterAccessories";
            this.checkedListBoxChestFilterAccessories.Size = new System.Drawing.Size(222, 204);
            this.checkedListBoxChestFilterAccessories.Sorted = true;
            this.checkedListBoxChestFilterAccessories.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.linkLabelSelectAllWeapons);
            this.flowLayoutPanel1.Controls.Add(this.linkLabelSelectNoneWeapons);
            this.flowLayoutPanel1.Controls.Add(this.linkLabelInvertWeapons);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(222, 14);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // linkLabelSelectAllWeapons
            // 
            this.linkLabelSelectAllWeapons.AutoSize = true;
            this.linkLabelSelectAllWeapons.Location = new System.Drawing.Point(3, 0);
            this.linkLabelSelectAllWeapons.Name = "linkLabelSelectAllWeapons";
            this.linkLabelSelectAllWeapons.Size = new System.Drawing.Size(50, 13);
            this.linkLabelSelectAllWeapons.TabIndex = 0;
            this.linkLabelSelectAllWeapons.TabStop = true;
            this.linkLabelSelectAllWeapons.Text = "Select all";
            this.linkLabelSelectAllWeapons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSelectAllWeapons_LinkClicked);
            // 
            // linkLabelSelectNoneWeapons
            // 
            this.linkLabelSelectNoneWeapons.AutoSize = true;
            this.linkLabelSelectNoneWeapons.Location = new System.Drawing.Point(59, 0);
            this.linkLabelSelectNoneWeapons.Name = "linkLabelSelectNoneWeapons";
            this.linkLabelSelectNoneWeapons.Size = new System.Drawing.Size(64, 13);
            this.linkLabelSelectNoneWeapons.TabIndex = 1;
            this.linkLabelSelectNoneWeapons.TabStop = true;
            this.linkLabelSelectNoneWeapons.Text = "Select none";
            this.linkLabelSelectNoneWeapons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSelectNoneWeapons_LinkClicked);
            // 
            // linkLabelInvertWeapons
            // 
            this.linkLabelInvertWeapons.AutoSize = true;
            this.linkLabelInvertWeapons.Location = new System.Drawing.Point(129, 0);
            this.linkLabelInvertWeapons.Name = "linkLabelInvertWeapons";
            this.linkLabelInvertWeapons.Size = new System.Drawing.Size(34, 13);
            this.linkLabelInvertWeapons.TabIndex = 2;
            this.linkLabelInvertWeapons.TabStop = true;
            this.linkLabelInvertWeapons.Text = "Invert";
            this.linkLabelInvertWeapons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelInvertWeapons_LinkClicked);
            // 
            // tabPageHelp
            // 
            this.tabPageHelp.Controls.Add(this.pictureBox2);
            this.tabPageHelp.Location = new System.Drawing.Point(4, 22);
            this.tabPageHelp.Name = "tabPageHelp";
            this.tabPageHelp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHelp.Size = new System.Drawing.Size(485, 491);
            this.tabPageHelp.TabIndex = 5;
            this.tabPageHelp.Text = "Help";
            this.tabPageHelp.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(473, 320);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.label1);
            this.tabPageAbout.Controls.Add(this.pictureBox1);
            this.tabPageAbout.Controls.Add(this.linkLabel1);
            this.tabPageAbout.Controls.Add(this.groupBox1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(485, 491);
            this.tabPageAbout.TabIndex = 4;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(18, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(102, 92);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(145, 103);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelSpecialThanks);
            this.groupBox1.Location = new System.Drawing.Point(3, 395);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 93);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Special Thanks To:";
            // 
            // labelSpecialThanks
            // 
            this.labelSpecialThanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSpecialThanks.Location = new System.Drawing.Point(3, 16);
            this.labelSpecialThanks.Name = "labelSpecialThanks";
            this.labelSpecialThanks.Size = new System.Drawing.Size(473, 74);
            this.labelSpecialThanks.TabIndex = 0;
            this.labelSpecialThanks.Text = "SpecialThanks";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.labelPercent,
            this.progressBarDrawWorld});
            this.statusStrip1.Location = new System.Drawing.Point(3, 523);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(493, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(39, 17);
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPercent
            // 
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(439, 17);
            this.labelPercent.Spring = true;
            this.labelPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBarDrawWorld
            // 
            this.progressBarDrawWorld.Name = "progressBarDrawWorld";
            this.progressBarDrawWorld.Size = new System.Drawing.Size(300, 16);
            this.progressBarDrawWorld.Visible = false;
            // 
            // WorldViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 548);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControlSettings);
            this.MinimumSize = new System.Drawing.Size(443, 473);
            this.Name = "WorldViewForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Terraria World Viewer";
            this.tabControlSettings.ResumeLayout(false);
            this.tabPagePreview.ResumeLayout(false);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.groupBoxImageOutput.ResumeLayout(false);
            this.groupBoxImageOutput.PerformLayout();
            this.groupBoxSelectWorld.ResumeLayout(false);
            this.tabPageWorldInformation.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.worldInfoGroupBox.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabPageAdvancedSettings.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBoxSymbols.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPageHelp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPagePreview;
        private System.Windows.Forms.TabPage tabPageWorldInformation;
        private System.Windows.Forms.GroupBox groupBoxImageOutput;
        private System.Windows.Forms.CheckBox checkBoxDrawWalls;
        private System.Windows.Forms.CheckBox checkBoxMarkers;
        private System.Windows.Forms.TextBox textBoxOutputFile;
        private System.Windows.Forms.Button buttonBrowseOutput;
        private System.Windows.Forms.GroupBox groupBoxSelectWorld;
        private System.Windows.Forms.Button buttonBrowseWorld;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelSpecialThanks;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ComboBox comboBoxWorldFilePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox worldInfoGroupBox;
        private System.Windows.Forms.PropertyGrid worldPropertyGrid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TreeView treeViewChestInformation;
        private System.Windows.Forms.TabPage tabPageAdvancedSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBoxSymbols;
        private System.Windows.Forms.CheckedListBox checkedListBoxMarkers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckedListBox checkedListBoxChestFilterWeapons;
        private System.Windows.Forms.CheckedListBox checkedListBoxChestFilterAccessories;
        private System.Windows.Forms.Button buttonDrawWorld;
        private System.Windows.Forms.ToolStripStatusLabel labelPercent;
        private System.Windows.Forms.ToolStripProgressBar progressBarDrawWorld;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.LinkLabel linkLabelHelp;
        private System.Windows.Forms.CheckBox checkBoxOpenImage;
        private System.Windows.Forms.TabPage tabPageHelp;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkLabelSelectAllWeapons;
        private System.Windows.Forms.LinkLabel linkLabelSelectNoneWeapons;
        private System.Windows.Forms.LinkLabel linkLabelInvertWeapons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.LinkLabel linkLabelSelectAllAccessories;
        private System.Windows.Forms.LinkLabel linkLabelSelectNoneAccessories;
        private System.Windows.Forms.LinkLabel linkLabelInvertAccessories;


    }
}