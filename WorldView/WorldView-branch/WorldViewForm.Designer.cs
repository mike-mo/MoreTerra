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
            this.tabPageDrawWorld = new System.Windows.Forms.TabPage();
            this.checkBoxOpenImage = new System.Windows.Forms.CheckBox();
            this.buttonDrawWorld = new System.Windows.Forms.Button();
            this.groupBoxImageOutput = new System.Windows.Forms.GroupBox();
            this.checkBoxDrawWalls = new System.Windows.Forms.CheckBox();
            this.textBoxOutputFile = new System.Windows.Forms.TextBox();
            this.buttonBrowseOutput = new System.Windows.Forms.Button();
            this.groupBoxSelectWorld = new System.Windows.Forms.GroupBox();
            this.comboBoxWorldFilePath = new System.Windows.Forms.ComboBox();
            this.buttonBrowseWorld = new System.Windows.Forms.Button();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSymbols = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabelSelectAllMarkers = new System.Windows.Forms.LinkLabel();
            this.linkLabelSelectNoneMarkers = new System.Windows.Forms.LinkLabel();
            this.linkLabelInvertMarkers = new System.Windows.Forms.LinkLabel();
            this.checkedListBoxMarkers = new System.Windows.Forms.CheckedListBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
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
            this.tabPageWorldInformation = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonLoadInformation = new System.Windows.Forms.Button();
            this.worldInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.worldPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.treeViewChestInformation = new System.Windows.Forms.TreeView();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabelHomepage = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSpecialThanks = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarDrawWorld = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControlSettings.SuspendLayout();
            this.tabPageDrawWorld.SuspendLayout();
            this.groupBoxImageOutput.SuspendLayout();
            this.groupBoxSelectWorld.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxSymbols.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageWorldInformation.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.worldInfoGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.tabControlSettings.Controls.Add(this.tabPageDrawWorld);
            this.tabControlSettings.Controls.Add(this.tabPageSettings);
            this.tabControlSettings.Controls.Add(this.tabPageWorldInformation);
            this.tabControlSettings.Controls.Add(this.tabPageAbout);
            this.tabControlSettings.Location = new System.Drawing.Point(3, 3);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(493, 517);
            this.tabControlSettings.TabIndex = 1;
            // 
            // tabPageDrawWorld
            // 
            this.tabPageDrawWorld.Controls.Add(this.checkBoxOpenImage);
            this.tabPageDrawWorld.Controls.Add(this.buttonDrawWorld);
            this.tabPageDrawWorld.Controls.Add(this.groupBoxImageOutput);
            this.tabPageDrawWorld.Controls.Add(this.groupBoxSelectWorld);
            this.tabPageDrawWorld.Location = new System.Drawing.Point(4, 22);
            this.tabPageDrawWorld.Name = "tabPageDrawWorld";
            this.tabPageDrawWorld.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDrawWorld.Size = new System.Drawing.Size(485, 491);
            this.tabPageDrawWorld.TabIndex = 0;
            this.tabPageDrawWorld.Text = "Draw World";
            this.tabPageDrawWorld.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpenImage
            // 
            this.checkBoxOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxOpenImage.AutoSize = true;
            this.checkBoxOpenImage.Checked = true;
            this.checkBoxOpenImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpenImage.Location = new System.Drawing.Point(12, 423);
            this.checkBoxOpenImage.Name = "checkBoxOpenImage";
            this.checkBoxOpenImage.Size = new System.Drawing.Size(170, 17);
            this.checkBoxOpenImage.TabIndex = 11;
            this.checkBoxOpenImage.Text = "Open World image when done";
            this.checkBoxOpenImage.UseVisualStyleBackColor = true;
            // 
            // buttonDrawWorld
            // 
            this.buttonDrawWorld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDrawWorld.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDrawWorld.Location = new System.Drawing.Point(6, 446);
            this.buttonDrawWorld.Name = "buttonDrawWorld";
            this.buttonDrawWorld.Size = new System.Drawing.Size(473, 39);
            this.buttonDrawWorld.TabIndex = 10;
            this.buttonDrawWorld.Text = "Draw World";
            this.buttonDrawWorld.UseVisualStyleBackColor = true;
            this.buttonDrawWorld.Click += new System.EventHandler(this.buttonDrawWorld_Click);
            // 
            // groupBoxImageOutput
            // 
            this.groupBoxImageOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxImageOutput.Controls.Add(this.checkBoxDrawWalls);
            this.groupBoxImageOutput.Controls.Add(this.textBoxOutputFile);
            this.groupBoxImageOutput.Controls.Add(this.buttonBrowseOutput);
            this.groupBoxImageOutput.Location = new System.Drawing.Point(6, 108);
            this.groupBoxImageOutput.Name = "groupBoxImageOutput";
            this.groupBoxImageOutput.Size = new System.Drawing.Size(473, 96);
            this.groupBoxImageOutput.TabIndex = 9;
            this.groupBoxImageOutput.TabStop = false;
            this.groupBoxImageOutput.Text = "Image Output";
            // 
            // checkBoxDrawWalls
            // 
            this.checkBoxDrawWalls.AutoSize = true;
            this.checkBoxDrawWalls.Checked = true;
            this.checkBoxDrawWalls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrawWalls.Location = new System.Drawing.Point(6, 65);
            this.checkBoxDrawWalls.Name = "checkBoxDrawWalls";
            this.checkBoxDrawWalls.Size = new System.Drawing.Size(80, 17);
            this.checkBoxDrawWalls.TabIndex = 10;
            this.checkBoxDrawWalls.Text = "Draw Walls";
            this.checkBoxDrawWalls.UseVisualStyleBackColor = true;
            this.checkBoxDrawWalls.CheckedChanged += new System.EventHandler(this.checkBoxDrawWalls_CheckedChanged);
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
            this.comboBoxWorldFilePath.TextChanged += new System.EventHandler(this.comboBoxWorldFilePath_TextChanged);
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
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.tableLayoutPanel2);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(485, 491);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
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
            this.groupBoxSymbols.Controls.Add(this.tableLayoutPanel4);
            this.groupBoxSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymbols.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSymbols.Name = "groupBoxSymbols";
            this.groupBoxSymbols.Size = new System.Drawing.Size(233, 479);
            this.groupBoxSymbols.TabIndex = 6;
            this.groupBoxSymbols.TabStop = false;
            this.groupBoxSymbols.Text = "World Markers";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.checkedListBoxMarkers, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox3, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(227, 460);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.linkLabelSelectAllMarkers);
            this.flowLayoutPanel3.Controls.Add(this.linkLabelSelectNoneMarkers);
            this.flowLayoutPanel3.Controls.Add(this.linkLabelInvertMarkers);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(221, 14);
            this.flowLayoutPanel3.TabIndex = 37;
            // 
            // linkLabelSelectAllMarkers
            // 
            this.linkLabelSelectAllMarkers.AutoSize = true;
            this.linkLabelSelectAllMarkers.Location = new System.Drawing.Point(3, 0);
            this.linkLabelSelectAllMarkers.Name = "linkLabelSelectAllMarkers";
            this.linkLabelSelectAllMarkers.Size = new System.Drawing.Size(50, 13);
            this.linkLabelSelectAllMarkers.TabIndex = 0;
            this.linkLabelSelectAllMarkers.TabStop = true;
            this.linkLabelSelectAllMarkers.Text = "Select all";
            this.linkLabelSelectAllMarkers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSelectAllMarkers_LinkClicked);
            // 
            // linkLabelSelectNoneMarkers
            // 
            this.linkLabelSelectNoneMarkers.AutoSize = true;
            this.linkLabelSelectNoneMarkers.Location = new System.Drawing.Point(59, 0);
            this.linkLabelSelectNoneMarkers.Name = "linkLabelSelectNoneMarkers";
            this.linkLabelSelectNoneMarkers.Size = new System.Drawing.Size(64, 13);
            this.linkLabelSelectNoneMarkers.TabIndex = 1;
            this.linkLabelSelectNoneMarkers.TabStop = true;
            this.linkLabelSelectNoneMarkers.Text = "Select none";
            this.linkLabelSelectNoneMarkers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSelectNoneMarkers_LinkClicked);
            // 
            // linkLabelInvertMarkers
            // 
            this.linkLabelInvertMarkers.AutoSize = true;
            this.linkLabelInvertMarkers.Location = new System.Drawing.Point(129, 0);
            this.linkLabelInvertMarkers.Name = "linkLabelInvertMarkers";
            this.linkLabelInvertMarkers.Size = new System.Drawing.Size(34, 13);
            this.linkLabelInvertMarkers.TabIndex = 2;
            this.linkLabelInvertMarkers.TabStop = true;
            this.linkLabelInvertMarkers.Text = "Invert";
            this.linkLabelInvertMarkers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelInvertMarkers_LinkClicked);
            // 
            // checkedListBoxMarkers
            // 
            this.checkedListBoxMarkers.CheckOnClick = true;
            this.checkedListBoxMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxMarkers.FormattingEnabled = true;
            this.checkedListBoxMarkers.IntegralHeight = false;
            this.checkedListBoxMarkers.Location = new System.Drawing.Point(3, 23);
            this.checkedListBoxMarkers.Name = "checkedListBoxMarkers";
            this.checkedListBoxMarkers.Size = new System.Drawing.Size(221, 204);
            this.checkedListBoxMarkers.TabIndex = 36;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(3, 233);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(221, 224);
            this.pictureBox3.TabIndex = 38;
            this.pictureBox3.TabStop = false;
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
            this.tableLayoutPanel1.Controls.Add(this.buttonLoadInformation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.worldInfoGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(479, 485);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonLoadInformation
            // 
            this.buttonLoadInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.buttonLoadInformation, 2);
            this.buttonLoadInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadInformation.Location = new System.Drawing.Point(3, 443);
            this.buttonLoadInformation.Name = "buttonLoadInformation";
            this.buttonLoadInformation.Size = new System.Drawing.Size(473, 39);
            this.buttonLoadInformation.TabIndex = 11;
            this.buttonLoadInformation.Text = "Load Information";
            this.buttonLoadInformation.UseVisualStyleBackColor = true;
            this.buttonLoadInformation.Click += new System.EventHandler(this.buttonLoadInformation_Click);
            // 
            // worldInfoGroupBox
            // 
            this.worldInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.worldInfoGroupBox.Controls.Add(this.worldPropertyGrid);
            this.worldInfoGroupBox.Location = new System.Drawing.Point(3, 3);
            this.worldInfoGroupBox.Name = "worldInfoGroupBox";
            this.worldInfoGroupBox.Size = new System.Drawing.Size(233, 432);
            this.worldInfoGroupBox.TabIndex = 3;
            this.worldInfoGroupBox.TabStop = false;
            this.worldInfoGroupBox.Text = "World Information";
            // 
            // worldPropertyGrid
            // 
            this.worldPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldPropertyGrid.HelpVisible = false;
            this.worldPropertyGrid.Location = new System.Drawing.Point(3, 16);
            this.worldPropertyGrid.Name = "worldPropertyGrid";
            this.worldPropertyGrid.Size = new System.Drawing.Size(227, 413);
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
            this.groupBox4.Size = new System.Drawing.Size(234, 432);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Chests";
            // 
            // treeViewChestInformation
            // 
            this.treeViewChestInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewChestInformation.Location = new System.Drawing.Point(3, 16);
            this.treeViewChestInformation.Name = "treeViewChestInformation";
            this.treeViewChestInformation.Size = new System.Drawing.Size(228, 413);
            this.treeViewChestInformation.TabIndex = 3;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.label3);
            this.tabPageAbout.Controls.Add(this.label2);
            this.tabPageAbout.Controls.Add(this.lblVersion);
            this.tabPageAbout.Controls.Add(this.label1);
            this.tabPageAbout.Controls.Add(this.pictureBox1);
            this.tabPageAbout.Controls.Add(this.linkLabelHomepage);
            this.tabPageAbout.Controls.Add(this.groupBox1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(485, 491);
            this.tabPageAbout.TabIndex = 4;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "by: fperks, noroom";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Homepage:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(129, 70);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(72, 13);
            this.lblVersion.TabIndex = 13;
            this.lblVersion.Text = "Version: 8.8.8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(126, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 33);
            this.label1.TabIndex = 12;
            this.label1.Text = "Terraria World Viewer";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(18, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(102, 92);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabelHomepage
            // 
            this.linkLabelHomepage.AutoSize = true;
            this.linkLabelHomepage.Location = new System.Drawing.Point(197, 103);
            this.linkLabelHomepage.Name = "linkLabelHomepage";
            this.linkLabelHomepage.Size = new System.Drawing.Size(200, 13);
            this.linkLabelHomepage.TabIndex = 10;
            this.linkLabelHomepage.TabStop = true;
            this.linkLabelHomepage.Text = "http://terrariaworldviewer.codeplex.com/";
            this.linkLabelHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHomepage_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelSpecialThanks);
            this.groupBox1.Location = new System.Drawing.Point(3, 421);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 67);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Special Thanks To:";
            // 
            // labelSpecialThanks
            // 
            this.labelSpecialThanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSpecialThanks.Location = new System.Drawing.Point(3, 16);
            this.labelSpecialThanks.Name = "labelSpecialThanks";
            this.labelSpecialThanks.Size = new System.Drawing.Size(473, 48);
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
            this.Load += new System.EventHandler(this.WorldViewForm_Load);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageDrawWorld.ResumeLayout(false);
            this.tabPageDrawWorld.PerformLayout();
            this.groupBoxImageOutput.ResumeLayout(false);
            this.groupBoxImageOutput.PerformLayout();
            this.groupBoxSelectWorld.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBoxSymbols.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPageWorldInformation.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.worldInfoGroupBox.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPageDrawWorld;
        private System.Windows.Forms.TabPage tabPageWorldInformation;
        private System.Windows.Forms.GroupBox groupBoxImageOutput;
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
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBoxSymbols;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelHomepage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckedListBox checkedListBoxChestFilterWeapons;
        private System.Windows.Forms.CheckedListBox checkedListBoxChestFilterAccessories;
        private System.Windows.Forms.Button buttonDrawWorld;
        private System.Windows.Forms.ToolStripStatusLabel labelPercent;
        private System.Windows.Forms.ToolStripProgressBar progressBarDrawWorld;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkLabelSelectAllWeapons;
        private System.Windows.Forms.LinkLabel linkLabelSelectNoneWeapons;
        private System.Windows.Forms.LinkLabel linkLabelInvertWeapons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.LinkLabel linkLabelSelectAllAccessories;
        private System.Windows.Forms.LinkLabel linkLabelSelectNoneAccessories;
        private System.Windows.Forms.LinkLabel linkLabelInvertAccessories;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.LinkLabel linkLabelSelectAllMarkers;
        private System.Windows.Forms.LinkLabel linkLabelSelectNoneMarkers;
        private System.Windows.Forms.LinkLabel linkLabelInvertMarkers;
        private System.Windows.Forms.CheckedListBox checkedListBoxMarkers;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.CheckBox checkBoxOpenImage;
        private System.Windows.Forms.CheckBox checkBoxDrawWalls;
        private System.Windows.Forms.Button buttonLoadInformation;


    }
}