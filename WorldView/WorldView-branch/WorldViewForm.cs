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
        private BackgroundWorker worker = null;
        private Timer tmrProgressCheck = new Timer();
            

        public WorldViewForm()
        {            
            mapper = new WorldMapper();
            mapper.Initialize();
            InitializeComponent();
         
            labelSpecialThanks.Text = Constants.Credits;

            tmrProgressCheck.Tick += new System.EventHandler(tmrProgressCheck_Tick);
            tmrProgressCheck.Enabled = false;
            tmrProgressCheck.Interval = 333; //3 times per second

            // Populate Symbol Properties
            Dictionary<string, bool> symbolStates = SettingsManager.Instance.SymbolStates;
            foreach (KeyValuePair<string, bool> kvp in symbolStates)
            {
                this.checkedListBoxMarkers.Items.Add(kvp.Key, kvp.Value);
            }

            foreach (KeyValuePair<string, bool> kvp in SettingsManager.Instance.FilterAccessoryStates)
            {
                this.checkedListBoxChestFilterAccessories.Items.Add(kvp.Key, kvp.Value);
            }


            foreach (KeyValuePair<string, bool> kvp in SettingsManager.Instance.FilterWeaponStates)
            {
                this.checkedListBoxChestFilterWeapons.Items.Add(kvp.Key, kvp.Value);            
            }
                        

            // Register the event handlers
            this.checkedListBoxMarkers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxMarkers_ItemCheck);
            this.checkedListBoxChestFilterWeapons.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxChestFilterWeapons_ItemCheck);
            this.checkedListBoxChestFilterAccessories.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxChestFilterAccessories_ItemCheck);
            
        }

        private void buttonBrowseWorld_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Terraria World (*.wld)|*.wld|Terraria Backup World (*.wld.bak)|*.wld.bak";
            dialog.Title = "Select World File";
            dialog.InitialDirectory = SettingsManager.Instance.InputWorldDirectory;
            string filePath = (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : string.Empty;

            if (filePath == string.Empty) return;

            comboBoxWorldFilePath.Text = filePath;
            SettingsManager.Instance.InputWorldDirectory = Path.GetDirectoryName(filePath);

            try
            {
                mapper.OpenWorld(filePath);
                PopulateWorldTree();
                //PopulateChestTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Opening World", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PopulateWorldTree()
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

                foreach (Item i in c.Items) itemNode.Nodes.Add(i.ToString());
                itemNode.Expand();

                node.Nodes.Add(itemNode);
                nodes.Add(node);
            }

        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG (*.png)|*.png";
            dialog.Title = "Select World File";
            dialog.InitialDirectory = SettingsManager.Instance.OutputPreviewDirectory;

            if (comboBoxWorldFilePath.Text != string.Empty)
            {
                dialog.FileName = string.Format("{0}.png", System.IO.Path.GetFileNameWithoutExtension(comboBoxWorldFilePath.Text));
            }
                             
            if (dialog.ShowDialog() != DialogResult.OK) return;

            SettingsManager.Instance.OutputPreviewDirectory = Path.GetDirectoryName(dialog.FileName);
            textBoxOutputFile.Text = dialog.FileName;
            
        }

        private void buttonDrawWorld_Click(object sender, EventArgs e)
        {
            if (comboBoxWorldFilePath.Text == string.Empty || textBoxOutputFile.Text == string.Empty)
            {
                MessageBox.Show("Please enter a path to your World file and a desired output image path!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else if (!File.Exists(comboBoxWorldFilePath.Text))  
            {
                MessageBox.Show("The World file could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            buttonDrawWorld.Enabled = false;

            groupBoxSelectWorld.Enabled = false;
            groupBoxImageOutput.Enabled = false;
            groupBoxOptions.Enabled = false;

            labelStatus.Text = "Drawing World...";
            progressBarDrawWorld.Visible = true;

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();

            tmrProgressCheck.Enabled = true;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            mapper.CreatePreviewPNG(textBoxOutputFile.Text, checkBoxDrawWalls.Checked, checkBoxMarkers.Checked);     
            mapper.CloseWorld();     
        }

        private void tmrProgressCheck_Tick(object sender, EventArgs e)
        {
            progressBarDrawWorld.Value = mapper.progress;
        
            labelPercent.Text = mapper.progress + "%";

            if (mapper.progress >= 100)
            {
                tmrProgressCheck.Enabled = false;

                PopulateChestTree();
                labelStatus.Text = "Ready";
                labelPercent.Text = string.Empty;
                progressBarDrawWorld.Value = 0;
                progressBarDrawWorld.Visible = false;

                buttonDrawWorld.Enabled = true;

                groupBoxSelectWorld.Enabled = true;
                groupBoxImageOutput.Enabled = true;
                groupBoxOptions.Enabled = true;

                if (checkBoxOpenImage.Checked) System.Diagnostics.Process.Start(textBoxOutputFile.Text);
            }
        }

        private void checkedListBoxMarkers_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void checkedListBoxChestFilterWeapons_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.ToggleFilterWeapon(checkedListBoxChestFilterWeapons.GetItemText(checkedListBoxChestFilterWeapons.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        private void checkedListBoxChestFilterAccessories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.ToggleFilterAccessories(checkedListBoxChestFilterAccessories.GetItemText(checkedListBoxChestFilterAccessories.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        private void checkBoxMarkers_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.Instance.IsSymbolsDrawable = this.checkBoxMarkers.Checked;
        }

        private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControlSettings.SelectedTab = tabPageHelp;
        }

        private void linkLabelSelectAllWeapons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxChestFilterWeapons.Items.Count; i++)
            {
                checkedListBoxChestFilterWeapons.SetItemChecked(i, true);
            }
        }

        private void linkLabelSelectNoneWeapons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxChestFilterWeapons.Items.Count; i++)
            {
                checkedListBoxChestFilterWeapons.SetItemChecked(i, false);
            }
        }

        private void linkLabelInvertWeapons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxChestFilterWeapons.Items.Count; i++)
            {
                checkedListBoxChestFilterWeapons.SetItemChecked(i, !checkedListBoxChestFilterWeapons.GetItemChecked(i));
            }
        }

        private void linkLabelSelectAllAccessories_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxChestFilterAccessories.Items.Count; i++)
            {
                checkedListBoxChestFilterAccessories.SetItemChecked(i, true);
            }
        }

        private void linkLabelSelectNoneAccessories_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxChestFilterAccessories.Items.Count; i++)
            {
                checkedListBoxChestFilterAccessories.SetItemChecked(i, false);
            }
        }

        private void linkLabelInvertAccessories_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxChestFilterAccessories.Items.Count; i++)
            {
                checkedListBoxChestFilterAccessories.SetItemChecked(i, !checkedListBoxChestFilterAccessories.GetItemChecked(i));
            }
        }

     
          

    }
}