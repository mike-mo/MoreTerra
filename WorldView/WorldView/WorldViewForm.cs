using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace WorldView
{
    public partial class WorldViewForm : Form
    {
        private WorldMapper mapper = null;

        public WorldViewForm()
        {
            mapper = new WorldMapper();
            mapper.Initialize();
            InitializeComponent();
            checkBoxUseSymbols.Enabled = false;
            checkBoxDrawWalls.Enabled = false;
            labelSpecialThanks.Text = Constants.Credits;

            this.checkBoxUseChestFilter.Checked = SettingsManager.Instance.IsChestFilterEnabled;

            // Populate Symbol Properties
            Dictionary<string, bool> symbolStates = SettingsManager.Instance.SymbolStates;
            foreach (KeyValuePair<string, bool> kvp in symbolStates)
            {
                this.checkedListBoxSymbols.Items.Add(kvp.Key, kvp.Value);
            }

            foreach (KeyValuePair<string, bool> kvp in SettingsManager.Instance.FilterAccessoryStates)
            {
                this.checkedListBoxChestFilterAccessories.Items.Add(kvp.Key, kvp.Value);
            }


            foreach (KeyValuePair<string, bool> kvp in SettingsManager.Instance.FilterWeaponStates)
            {
                this.checkedListBoxChestFilterWeapons.Items.Add(kvp.Key, kvp.Value);
            }

            if (!this.checkBoxUseChestFilter.Checked)
            {
                this.checkedListBoxChestFilterAccessories.Enabled = false;
                this.checkedListBoxChestFilterWeapons.Enabled = false;
            }

            

            // Wiring
            this.checkedListBoxSymbols.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxSymbols_ItemCheck);
            this.checkedListBoxChestFilterWeapons.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxChestFilterWeapons_ItemCheck);
            this.checkedListBoxChestFilterAccessories.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxChestFilterAccessories_ItemCheck);
            this.checkBoxUseChestFilter.CheckedChanged += new System.EventHandler(this.checkBoxUseChestFilter_CheckedChanged);
            this.browseForWorldButton.Click += new System.EventHandler(this.WorldViewForm_OpenWorldClicked);
            this.checkBoxUseSymbols.CheckedChanged += new System.EventHandler(this.checkBoxUseSymbols_CheckedChanged);
            this.outputFileBrowseButton.Click += new System.EventHandler(this.outputFileBrowseButton_Click);
            this.outputFileConfirmButton.Click += new System.EventHandler(this.outputFileConfirmButton_Click);
        }

        

        private void WorldViewForm_Load(object sender, EventArgs e)
        {

        }

        private void WorldViewForm_OpenWorldClicked(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Terraria World (*.wld)|*.wld|Terraria Backup World (*.wld.bak)|*.wld.bak";
            dialog.Title = "Select World File";
            dialog.InitialDirectory = SettingsManager.Instance.InputWorldDirectory;
            string filePath = (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : string.Empty;

            if (filePath == string.Empty)
            {
                return;
            }
            this.worldFilePathTextBox.Text = filePath;
            SettingsManager.Instance.InputWorldDirectory = Path.GetDirectoryName(filePath);
            try
            {
                mapper.OpenWorld(filePath);
                PopulateTree();
                outputFileBrowseButton.Enabled = true;
                outputFileConfirmButton.Enabled = false;
                checkBoxUseSymbols.Enabled = true;
                checkBoxDrawWalls.Enabled = true;
                outputFileConfirmButton.Text = "Create";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PopulateTree()
        {
            worldPropertyGrid.SelectedObject = mapper.Header;
        }

        private void PopulateChestTree()
        {
            List<Chest> chests = this.mapper.Chests;
            TreeNodeCollection nodes = this.treeViewChestInformation.Nodes;
            foreach (Chest c in chests)
            {
                TreeNode node = new TreeNode(string.Format("Chest #:{0}", c.ChestId));
                node.Nodes.Add(string.Format("Coordinates: {0}", c.Coordinates));
                TreeNode itemNode = new TreeNode("Items");
                foreach (Item i in c.Items)
                {
                    itemNode.Nodes.Add(i.ToString());
                }
                node.Nodes.Add(itemNode);
                nodes.Add(node);
            }

        }

        private void outputFileBrowseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG (*.png)|*.png";
            dialog.Title = "Select World File";
            dialog.InitialDirectory = SettingsManager.Instance.OutputPreviewDirectory;
            dialog.FileName = string.Format("{0}.png", System.IO.Path.GetFileNameWithoutExtension(this.worldFilePathTextBox.Text));
            string filePath = (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : string.Empty;
            if (filePath == string.Empty)
            {
                return;
            }
            else if (new System.IO.FileInfo(filePath).Extension.ToUpper() != ".PNG")
            {
                MessageBox.Show("Output File MUST End in .png", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SettingsManager.Instance.OutputPreviewDirectory = Path.GetDirectoryName(filePath);
            outputFileTextBox.Text = filePath;
            outputFileConfirmButton.Enabled = true;
        }

        private void outputFileConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                outputFileConfirmButton.Enabled = false;
                outputFileConfirmButton.Text = "Writing Image File...";
                mapper.CreatePreviewPNG(outputFileTextBox.Text, checkBoxDrawWalls.Checked, checkBoxUseSymbols.Checked, progressBarOutputPreview);
                outputFileConfirmButton.Text = "File Written!";
                outputFileBrowseButton.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.mapper.CloseWorld();
            this.PopulateChestTree();

            outputFileTextBox.Text = string.Empty;
            worldFilePathTextBox.Text = string.Empty;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkedListBoxSymbols_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBoxSymbols_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.ToggleSymbolVisibility(checkedListBoxSymbols.GetItemText(checkedListBoxSymbols.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        private void checkedListBoxChestFilterWeapons_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.ToggleFilterWeapon(checkedListBoxChestFilterWeapons.GetItemText(checkedListBoxChestFilterWeapons.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        private void checkedListBoxChestFilterAccessories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.ToggleFilterAccessories(checkedListBoxChestFilterAccessories.GetItemText(checkedListBoxChestFilterAccessories.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        private void checkBoxUseChestFilter_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.Instance.IsChestFilterEnabled = this.checkBoxUseChestFilter.Checked;
            this.checkedListBoxChestFilterAccessories.Enabled = this.checkBoxUseChestFilter.Checked;
            this.checkedListBoxChestFilterWeapons.Enabled = this.checkBoxUseChestFilter.Checked;
        }

        private void checkBoxUseSymbols_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.Instance.IsSymbolsDrawable = this.checkBoxUseSymbols.Checked;
        }



    }
}