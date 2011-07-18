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
using MoreTerra.Properties;

namespace MoreTerra
{
    public partial class FormWorldView : Form
    {
        private delegate void PopulateWorldTreeDelegate();
        private delegate void PopulateChestTreeDelegate(TreeNode[] node_array);

        private WorldMapper mapper = null;
        private BackgroundWorker mapperWorker = null;
        private Timer tmrMapperProgress = new Timer();


        private string worldPath = string.Empty;



        public FormWorldView()
        {
            InitializeComponent();

            labelSpecialThanks.Text = Constants.Credits + Environment.NewLine + Environment.NewLine +
                                      "And special thanks to kdfb for donating a copy of the game!";

            tmrMapperProgress.Tick += new System.EventHandler(tmrMapperProgress_Tick);
            tmrMapperProgress.Enabled = false;
            tmrMapperProgress.Interval = 333;

            // Populate Symbol Properties
            Dictionary<string, bool> symbolStates = SettingsManager.Instance.SymbolStates;

            foreach (KeyValuePair<string, bool> kvp in symbolStates)
            {
                this.checkedListBoxMarkers.Items.Add(kvp.Key, kvp.Value);
            }


            if (SettingsManager.Instance.FilterItemStates != null)
            {
                foreach (KeyValuePair<string, bool> kvp in SettingsManager.Instance.FilterItemStates)
                {
                    lstAvailableItems.Items.Add(kvp.Key);
                    if (kvp.Value) lstChestFilter.Items.Add(kvp.Key);                    
                }
            }




            // Register the event handlers
            this.checkedListBoxMarkers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxMarkers_ItemCheck);
        }

        private void WorldViewForm_Load(object sender, EventArgs e)
        {
            string ver = Application.ProductVersion;

            while (ver.Length > 3 && ver.Substring(ver.Length - 2) == ".0")
            {
                ver = ver.Substring(0, ver.Length - 2);
            }

            lblVersion.Text = "Version: " + ver;

            string folder = string.Empty;

            if (Directory.Exists(SettingsManager.Instance.InputWorldDirectory))
            {
                folder = SettingsManager.Instance.InputWorldDirectory;
            }
            else if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\Terraria")))
            {
                folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\Terraria");
            }

            if (folder != string.Empty)
            {
                foreach (string file in Directory.GetFiles(SettingsManager.Instance.InputWorldDirectory, "*.wld"))
                {
                    comboBoxWorldFilePath.Items.Add(file);
                }

                if (comboBoxWorldFilePath.Items.Count > 0) comboBoxWorldFilePath.SelectedIndex = 0;
            }

            checkBoxFilterChests.Checked = SettingsManager.Instance.FilterChests;
        }

