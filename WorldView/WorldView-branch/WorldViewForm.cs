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
using WorldView.Properties;

namespace WorldView
{
    public partial class WorldViewForm : Form
    {
        private delegate void PopulateWorldTreeDelegate();
        private delegate void PopulateChestTreeDelegate();     

        private WorldMapper mapper = null;
        private BackgroundWorker mapperWorker = null;
        private Timer tmrMapperProgress = new Timer();
       

        private string worldPath = string.Empty;



        public WorldViewForm()
        {            
            InitializeComponent();
         
            labelSpecialThanks.Text = Constants.Credits;

            tmrMapperProgress.Tick += new System.EventHandler(tmrMapperProgress_Tick);
            tmrMapperProgress.Enabled = false;
            tmrMapperProgress.Interval = 333;             

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

        private void WorldViewForm_Load(object sender, EventArgs e)
        {
            string ver = Application.ProductVersion;

            while (ver.Length > 3 && ver.Substring(ver.Length - 2) == ".0")
            {
                ver = ver.Substring(0, ver.Length - 2);
            }

            lblVersion.Text = "Version: " + ver;

            if (Directory.Exists(SettingsManager.Instance.InputWorldDirectory))
            {
                foreach (string file in Directory.GetFiles(SettingsManager.Instance.InputWorldDirectory, "*.wld" ))
                {
                    comboBoxWorldFilePath.Items.Add(file);
                }

                if (comboBoxWorldFilePath.Items.Count > 0) comboBoxWorldFilePath.SelectedIndex = 0;
            }
        }

        private void comboBoxWorldFilePath_TextChanged(object sender, EventArgs e)
        {
            worldPath = comboBoxWorldFilePath.Text;

            if (File.Exists(comboBoxWorldFilePath.Text))
            {
                textBoxOutputFile.Text = Path.Combine( SettingsManager.Instance.OutputPreviewDirectory , Path.GetFileNameWithoutExtension(comboBoxWorldFilePath.Text) + ".png");
            }
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
            if (checkValidPaths(true))
            {
                buttonDrawWorld.Enabled = false;

                groupBoxSelectWorld.Enabled = false;
                groupBoxImageOutput.Enabled = false;
                (this.tabPageSettings as Control).Enabled = false;
                (this.tabPageWorldInformation as Control).Enabled = false;

                labelStatus.Text = "Drawing World...";
                progressBarDrawWorld.Visible = true;

                mapperWorker = new BackgroundWorker();
                mapperWorker.DoWork += new DoWorkEventHandler(worker_GenerateMap);
                mapperWorker.RunWorkerAsync(true);

                tmrMapperProgress.Enabled = true;
            }                    
        }

        private void worker_GenerateMap(object sender, DoWorkEventArgs e)
        {
            try
            {
                mapper = new WorldMapper();
                mapper.Initialize();
                mapper.OpenWorld(worldPath);
                PopulateWorldTree();

                if ((bool)e.Argument == true)
                {
                    //the timer will populate the chests for us
                    mapper.CreatePreviewPNG(textBoxOutputFile.Text);
                    if (checkBoxOpenImage.Checked) System.Diagnostics.Process.Start(textBoxOutputFile.Text);
                }
                else
                {
                    PopulateChestTree();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Opening World", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                mapper.CloseWorld();
            }

        }

        private void tmrMapperProgress_Tick(object sender, EventArgs e)
        {
            progressBarDrawWorld.Value = mapper.progress;

            labelPercent.Text = mapper.progress + "%";

            if (mapper.progress >= 100)
            {
                tmrMapperProgress.Enabled = false;

                PopulateChestTree();
                labelStatus.Text = "Ready";
                labelPercent.Text = string.Empty;
                progressBarDrawWorld.Value = 0;
                progressBarDrawWorld.Visible = false;

                buttonDrawWorld.Enabled = true;

                groupBoxSelectWorld.Enabled = true;
                groupBoxImageOutput.Enabled = true;
                (this.tabPageSettings as Control).Enabled = true;
                (this.tabPageWorldInformation as Control).Enabled = true;                
            }
        }

        private void PopulateWorldTree()
        {
            if (worldPropertyGrid.InvokeRequired)
            {
                PopulateWorldTreeDelegate del = new PopulateWorldTreeDelegate(PopulateWorldTree);
                worldPropertyGrid.Invoke(del);
                return;
            }
            
            worldPropertyGrid.SelectedObject = mapper.Header;
        }

        private void PopulateChestTree()
        {
            if (treeViewChestInformation.InvokeRequired)
            {
                PopulateChestTreeDelegate del = new PopulateChestTreeDelegate(PopulateChestTree);
                treeViewChestInformation.Invoke(del);
                return;
            }

            //see below, when set to true
            treeViewChestInformation.SuspendLayout();
            treeViewChestInformation.Scrollable = false;
            

            treeViewChestInformation.Nodes.Clear();

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


            //fixes a bug that will cause the last item to only show the top half, and you can't scroll further down
            treeViewChestInformation.Scrollable = true;
            treeViewChestInformation.ResumeLayout(true);
        }            

        private void checkedListBoxMarkers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.ToggleSymbolVisibility(checkedListBoxChestFilterWeapons.GetItemText(checkedListBoxMarkers.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        private void checkedListBoxChestFilterWeapons_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.ToggleFilterWeapon(checkedListBoxChestFilterWeapons.GetItemText(checkedListBoxChestFilterWeapons.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        private void checkedListBoxChestFilterAccessories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.ToggleFilterAccessories(checkedListBoxChestFilterAccessories.GetItemText(checkedListBoxChestFilterAccessories.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        #region " LinkLabel Event Handlers "

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

        private void linkLabelHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://terrariaworldviewer.codeplex.com/");
        }

        private void linkLabelSelectAllMarkers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxMarkers.Items.Count; i++)
            {
                checkedListBoxMarkers.SetItemChecked(i, true);
            }
        }

        private void linkLabelSelectNoneMarkers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxMarkers.Items.Count; i++)
            {
                checkedListBoxMarkers.SetItemChecked(i, false);
            }
        }

        private void linkLabelInvertMarkers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxMarkers.Items.Count; i++)
            {
                checkedListBoxMarkers.SetItemChecked(i, !checkedListBoxMarkers.GetItemChecked(i));
            }
        }

        #endregion
       
        private void buttonLoadInformation_Click(object sender, EventArgs e)
        {
            if (checkValidPaths(false))
            {
                buttonDrawWorld.Enabled = false;

                groupBoxSelectWorld.Enabled = false;
                groupBoxImageOutput.Enabled = false;
                (this.tabPageSettings as Control).Enabled = false;
                (this.tabPageWorldInformation as Control).Enabled = false;

                labelStatus.Text = "Reading World...";

                mapperWorker = new BackgroundWorker();
                mapperWorker.DoWork += new DoWorkEventHandler(worker_GenerateMap);
                mapperWorker.RunWorkerAsync(false);

                tmrMapperProgress.Enabled = true;
            }    
        }

          

        private bool checkValidPaths(bool checkOutput)
        {
            if (comboBoxWorldFilePath.Text == string.Empty || !File.Exists(comboBoxWorldFilePath.Text))
            {
                MessageBox.Show("The World file could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (checkOutput && textBoxOutputFile.Text == string.Empty)
            {
                MessageBox.Show("Please enter desired output image path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void checkBoxDrawWalls_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.Instance.IsWallDrawable = checkBoxDrawWalls.Checked;
        }

    }
}