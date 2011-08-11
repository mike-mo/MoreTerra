namespace MoreTerra
{
    partial class FormWorldView
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorldView));
			this.tabControlSettings = new System.Windows.Forms.TabControl();
			this.tabPageDrawWorld = new System.Windows.Forms.TabPage();
			this.checkBoxScanForItems = new System.Windows.Forms.CheckBox();
			this.checkBoxOpenImage = new System.Windows.Forms.CheckBox();
			this.buttonDrawWorld = new System.Windows.Forms.Button();
			this.groupBoxImageOutput = new System.Windows.Forms.GroupBox();
			this.textBoxOutputFile = new System.Windows.Forms.TextBox();
			this.buttonBrowseOutput = new System.Windows.Forms.Button();
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.groupBoxSymbols = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.buttonCustomResources = new System.Windows.Forms.Button();
			this.labelCustomResources = new System.Windows.Forms.Label();
			this.checkBoxDrawWalls = new System.Windows.Forms.CheckBox();
			this.treeViewMarkerList = new System.Windows.Forms.TreeView();
			this.contextMenuStripListOperations = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.invertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPageChestFinder = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.buttonMoveAllToFiltered = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lstAvailableItems = new System.Windows.Forms.ListBox();
			this.buttonRemoveCustomItem = new System.Windows.Forms.Button();
			this.buttonAddCustomItem = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lstFilteredItems = new System.Windows.Forms.ListBox();
			this.buttonMoveAllToAvailable = new System.Windows.Forms.Button();
			this.checkBoxFilterChests = new System.Windows.Forms.CheckBox();
			this.tabPageWorldInformation = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.worldInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.worldPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.treeViewChestInformation = new System.Windows.Forms.TreeView();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.radioButtonSortByX = new System.Windows.Forms.RadioButton();
			this.radioButtonSortByY = new System.Windows.Forms.RadioButton();
			this.radioButtonSortByNone = new System.Windows.Forms.RadioButton();
			this.buttonLoadInformation = new System.Windows.Forms.Button();
			this.tabPageAbout = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.linkLabelHomepage = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelSpecialThanks = new System.Windows.Forms.Label();
			this.groupBoxSelectWorld = new System.Windows.Forms.GroupBox();
			this.labelWorldName = new System.Windows.Forms.Label();
			this.comboBoxWorldFilePath = new System.Windows.Forms.ComboBox();
			this.buttonBrowseWorld = new System.Windows.Forms.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.comboBoxDrawUsing = new System.Windows.Forms.ComboBox();
			this.tabControlSettings.SuspendLayout();
			this.tabPageDrawWorld.SuspendLayout();
			this.groupBoxImageOutput.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			this.groupBoxSymbols.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.contextMenuStripListOperations.SuspendLayout();
			this.tabPageChestFinder.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabPageWorldInformation.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.worldInfoGroupBox.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.tabPageAbout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBoxSelectWorld.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlSettings
			// 
			this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlSettings.Controls.Add(this.tabPageDrawWorld);
			this.tabControlSettings.Controls.Add(this.tabPageSettings);
			this.tabControlSettings.Controls.Add(this.tabPageChestFinder);
			this.tabControlSettings.Controls.Add(this.tabPageWorldInformation);
			this.tabControlSettings.Controls.Add(this.tabPageAbout);
			this.tabControlSettings.Location = new System.Drawing.Point(6, 108);
			this.tabControlSettings.Name = "tabControlSettings";
			this.tabControlSettings.SelectedIndex = 0;
			this.tabControlSettings.Size = new System.Drawing.Size(482, 338);
			this.tabControlSettings.TabIndex = 1;
			this.tabControlSettings.SelectedIndexChanged += new System.EventHandler(this.tabControlSettings_SelectedIndexChanged);
			// 
			// tabPageDrawWorld
			// 
			this.tabPageDrawWorld.Controls.Add(this.groupBox6);
			this.tabPageDrawWorld.Controls.Add(this.checkBoxScanForItems);
			this.tabPageDrawWorld.Controls.Add(this.checkBoxOpenImage);
			this.tabPageDrawWorld.Controls.Add(this.buttonDrawWorld);
			this.tabPageDrawWorld.Controls.Add(this.groupBoxImageOutput);
			this.tabPageDrawWorld.Location = new System.Drawing.Point(4, 22);
			this.tabPageDrawWorld.Name = "tabPageDrawWorld";
			this.tabPageDrawWorld.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDrawWorld.Size = new System.Drawing.Size(474, 312);
			this.tabPageDrawWorld.TabIndex = 0;
			this.tabPageDrawWorld.Text = "Draw World";
			this.tabPageDrawWorld.UseVisualStyleBackColor = true;
			// 
			// checkBoxScanForItems
			// 
			this.checkBoxScanForItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxScanForItems.AutoSize = true;
			this.checkBoxScanForItems.Location = new System.Drawing.Point(12, 221);
			this.checkBoxScanForItems.Name = "checkBoxScanForItems";
			this.checkBoxScanForItems.Size = new System.Drawing.Size(161, 17);
			this.checkBoxScanForItems.TabIndex = 12;
			this.checkBoxScanForItems.Text = "Scan for new items in chests";
			this.checkBoxScanForItems.UseVisualStyleBackColor = true;
			this.checkBoxScanForItems.CheckedChanged += new System.EventHandler(this.checkBoxScanForItems_CheckedChanged);
			// 
			// checkBoxOpenImage
			// 
			this.checkBoxOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxOpenImage.AutoSize = true;
			this.checkBoxOpenImage.Checked = true;
			this.checkBoxOpenImage.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxOpenImage.Location = new System.Drawing.Point(12, 244);
			this.checkBoxOpenImage.Name = "checkBoxOpenImage";
			this.checkBoxOpenImage.Size = new System.Drawing.Size(170, 17);
			this.checkBoxOpenImage.TabIndex = 11;
			this.checkBoxOpenImage.Text = "Open World image when done";
			this.checkBoxOpenImage.UseVisualStyleBackColor = true;
			this.checkBoxOpenImage.CheckedChanged += new System.EventHandler(this.checkBoxOpenImage_CheckedChanged);
			// 
			// buttonDrawWorld
			// 
			this.buttonDrawWorld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDrawWorld.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonDrawWorld.Location = new System.Drawing.Point(6, 267);
			this.buttonDrawWorld.Name = "buttonDrawWorld";
			this.buttonDrawWorld.Size = new System.Drawing.Size(462, 39);
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
			this.groupBoxImageOutput.Location = new System.Drawing.Point(6, 6);
			this.groupBoxImageOutput.Name = "groupBoxImageOutput";
			this.groupBoxImageOutput.Size = new System.Drawing.Size(462, 96);
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
			this.textBoxOutputFile.Size = new System.Drawing.Size(450, 20);
			this.textBoxOutputFile.TabIndex = 4;
			// 
			// buttonBrowseOutput
			// 
			this.buttonBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseOutput.Location = new System.Drawing.Point(338, 58);
			this.buttonBrowseOutput.Name = "buttonBrowseOutput";
			this.buttonBrowseOutput.Size = new System.Drawing.Size(118, 28);
			this.buttonBrowseOutput.TabIndex = 5;
			this.buttonBrowseOutput.Text = "Browse...";
			this.buttonBrowseOutput.UseVisualStyleBackColor = true;
			this.buttonBrowseOutput.Click += new System.EventHandler(this.buttonBrowseOutput_Click);
			// 
			// tabPageSettings
			// 
			this.tabPageSettings.Controls.Add(this.groupBoxSymbols);
			this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageSettings.Name = "tabPageSettings";
			this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSettings.Size = new System.Drawing.Size(474, 312);
			this.tabPageSettings.TabIndex = 2;
			this.tabPageSettings.Text = "Settings";
			this.tabPageSettings.UseVisualStyleBackColor = true;
			// 
			// groupBoxSymbols
			// 
			this.groupBoxSymbols.Controls.Add(this.tableLayoutPanel4);
			this.groupBoxSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxSymbols.Location = new System.Drawing.Point(3, 3);
			this.groupBoxSymbols.Name = "groupBoxSymbols";
			this.groupBoxSymbols.Size = new System.Drawing.Size(468, 306);
			this.groupBoxSymbols.TabIndex = 7;
			this.groupBoxSymbols.TabStop = false;
			this.groupBoxSymbols.Text = "World Markers";
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 234F));
			this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.treeViewMarkerList, 0, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(462, 287);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.buttonCustomResources, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.labelCustomResources, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.checkBoxDrawWalls, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(231, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(228, 281);
			this.tableLayoutPanel2.TabIndex = 42;
			// 
			// buttonCustomResources
			// 
			this.buttonCustomResources.Location = new System.Drawing.Point(3, 248);
			this.buttonCustomResources.Name = "buttonCustomResources";
			this.buttonCustomResources.Size = new System.Drawing.Size(152, 28);
			this.buttonCustomResources.TabIndex = 43;
			this.buttonCustomResources.Text = "Open Resource Folder";
			this.buttonCustomResources.UseVisualStyleBackColor = true;
			this.buttonCustomResources.Click += new System.EventHandler(this.buttonCustomResources_Click);
			// 
			// labelCustomResources
			// 
			this.labelCustomResources.AutoSize = true;
			this.labelCustomResources.Location = new System.Drawing.Point(3, 174);
			this.labelCustomResources.Name = "labelCustomResources";
			this.labelCustomResources.Size = new System.Drawing.Size(115, 13);
			this.labelCustomResources.TabIndex = 44;
			this.labelCustomResources.Text = "labelCustomResources";
			// 
			// checkBoxDrawWalls
			// 
			this.checkBoxDrawWalls.AutoSize = true;
			this.checkBoxDrawWalls.Checked = true;
			this.checkBoxDrawWalls.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxDrawWalls.Location = new System.Drawing.Point(3, 3);
			this.checkBoxDrawWalls.Name = "checkBoxDrawWalls";
			this.checkBoxDrawWalls.Size = new System.Drawing.Size(80, 17);
			this.checkBoxDrawWalls.TabIndex = 41;
			this.checkBoxDrawWalls.Text = "Draw Walls";
			this.checkBoxDrawWalls.UseVisualStyleBackColor = true;
			this.checkBoxDrawWalls.CheckedChanged += new System.EventHandler(this.checkBoxDrawWalls_CheckedChanged);
			// 
			// treeViewMarkerList
			// 
			this.treeViewMarkerList.CheckBoxes = true;
			this.treeViewMarkerList.ContextMenuStrip = this.contextMenuStripListOperations;
			this.treeViewMarkerList.Location = new System.Drawing.Point(3, 3);
			this.treeViewMarkerList.Name = "treeViewMarkerList";
			this.treeViewMarkerList.Size = new System.Drawing.Size(222, 281);
			this.treeViewMarkerList.TabIndex = 44;
			// 
			// contextMenuStripListOperations
			// 
			this.contextMenuStripListOperations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.selectNoneToolStripMenuItem,
            this.invertSelectionToolStripMenuItem});
			this.contextMenuStripListOperations.Name = "contextMenuStripListOperations";
			this.contextMenuStripListOperations.ShowImageMargin = false;
			this.contextMenuStripListOperations.Size = new System.Drawing.Size(128, 92);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.selectAllToolStripMenuItem.Text = "Select all";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// selectNoneToolStripMenuItem
			// 
			this.selectNoneToolStripMenuItem.Name = "selectNoneToolStripMenuItem";
			this.selectNoneToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.selectNoneToolStripMenuItem.Text = "Select none";
			this.selectNoneToolStripMenuItem.Click += new System.EventHandler(this.selectNoneToolStripMenuItem_Click);
			// 
			// invertSelectionToolStripMenuItem
			// 
			this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
			this.invertSelectionToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.invertSelectionToolStripMenuItem.Text = "Invert selection";
			this.invertSelectionToolStripMenuItem.Click += new System.EventHandler(this.invertSelectionToolStripMenuItem_Click);
			// 
			// tabPageChestFinder
			// 
			this.tabPageChestFinder.Controls.Add(this.tableLayoutPanel3);
			this.tabPageChestFinder.Controls.Add(this.checkBoxFilterChests);
			this.tabPageChestFinder.Location = new System.Drawing.Point(4, 22);
			this.tabPageChestFinder.Name = "tabPageChestFinder";
			this.tabPageChestFinder.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageChestFinder.Size = new System.Drawing.Size(474, 312);
			this.tabPageChestFinder.TabIndex = 5;
			this.tabPageChestFinder.Text = "Chest Finder";
			this.tabPageChestFinder.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.ColumnCount = 4;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
			this.tableLayoutPanel3.Controls.Add(this.buttonMoveAllToFiltered, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.buttonRemoveCustomItem, 2, 1);
			this.tableLayoutPanel3.Controls.Add(this.buttonAddCustomItem, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.groupBox3, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.buttonMoveAllToAvailable, 3, 1);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 31);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(462, 275);
			this.tableLayoutPanel3.TabIndex = 2;
			// 
			// buttonMoveAllToFiltered
			// 
			this.buttonMoveAllToFiltered.Location = new System.Drawing.Point(3, 248);
			this.buttonMoveAllToFiltered.Name = "buttonMoveAllToFiltered";
			this.buttonMoveAllToFiltered.Size = new System.Drawing.Size(75, 23);
			this.buttonMoveAllToFiltered.TabIndex = 5;
			this.buttonMoveAllToFiltered.Text = "Move All -->";
			this.buttonMoveAllToFiltered.UseVisualStyleBackColor = true;
			this.buttonMoveAllToFiltered.Click += new System.EventHandler(this.buttonMoveAllToFiltered_Click);
			// 
			// groupBox2
			// 
			this.tableLayoutPanel3.SetColumnSpan(this.groupBox2, 2);
			this.groupBox2.Controls.Add(this.lstAvailableItems);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(224, 239);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Available Items";
			// 
			// lstAvailableItems
			// 
			this.lstAvailableItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstAvailableItems.Enabled = false;
			this.lstAvailableItems.FormattingEnabled = true;
			this.lstAvailableItems.IntegralHeight = false;
			this.lstAvailableItems.Location = new System.Drawing.Point(3, 16);
			this.lstAvailableItems.Name = "lstAvailableItems";
			this.lstAvailableItems.Size = new System.Drawing.Size(218, 220);
			this.lstAvailableItems.Sorted = true;
			this.lstAvailableItems.TabIndex = 2;
			this.lstAvailableItems.SelectedIndexChanged += new System.EventHandler(this.lstAvailableItems_SelectionChanged);
			this.lstAvailableItems.DoubleClick += new System.EventHandler(this.lstAvailableItems_DoubleClick);
			// 
			// buttonRemoveCustomItem
			// 
			this.buttonRemoveCustomItem.Enabled = false;
			this.buttonRemoveCustomItem.Location = new System.Drawing.Point(233, 248);
			this.buttonRemoveCustomItem.Name = "buttonRemoveCustomItem";
			this.buttonRemoveCustomItem.Size = new System.Drawing.Size(118, 23);
			this.buttonRemoveCustomItem.TabIndex = 2;
			this.buttonRemoveCustomItem.Text = "Remove Custom Item";
			this.buttonRemoveCustomItem.UseVisualStyleBackColor = true;
			this.buttonRemoveCustomItem.Click += new System.EventHandler(this.buttonRemoveCustomItem_Click);
			// 
			// buttonAddCustomItem
			// 
			this.buttonAddCustomItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddCustomItem.Location = new System.Drawing.Point(109, 248);
			this.buttonAddCustomItem.Name = "buttonAddCustomItem";
			this.buttonAddCustomItem.Size = new System.Drawing.Size(118, 23);
			this.buttonAddCustomItem.TabIndex = 3;
			this.buttonAddCustomItem.Text = "Add Custom Item";
			this.buttonAddCustomItem.UseVisualStyleBackColor = true;
			this.buttonAddCustomItem.Click += new System.EventHandler(this.buttonAddCustomItem_Click);
			// 
			// groupBox3
			// 
			this.tableLayoutPanel3.SetColumnSpan(this.groupBox3, 2);
			this.groupBox3.Controls.Add(this.lstFilteredItems);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(233, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(226, 239);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Selected Items";
			// 
			// lstFilteredItems
			// 
			this.lstFilteredItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstFilteredItems.Enabled = false;
			this.lstFilteredItems.FormattingEnabled = true;
			this.lstFilteredItems.IntegralHeight = false;
			this.lstFilteredItems.Location = new System.Drawing.Point(3, 16);
			this.lstFilteredItems.Name = "lstFilteredItems";
			this.lstFilteredItems.Size = new System.Drawing.Size(220, 220);
			this.lstFilteredItems.Sorted = true;
			this.lstFilteredItems.TabIndex = 0;
			this.lstFilteredItems.SelectedIndexChanged += new System.EventHandler(this.lstFilteredItems_SelectionChanged);
			this.lstFilteredItems.DoubleClick += new System.EventHandler(this.lstFilteredItems_DoubleClick);
			this.lstFilteredItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFilteredItems_KeyDown);
			// 
			// buttonMoveAllToAvailable
			// 
			this.buttonMoveAllToAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonMoveAllToAvailable.Location = new System.Drawing.Point(384, 248);
			this.buttonMoveAllToAvailable.Name = "buttonMoveAllToAvailable";
			this.buttonMoveAllToAvailable.Size = new System.Drawing.Size(75, 23);
			this.buttonMoveAllToAvailable.TabIndex = 4;
			this.buttonMoveAllToAvailable.Text = "<-- Move All";
			this.buttonMoveAllToAvailable.UseVisualStyleBackColor = true;
			this.buttonMoveAllToAvailable.Click += new System.EventHandler(this.buttonMoveAllToAvailable_Click);
			// 
			// checkBoxFilterChests
			// 
			this.checkBoxFilterChests.AutoSize = true;
			this.checkBoxFilterChests.Location = new System.Drawing.Point(6, 8);
			this.checkBoxFilterChests.Name = "checkBoxFilterChests";
			this.checkBoxFilterChests.Size = new System.Drawing.Size(412, 17);
			this.checkBoxFilterChests.TabIndex = 1;
			this.checkBoxFilterChests.Text = "Only show Chests that contain one or more of the following weapons/accessories:";
			this.checkBoxFilterChests.UseVisualStyleBackColor = true;
			this.checkBoxFilterChests.CheckedChanged += new System.EventHandler(this.checkBoxFilterChests_CheckedChanged);
			// 
			// tabPageWorldInformation
			// 
			this.tabPageWorldInformation.Controls.Add(this.tableLayoutPanel1);
			this.tabPageWorldInformation.Location = new System.Drawing.Point(4, 22);
			this.tabPageWorldInformation.Name = "tabPageWorldInformation";
			this.tabPageWorldInformation.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageWorldInformation.Size = new System.Drawing.Size(474, 312);
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
			this.tableLayoutPanel1.Controls.Add(this.groupBox5, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.buttonLoadInformation, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 306);
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
			this.tableLayoutPanel1.SetRowSpan(this.worldInfoGroupBox, 2);
			this.worldInfoGroupBox.Size = new System.Drawing.Size(228, 251);
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
			this.worldPropertyGrid.Size = new System.Drawing.Size(222, 232);
			this.worldPropertyGrid.TabIndex = 0;
			this.worldPropertyGrid.ToolbarVisible = false;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.treeViewChestInformation);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Location = new System.Drawing.Point(237, 3);
			this.groupBox4.Name = "groupBox4";
			this.tableLayoutPanel1.SetRowSpan(this.groupBox4, 2);
			this.groupBox4.Size = new System.Drawing.Size(228, 251);
			this.groupBox4.TabIndex = 5;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Chests";
			// 
			// treeViewChestInformation
			// 
			this.treeViewChestInformation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewChestInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeViewChestInformation.Location = new System.Drawing.Point(3, 16);
			this.treeViewChestInformation.Name = "treeViewChestInformation";
			this.treeViewChestInformation.Size = new System.Drawing.Size(222, 232);
			this.treeViewChestInformation.TabIndex = 3;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.radioButtonSortByX);
			this.groupBox5.Controls.Add(this.radioButtonSortByY);
			this.groupBox5.Controls.Add(this.radioButtonSortByNone);
			this.groupBox5.Location = new System.Drawing.Point(237, 260);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(224, 43);
			this.groupBox5.TabIndex = 12;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Sort Type";
			// 
			// radioButtonSortByX
			// 
			this.radioButtonSortByX.AutoSize = true;
			this.radioButtonSortByX.Location = new System.Drawing.Point(63, 18);
			this.radioButtonSortByX.Name = "radioButtonSortByX";
			this.radioButtonSortByX.Size = new System.Drawing.Size(32, 17);
			this.radioButtonSortByX.TabIndex = 3;
			this.radioButtonSortByX.TabStop = true;
			this.radioButtonSortByX.Text = "X";
			this.radioButtonSortByX.UseVisualStyleBackColor = true;
			this.radioButtonSortByX.CheckedChanged += new System.EventHandler(this.radioButtonSortByX_CheckedChanged);
			// 
			// radioButtonSortByY
			// 
			this.radioButtonSortByY.AutoSize = true;
			this.radioButtonSortByY.Location = new System.Drawing.Point(101, 18);
			this.radioButtonSortByY.Name = "radioButtonSortByY";
			this.radioButtonSortByY.Size = new System.Drawing.Size(32, 17);
			this.radioButtonSortByY.TabIndex = 2;
			this.radioButtonSortByY.TabStop = true;
			this.radioButtonSortByY.Text = "Y";
			this.radioButtonSortByY.UseVisualStyleBackColor = true;
			this.radioButtonSortByY.CheckedChanged += new System.EventHandler(this.radioButtonSortByY_CheckedChanged);
			// 
			// radioButtonSortByNone
			// 
			this.radioButtonSortByNone.AutoSize = true;
			this.radioButtonSortByNone.Location = new System.Drawing.Point(6, 18);
			this.radioButtonSortByNone.Name = "radioButtonSortByNone";
			this.radioButtonSortByNone.Size = new System.Drawing.Size(51, 17);
			this.radioButtonSortByNone.TabIndex = 1;
			this.radioButtonSortByNone.TabStop = true;
			this.radioButtonSortByNone.Text = "None";
			this.radioButtonSortByNone.UseVisualStyleBackColor = true;
			this.radioButtonSortByNone.CheckedChanged += new System.EventHandler(this.radioButtonSortByNone_CheckedChanged);
			// 
			// buttonLoadInformation
			// 
			this.buttonLoadInformation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonLoadInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonLoadInformation.Location = new System.Drawing.Point(3, 260);
			this.buttonLoadInformation.Name = "buttonLoadInformation";
			this.buttonLoadInformation.Size = new System.Drawing.Size(228, 43);
			this.buttonLoadInformation.TabIndex = 11;
			this.buttonLoadInformation.Text = "Load Information";
			this.buttonLoadInformation.UseVisualStyleBackColor = true;
			this.buttonLoadInformation.Click += new System.EventHandler(this.buttonLoadInformation_Click);
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
			this.tabPageAbout.Size = new System.Drawing.Size(474, 312);
			this.tabPageAbout.TabIndex = 4;
			this.tabPageAbout.Text = "About";
			this.tabPageAbout.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(248, 85);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "by: fperks, noroom, solsund";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(165, 118);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Homepage:";
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(165, 85);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(72, 13);
			this.lblVersion.TabIndex = 13;
			this.lblVersion.Text = "Version: 8.8.8";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(158, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(249, 55);
			this.label1.TabIndex = 12;
			this.label1.Text = "MoreTerra";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 128);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 11;
			this.pictureBox1.TabStop = false;
			// 
			// linkLabelHomepage
			// 
			this.linkLabelHomepage.AutoSize = true;
			this.linkLabelHomepage.Location = new System.Drawing.Point(264, 118);
			this.linkLabelHomepage.Name = "linkLabelHomepage";
			this.linkLabelHomepage.Size = new System.Drawing.Size(120, 13);
			this.linkLabelHomepage.TabIndex = 10;
			this.linkLabelHomepage.TabStop = true;
			this.linkLabelHomepage.Text = "moreterra.codeplex.com";
			this.linkLabelHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHomepage_LinkClicked);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.labelSpecialThanks);
			this.groupBox1.Location = new System.Drawing.Point(3, 207);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(468, 102);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Special Thanks To:";
			// 
			// labelSpecialThanks
			// 
			this.labelSpecialThanks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelSpecialThanks.Location = new System.Drawing.Point(3, 16);
			this.labelSpecialThanks.Name = "labelSpecialThanks";
			this.labelSpecialThanks.Size = new System.Drawing.Size(462, 83);
			this.labelSpecialThanks.TabIndex = 0;
			this.labelSpecialThanks.Text = "SpecialThanks";
			// 
			// groupBoxSelectWorld
			// 
			this.groupBoxSelectWorld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxSelectWorld.Controls.Add(this.labelWorldName);
			this.groupBoxSelectWorld.Controls.Add(this.comboBoxWorldFilePath);
			this.groupBoxSelectWorld.Controls.Add(this.buttonBrowseWorld);
			this.groupBoxSelectWorld.Location = new System.Drawing.Point(6, 6);
			this.groupBoxSelectWorld.Name = "groupBoxSelectWorld";
			this.groupBoxSelectWorld.Size = new System.Drawing.Size(482, 96);
			this.groupBoxSelectWorld.TabIndex = 6;
			this.groupBoxSelectWorld.TabStop = false;
			this.groupBoxSelectWorld.Text = "Select World";
			// 
			// labelWorldName
			// 
			this.labelWorldName.AutoSize = true;
			this.labelWorldName.Location = new System.Drawing.Point(18, 66);
			this.labelWorldName.Name = "labelWorldName";
			this.labelWorldName.Size = new System.Drawing.Size(85, 13);
			this.labelWorldName.TabIndex = 4;
			this.labelWorldName.Text = "labelWorldName";
			// 
			// comboBoxWorldFilePath
			// 
			this.comboBoxWorldFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxWorldFilePath.FormattingEnabled = true;
			this.comboBoxWorldFilePath.Location = new System.Drawing.Point(16, 32);
			this.comboBoxWorldFilePath.Name = "comboBoxWorldFilePath";
			this.comboBoxWorldFilePath.Size = new System.Drawing.Size(450, 21);
			this.comboBoxWorldFilePath.TabIndex = 3;
			this.comboBoxWorldFilePath.TextChanged += new System.EventHandler(this.comboBoxWorldFilePath_TextChanged);
			// 
			// buttonBrowseWorld
			// 
			this.buttonBrowseWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseWorld.Location = new System.Drawing.Point(348, 59);
			this.buttonBrowseWorld.Name = "buttonBrowseWorld";
			this.buttonBrowseWorld.Size = new System.Drawing.Size(118, 28);
			this.buttonBrowseWorld.TabIndex = 2;
			this.buttonBrowseWorld.Text = "Browse...";
			this.buttonBrowseWorld.UseVisualStyleBackColor = true;
			this.buttonBrowseWorld.Click += new System.EventHandler(this.buttonBrowseWorld_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.comboBoxDrawUsing);
			this.groupBox6.Controls.Add(this.button2);
			this.groupBox6.Controls.Add(this.button1);
			this.groupBox6.Location = new System.Drawing.Point(250, 188);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(218, 73);
			this.groupBox6.TabIndex = 13;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Unsupported World Version Helper";
			this.groupBox6.Visible = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(6, 46);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(87, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Scan for Tiles";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(120, 46);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(92, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Save as Default";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// comboBoxDrawUsing
			// 
			this.comboBoxDrawUsing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDrawUsing.FormattingEnabled = true;
			this.comboBoxDrawUsing.Location = new System.Drawing.Point(6, 19);
			this.comboBoxDrawUsing.Name = "comboBoxDrawUsing";
			this.comboBoxDrawUsing.Size = new System.Drawing.Size(206, 21);
			this.comboBoxDrawUsing.TabIndex = 2;
			// 
			// FormWorldView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 452);
			this.Controls.Add(this.groupBoxSelectWorld);
			this.Controls.Add(this.tabControlSettings);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(502, 486);
			this.Name = "FormWorldView";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Text = "MoreTerra (TerrariaWorldViewer)";
			this.Load += new System.EventHandler(this.WorldViewForm_Load);
			this.tabControlSettings.ResumeLayout(false);
			this.tabPageDrawWorld.ResumeLayout(false);
			this.tabPageDrawWorld.PerformLayout();
			this.groupBoxImageOutput.ResumeLayout(false);
			this.groupBoxImageOutput.PerformLayout();
			this.tabPageSettings.ResumeLayout(false);
			this.groupBoxSymbols.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.contextMenuStripListOperations.ResumeLayout(false);
			this.tabPageChestFinder.ResumeLayout(false);
			this.tabPageChestFinder.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tabPageWorldInformation.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.worldInfoGroupBox.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.tabPageAbout.ResumeLayout(false);
			this.tabPageAbout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBoxSelectWorld.ResumeLayout(false);
			this.groupBoxSelectWorld.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageDrawWorld;
        private System.Windows.Forms.TabPage tabPageWorldInformation;
        private System.Windows.Forms.GroupBox groupBoxImageOutput;
        private System.Windows.Forms.TextBox textBoxOutputFile;
        private System.Windows.Forms.Button buttonBrowseOutput;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelSpecialThanks;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox worldInfoGroupBox;
        private System.Windows.Forms.PropertyGrid worldPropertyGrid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TreeView treeViewChestInformation;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelHomepage;
        private System.Windows.Forms.Button buttonDrawWorld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxOpenImage;
        private System.Windows.Forms.Button buttonLoadInformation;
        private System.Windows.Forms.GroupBox groupBoxSymbols;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.CheckBox checkBoxDrawWalls;
        private System.Windows.Forms.TabPage tabPageChestFinder;
        private System.Windows.Forms.GroupBox groupBoxSelectWorld;
        private System.Windows.Forms.ComboBox comboBoxWorldFilePath;
        private System.Windows.Forms.Button buttonBrowseWorld;
        private System.Windows.Forms.CheckBox checkBoxFilterChests;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox lstAvailableItems;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ListBox lstFilteredItems;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListOperations;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectNoneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem invertSelectionToolStripMenuItem;
		private System.Windows.Forms.TreeView treeViewMarkerList;
		private System.Windows.Forms.Label labelWorldName;
		private System.Windows.Forms.Button buttonCustomResources;
		private System.Windows.Forms.Label labelCustomResources;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton radioButtonSortByNone;
		private System.Windows.Forms.RadioButton radioButtonSortByX;
		private System.Windows.Forms.RadioButton radioButtonSortByY;
		private System.Windows.Forms.Button buttonAddCustomItem;
		private System.Windows.Forms.Button buttonRemoveCustomItem;
		private System.Windows.Forms.Button buttonMoveAllToFiltered;
		private System.Windows.Forms.Button buttonMoveAllToAvailable;
		private System.Windows.Forms.CheckBox checkBoxScanForItems;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.ComboBox comboBoxDrawUsing;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;


    }
}