        private void comboBoxWorldFilePath_TextChanged(object sender, EventArgs e)
        {
            worldPath = comboBoxWorldFilePath.Text;

            if (File.Exists(comboBoxWorldFilePath.Text) && Directory.Exists(SettingsManager.Instance.OutputPreviewDirectory))
            {
                textBoxOutputFile.Text = Path.Combine(SettingsManager.Instance.OutputPreviewDirectory, Path.GetFileNameWithoutExtension(comboBoxWorldFilePath.Text) + ".png");
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
            if (SettingsManager.Instance.FilterChests && !SettingsManager.Instance.DrawMarker("Chest"))
            {
                DialogResult markers = MessageBox.Show("You have enabled Chest Filtering but have disabled drawing Chest Markers. " +
                                                       "No Chests will show up, even if they contain the items you are looking for.\r\n\r\n" +
                                                       "Would you like to enable Markers for Chests before continuing?", "Chest Markers disabled", 
                                                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (markers == System.Windows.Forms.DialogResult.Yes)
                {
                    SettingsManager.Instance.MarkerVisible("Chest", true);
                    checkedListBoxMarkers.SetItemChecked(checkedListBoxMarkers.Items.IndexOf("Chest"), true);
                }
                else if (markers == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }

            if (SettingsManager.Instance.FilterChests && lstChestFilter.Items.Count == 0)
            {
                DialogResult markers = MessageBox.Show("You have enabled Chest Filtering but have not selected any items. " +
                                                       "No Chests will show up, even if they are empty.\r\n\r\n" +
                                                       "Would you like to disable Chest Filtering before continuing?", "Chest Finder list is empty",
                                                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (markers == System.Windows.Forms.DialogResult.Yes)
                {
                    SettingsManager.Instance.FilterChests = false;
                    checkBoxFilterChests.Checked = false;
                }
                else if (markers == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }

            if (checkValidPaths(true))
            {
                buttonDrawWorld.Enabled = false;

                groupBoxSelectWorld.Enabled = false;
                groupBoxImageOutput.Enabled = false;
                (this.tabPageSettings as Control).Enabled = false;
                (this.tabPageWorldInformation as Control).Enabled = false;

                mapper = new WorldMapper();
                mapper.Initialize();

                labelStatus.Text = "Reading World...";
                labelPercent.Visible = true;
                labelPercent.Text = mapper.progress + "%";
                progressBarDrawWorld.Value = mapper.progress;
                progressBarDrawWorld.Visible = true;

                if (textBoxOutputFile.Text.Substring(textBoxOutputFile.Text.Length - 4).CompareTo(".png") != 0)
                {
                    textBoxOutputFile.Text += ".png";
                }

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
                mapper.OpenWorld(worldPath);

                //we're drawing a map
                if ((bool)e.Argument == true) mapper.ReadWorldTiles();

                PopulateWorldTree();

                TreeNode[] chests = GetChests();

                PopulateChestTree(chests);

                mapper.CloseWorld();

                //we're drawing a map
                if ((bool)e.Argument == true)
                {
                    mapper.CreatePreviewPNG(textBoxOutputFile.Text);
                    if (checkBoxOpenImage.Checked) System.Diagnostics.Process.Start(textBoxOutputFile.Text);
                }

                mapper.progress = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Details: " + ex.ToString(),
                                 "Error Opening World", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (mapper.progress > 30 && mapper.progress < 50)
            {
                labelStatus.Text = "Reading Chests...";
            }
            else if (mapper.progress > 50 && mapper.progress < 100)
            {
                labelStatus.Text = "Drawing World...";
            }
            else if (mapper.progress >= 100)
            {
                tmrMapperProgress.Enabled = false;

                labelStatus.Text = "Ready";
                labelPercent.Text = string.Empty;
                labelPercent.Visible = false;
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

        private TreeNode[] GetChests()
        {
            List<Chest> chests = this.mapper.Chests;
            List<TreeNode> nodes = new List<TreeNode>(chests.Count);

            foreach (Chest c in chests)
            {
                TreeNode node = new TreeNode(string.Format("Chest at ({0},{1})", c.Coordinates.X, c.Coordinates.Y));
                foreach (Item i in c.Items) node.Nodes.Add(i.ToString());
                nodes.Add(node);
            }

            return nodes.ToArray();
        }

        private void PopulateChestTree(TreeNode[] node_array)
        {
            if (treeViewChestInformation.InvokeRequired)
            {
                PopulateChestTreeDelegate del = new PopulateChestTreeDelegate(PopulateChestTree);
                treeViewChestInformation.Invoke(del, new object[] { node_array });
                return;
            }

            treeViewChestInformation.SuspendLayout();
            treeViewChestInformation.Nodes.Clear();

            //nodes have to be added in this fairly awkward way, because this
            //fixes a bug with the TreeView control that will cause the last node
            //to have its lower half cut off at the bottom of the control,
            //and it won't allow the user to scroll further down...

            treeViewChestInformation.Nodes.AddRange(node_array);

            treeViewChestInformation.ResumeLayout(true);
        }

        private void checkedListBoxMarkers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingsManager.Instance.MarkerVisible(checkedListBoxMarkers.GetItemText(checkedListBoxMarkers.Items[e.Index]), e.NewValue == CheckState.Checked);
        }

        private void linkLabelHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://terrariaworldviewer.codeplex.com/");
        }

        private void buttonLoadInformation_Click(object sender, EventArgs e)
        {
            if (checkValidPaths(false))
            {
                buttonDrawWorld.Enabled = false;

                groupBoxSelectWorld.Enabled = false;
                groupBoxImageOutput.Enabled = false;
                (this.tabPageSettings as Control).Enabled = false;
                (this.tabPageWorldInformation as Control).Enabled = false;

                labelStatus.Text = "Reading Chests...";
                labelPercent.Visible = false;

                mapper = new WorldMapper();
                mapper.Initialize();

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
            SettingsManager.Instance.DrawWalls = checkBoxDrawWalls.Checked;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStripListOperations.SourceControl is CheckedListBox)
            {
                for (int i = 0; i < (contextMenuStripListOperations.SourceControl as CheckedListBox).Items.Count; i++)
                {
                    (contextMenuStripListOperations.SourceControl as CheckedListBox).SetItemChecked(i, true);
                }
            }
        }

        private void selectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStripListOperations.SourceControl is CheckedListBox)
            {
                for (int i = 0; i < (contextMenuStripListOperations.SourceControl as CheckedListBox).Items.Count; i++)
                {
                    (contextMenuStripListOperations.SourceControl as CheckedListBox).SetItemChecked(i, false);
                }
            }
        }

        private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStripListOperations.SourceControl is CheckedListBox)
            {
                for (int i = 0; i < (contextMenuStripListOperations.SourceControl as CheckedListBox).Items.Count; i++)
                {
                    (contextMenuStripListOperations.SourceControl as CheckedListBox).SetItemChecked(i, !(contextMenuStripListOperations.SourceControl as CheckedListBox).GetItemChecked(i));
                }
            }
        }

        private void checkBoxFilterChests_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.Instance.FilterChests = checkBoxFilterChests.Checked;
            
            lstAvailableItems.Enabled = checkBoxFilterChests.Checked;
            lstChestFilter.Enabled = checkBoxFilterChests.Checked;
        }

        private void lstChestFilter_DoubleClick(object sender, EventArgs e)
        {
            lstChestFilter_KeyDown(sender, new KeyEventArgs(Keys.Delete));
        }

        private void lstAvailableItems_DoubleClick(object sender, EventArgs e)
        {
            if (lstAvailableItems.SelectedIndex == -1) return;

            if (!lstChestFilter.Items.Contains(lstAvailableItems.SelectedItem.ToString())) lstChestFilter.Items.Add(lstAvailableItems.SelectedItem.ToString());
            SettingsManager.Instance.FilterItem(lstAvailableItems.SelectedItem.ToString(), true);
        }

        private void lstChestFilter_KeyDown(object sender, KeyEventArgs e)
        {
            int selected =  lstChestFilter.SelectedIndex;

            if (selected == -1) return;

            if (e.KeyCode == Keys.Delete)
            {
                SettingsManager.Instance.FilterItem(lstChestFilter.SelectedItem.ToString(), false);
                lstChestFilter.Items.Remove(lstChestFilter.SelectedItem);
                lstChestFilter.SelectedIndex = Math.Min(selected, lstChestFilter.Items.Count-1);
            }
        }
    }
}