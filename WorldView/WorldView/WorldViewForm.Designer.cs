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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.selectWorldGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.browseForWorldButton = new System.Windows.Forms.Button();
            this.worldFilePathTextBox = new System.Windows.Forms.TextBox();
            this.outputWorldGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.outputFileTextBox = new System.Windows.Forms.TextBox();
            this.outputFileBrowseButton = new System.Windows.Forms.Button();
            this.outputFileConfirmButton = new System.Windows.Forms.Button();
            this.progressBarOutputPreview = new System.Windows.Forms.ProgressBar();
            this.previewPropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxUseSymbols = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawWalls = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSpecialThanks = new System.Windows.Forms.Label();
            this.worldInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.worldPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.tabPageWorldInformation = new System.Windows.Forms.TabPage();
            this.tabPageChestInformation = new System.Windows.Forms.TabPage();
            this.treeViewChestInformation = new System.Windows.Forms.TreeView();
            this.tabPageAdvancedSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSymbols = new System.Windows.Forms.GroupBox();
            this.checkedListBoxSymbols = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.checkedListBoxChestFilterWeapons = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxChestFilterAccessories = new System.Windows.Forms.CheckedListBox();
            this.checkBoxUseChestFilter = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.selectWorldGroupBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.outputWorldGroupBox.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.previewPropertiesGroupBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.worldInfoGroupBox.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            this.tabPageWorldInformation.SuspendLayout();
            this.tabPageChestInformation.SuspendLayout();
            this.tabPageAdvancedSettings.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBoxSymbols.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.selectWorldGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.outputWorldGroupBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.previewPropertiesGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(474, 434);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // selectWorldGroupBox
            // 
            this.selectWorldGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.selectWorldGroupBox.Controls.Add(this.tableLayoutPanel2);
            this.selectWorldGroupBox.Location = new System.Drawing.Point(6, 6);
            this.selectWorldGroupBox.Name = "selectWorldGroupBox";
            this.selectWorldGroupBox.Size = new System.Drawing.Size(462, 54);
            this.selectWorldGroupBox.TabIndex = 0;
            this.selectWorldGroupBox.TabStop = false;
            this.selectWorldGroupBox.Text = "Select World";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.10714F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.89286F));
            this.tableLayoutPanel2.Controls.Add(this.browseForWorldButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.worldFilePathTextBox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(456, 35);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // browseForWorldButton
            // 
            this.browseForWorldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.browseForWorldButton.Location = new System.Drawing.Point(363, 6);
            this.browseForWorldButton.Name = "browseForWorldButton";
            this.browseForWorldButton.Size = new System.Drawing.Size(90, 23);
            this.browseForWorldButton.TabIndex = 0;
            this.browseForWorldButton.Text = "Browse";
            this.browseForWorldButton.UseVisualStyleBackColor = true;
            // 
            // worldFilePathTextBox
            // 
            this.worldFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.worldFilePathTextBox.Location = new System.Drawing.Point(3, 7);
            this.worldFilePathTextBox.Name = "worldFilePathTextBox";
            this.worldFilePathTextBox.ReadOnly = true;
            this.worldFilePathTextBox.Size = new System.Drawing.Size(354, 20);
            this.worldFilePathTextBox.TabIndex = 1;
            // 
            // outputWorldGroupBox
            // 
            this.outputWorldGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.outputWorldGroupBox.Controls.Add(this.tableLayoutPanel3);
            this.outputWorldGroupBox.Location = new System.Drawing.Point(6, 117);
            this.outputWorldGroupBox.Name = "outputWorldGroupBox";
            this.outputWorldGroupBox.Size = new System.Drawing.Size(462, 143);
            this.outputWorldGroupBox.TabIndex = 2;
            this.outputWorldGroupBox.TabStop = false;
            this.outputWorldGroupBox.Text = "Output World Preview";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.96428F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.03572F));
            this.tableLayoutPanel3.Controls.Add(this.outputFileTextBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.outputFileBrowseButton, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.outputFileConfirmButton, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.progressBarOutputPreview, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.63107F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.36893F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(456, 124);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // outputFileTextBox
            // 
            this.outputFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFileTextBox.Location = new System.Drawing.Point(3, 13);
            this.outputFileTextBox.Name = "outputFileTextBox";
            this.outputFileTextBox.ReadOnly = true;
            this.outputFileTextBox.Size = new System.Drawing.Size(344, 20);
            this.outputFileTextBox.TabIndex = 0;
            // 
            // outputFileBrowseButton
            // 
            this.outputFileBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFileBrowseButton.Enabled = false;
            this.outputFileBrowseButton.Location = new System.Drawing.Point(353, 12);
            this.outputFileBrowseButton.Name = "outputFileBrowseButton";
            this.outputFileBrowseButton.Size = new System.Drawing.Size(100, 23);
            this.outputFileBrowseButton.TabIndex = 1;
            this.outputFileBrowseButton.Text = "Browse";
            this.outputFileBrowseButton.UseVisualStyleBackColor = true;
            // 
            // outputFileConfirmButton
            // 
            this.outputFileConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.SetColumnSpan(this.outputFileConfirmButton, 2);
            this.outputFileConfirmButton.Enabled = false;
            this.outputFileConfirmButton.Location = new System.Drawing.Point(3, 63);
            this.outputFileConfirmButton.Name = "outputFileConfirmButton";
            this.outputFileConfirmButton.Size = new System.Drawing.Size(450, 23);
            this.outputFileConfirmButton.TabIndex = 2;
            this.outputFileConfirmButton.Text = "Create Preview";
            this.outputFileConfirmButton.UseVisualStyleBackColor = true;
            // 
            // progressBarOutputPreview
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.progressBarOutputPreview, 2);
            this.progressBarOutputPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarOutputPreview.Location = new System.Drawing.Point(3, 106);
            this.progressBarOutputPreview.Name = "progressBarOutputPreview";
            this.progressBarOutputPreview.Size = new System.Drawing.Size(450, 15);
            this.progressBarOutputPreview.TabIndex = 3;
            // 
            // previewPropertiesGroupBox
            // 
            this.previewPropertiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewPropertiesGroupBox.Controls.Add(this.flowLayoutPanel1);
            this.previewPropertiesGroupBox.Location = new System.Drawing.Point(6, 66);
            this.previewPropertiesGroupBox.Name = "previewPropertiesGroupBox";
            this.previewPropertiesGroupBox.Size = new System.Drawing.Size(462, 45);
            this.previewPropertiesGroupBox.TabIndex = 3;
            this.previewPropertiesGroupBox.TabStop = false;
            this.previewPropertiesGroupBox.Text = "Preview Properties";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.checkBoxUseSymbols);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxDrawWalls);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(456, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // checkBoxUseSymbols
            // 
            this.checkBoxUseSymbols.AutoSize = true;
            this.checkBoxUseSymbols.Checked = true;
            this.checkBoxUseSymbols.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseSymbols.Location = new System.Drawing.Point(3, 3);
            this.checkBoxUseSymbols.Name = "checkBoxUseSymbols";
            this.checkBoxUseSymbols.Size = new System.Drawing.Size(87, 17);
            this.checkBoxUseSymbols.TabIndex = 1;
            this.checkBoxUseSymbols.Text = "Use Symbols";
            this.checkBoxUseSymbols.UseVisualStyleBackColor = true;
            // 
            // checkBoxDrawWalls
            // 
            this.checkBoxDrawWalls.AutoSize = true;
            this.checkBoxDrawWalls.Checked = true;
            this.checkBoxDrawWalls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrawWalls.Location = new System.Drawing.Point(96, 3);
            this.checkBoxDrawWalls.Name = "checkBoxDrawWalls";
            this.checkBoxDrawWalls.Size = new System.Drawing.Size(80, 17);
            this.checkBoxDrawWalls.TabIndex = 2;
            this.checkBoxDrawWalls.Text = "Draw Walls";
            this.checkBoxDrawWalls.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelSpecialThanks);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(6, 266);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 162);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Special Thanks To:";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // labelSpecialThanks
            // 
            this.labelSpecialThanks.AutoSize = true;
            this.labelSpecialThanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSpecialThanks.Location = new System.Drawing.Point(3, 16);
            this.labelSpecialThanks.Name = "labelSpecialThanks";
            this.labelSpecialThanks.Size = new System.Drawing.Size(78, 13);
            this.labelSpecialThanks.TabIndex = 0;
            this.labelSpecialThanks.Text = "SpecialThanks";
            // 
            // worldInfoGroupBox
            // 
            this.worldInfoGroupBox.Controls.Add(this.worldPropertyGrid);
            this.worldInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldInfoGroupBox.Location = new System.Drawing.Point(3, 3);
            this.worldInfoGroupBox.Name = "worldInfoGroupBox";
            this.worldInfoGroupBox.Size = new System.Drawing.Size(474, 434);
            this.worldInfoGroupBox.TabIndex = 1;
            this.worldInfoGroupBox.TabStop = false;
            this.worldInfoGroupBox.Text = "World Information";
            // 
            // worldPropertyGrid
            // 
            this.worldPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldPropertyGrid.Location = new System.Drawing.Point(3, 16);
            this.worldPropertyGrid.Name = "worldPropertyGrid";
            this.worldPropertyGrid.Size = new System.Drawing.Size(468, 415);
            this.worldPropertyGrid.TabIndex = 0;
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabPagePreview);
            this.tabControlSettings.Controls.Add(this.tabPageWorldInformation);
            this.tabControlSettings.Controls.Add(this.tabPageChestInformation);
            this.tabControlSettings.Controls.Add(this.tabPageAdvancedSettings);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSettings.Location = new System.Drawing.Point(3, 3);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(488, 466);
            this.tabControlSettings.TabIndex = 1;
            // 
            // tabPagePreview
            // 
            this.tabPagePreview.Controls.Add(this.tableLayoutPanel1);
            this.tabPagePreview.Location = new System.Drawing.Point(4, 22);
            this.tabPagePreview.Name = "tabPagePreview";
            this.tabPagePreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePreview.Size = new System.Drawing.Size(480, 440);
            this.tabPagePreview.TabIndex = 0;
            this.tabPagePreview.Text = "Main";
            this.tabPagePreview.UseVisualStyleBackColor = true;
            // 
            // tabPageWorldInformation
            // 
            this.tabPageWorldInformation.Controls.Add(this.worldInfoGroupBox);
            this.tabPageWorldInformation.Location = new System.Drawing.Point(4, 22);
            this.tabPageWorldInformation.Name = "tabPageWorldInformation";
            this.tabPageWorldInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWorldInformation.Size = new System.Drawing.Size(480, 440);
            this.tabPageWorldInformation.TabIndex = 1;
            this.tabPageWorldInformation.Text = "World Information";
            this.tabPageWorldInformation.UseVisualStyleBackColor = true;
            // 
            // tabPageChestInformation
            // 
            this.tabPageChestInformation.Controls.Add(this.treeViewChestInformation);
            this.tabPageChestInformation.Location = new System.Drawing.Point(4, 22);
            this.tabPageChestInformation.Name = "tabPageChestInformation";
            this.tabPageChestInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChestInformation.Size = new System.Drawing.Size(480, 440);
            this.tabPageChestInformation.TabIndex = 3;
            this.tabPageChestInformation.Text = "Chest Information";
            this.tabPageChestInformation.UseVisualStyleBackColor = true;
            // 
            // treeViewChestInformation
            // 
            this.treeViewChestInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewChestInformation.Location = new System.Drawing.Point(3, 3);
            this.treeViewChestInformation.Name = "treeViewChestInformation";
            this.treeViewChestInformation.Size = new System.Drawing.Size(474, 434);
            this.treeViewChestInformation.TabIndex = 0;
            // 
            // tabPageAdvancedSettings
            // 
            this.tabPageAdvancedSettings.Controls.Add(this.tableLayoutPanel4);
            this.tabPageAdvancedSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvancedSettings.Name = "tabPageAdvancedSettings";
            this.tabPageAdvancedSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdvancedSettings.Size = new System.Drawing.Size(480, 440);
            this.tabPageAdvancedSettings.TabIndex = 2;
            this.tabPageAdvancedSettings.Text = "Advanced Settings";
            this.tabPageAdvancedSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.groupBoxSymbols, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.16129F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.83871F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(474, 434);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // groupBoxSymbols
            // 
            this.groupBoxSymbols.Controls.Add(this.checkedListBoxSymbols);
            this.groupBoxSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymbols.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSymbols.Name = "groupBoxSymbols";
            this.groupBoxSymbols.Size = new System.Drawing.Size(468, 189);
            this.groupBoxSymbols.TabIndex = 0;
            this.groupBoxSymbols.TabStop = false;
            this.groupBoxSymbols.Text = "Symbols";
            // 
            // checkedListBoxSymbols
            // 
            this.checkedListBoxSymbols.CheckOnClick = true;
            this.checkedListBoxSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxSymbols.FormattingEnabled = true;
            this.checkedListBoxSymbols.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxSymbols.Name = "checkedListBoxSymbols";
            this.checkedListBoxSymbols.Size = new System.Drawing.Size(462, 170);
            this.checkedListBoxSymbols.TabIndex = 0;
            this.checkedListBoxSymbols.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxSymbols_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(468, 233);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Show Chests Containing Atleast One of the Following";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.checkedListBoxChestFilterWeapons, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.checkedListBoxChestFilterAccessories, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.checkBoxUseChestFilter, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(462, 214);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // checkedListBoxChestFilterWeapons
            // 
            this.checkedListBoxChestFilterWeapons.CheckOnClick = true;
            this.checkedListBoxChestFilterWeapons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxChestFilterWeapons.FormattingEnabled = true;
            this.checkedListBoxChestFilterWeapons.Location = new System.Drawing.Point(3, 3);
            this.checkedListBoxChestFilterWeapons.Name = "checkedListBoxChestFilterWeapons";
            this.checkedListBoxChestFilterWeapons.Size = new System.Drawing.Size(148, 208);
            this.checkedListBoxChestFilterWeapons.TabIndex = 0;
            // 
            // checkedListBoxChestFilterAccessories
            // 
            this.checkedListBoxChestFilterAccessories.CheckOnClick = true;
            this.checkedListBoxChestFilterAccessories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxChestFilterAccessories.FormattingEnabled = true;
            this.checkedListBoxChestFilterAccessories.Location = new System.Drawing.Point(157, 3);
            this.checkedListBoxChestFilterAccessories.Name = "checkedListBoxChestFilterAccessories";
            this.checkedListBoxChestFilterAccessories.Size = new System.Drawing.Size(148, 208);
            this.checkedListBoxChestFilterAccessories.TabIndex = 1;
            // 
            // checkBoxUseChestFilter
            // 
            this.checkBoxUseChestFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxUseChestFilter.AutoSize = true;
            this.checkBoxUseChestFilter.Location = new System.Drawing.Point(332, 3);
            this.checkBoxUseChestFilter.Name = "checkBoxUseChestFilter";
            this.checkBoxUseChestFilter.Padding = new System.Windows.Forms.Padding(3);
            this.checkBoxUseChestFilter.Size = new System.Drawing.Size(106, 23);
            this.checkBoxUseChestFilter.TabIndex = 2;
            this.checkBoxUseChestFilter.Text = "Use Chest Filter";
            this.checkBoxUseChestFilter.UseVisualStyleBackColor = true;
            // 
            // WorldViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 472);
            this.Controls.Add(this.tabControlSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WorldViewForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Terraria World Viewer";
            this.Load += new System.EventHandler(this.WorldViewForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.selectWorldGroupBox.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.outputWorldGroupBox.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.previewPropertiesGroupBox.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.worldInfoGroupBox.ResumeLayout(false);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPagePreview.ResumeLayout(false);
            this.tabPageWorldInformation.ResumeLayout(false);
            this.tabPageChestInformation.ResumeLayout(false);
            this.tabPageAdvancedSettings.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBoxSymbols.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox selectWorldGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button browseForWorldButton;
        private System.Windows.Forms.TextBox worldFilePathTextBox;
        private System.Windows.Forms.GroupBox worldInfoGroupBox;
        private System.Windows.Forms.GroupBox outputWorldGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox outputFileTextBox;
        private System.Windows.Forms.Button outputFileBrowseButton;
        private System.Windows.Forms.Button outputFileConfirmButton;
        private System.Windows.Forms.PropertyGrid worldPropertyGrid;
        private System.Windows.Forms.GroupBox previewPropertiesGroupBox;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPagePreview;
        private System.Windows.Forms.TabPage tabPageWorldInformation;
        private System.Windows.Forms.ProgressBar progressBarOutputPreview;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxUseSymbols;
        private System.Windows.Forms.CheckBox checkBoxDrawWalls;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelSpecialThanks;
        private System.Windows.Forms.TabPage tabPageAdvancedSettings;
        private System.Windows.Forms.TabPage tabPageChestInformation;
        private System.Windows.Forms.TreeView treeViewChestInformation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBoxSymbols;
        private System.Windows.Forms.CheckedListBox checkedListBoxSymbols;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckedListBox checkedListBoxChestFilterWeapons;
        private System.Windows.Forms.CheckedListBox checkedListBoxChestFilterAccessories;
        private System.Windows.Forms.CheckBox checkBoxUseChestFilter;


    }
}