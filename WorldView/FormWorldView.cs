using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using MoreTerra.Properties;
using MoreTerra.Structures;
using MoreTerra.Utilities;

namespace MoreTerra
{
    public partial class FormWorldView : Form
    {
        private delegate void PopulateWorldTreeDelegate();
        private delegate void PopulateChestTreeDelegate(TreeNode[] node_array);

        private WorldMapper mapper = null;
        private BackgroundWorker mapperWorker = null;
		private System.Timers.Timer tmrMapperProgress;

		// Used to store the images for our TreeView marker list.
		private ImageList iList = new ImageList();


        private string worldPath = string.Empty;

		// Used to store the real names of each .WLD file in the chosen directory.
		private Dictionary<String, String> worldNames = new Dictionary<String, String>();

		// This stores the nodes for easy lookup.  Only the subnodes are stored.
		private Dictionary<String, TreeNode> markerNodes;

		// Used to store the chest TreeNode objects for when we change sorting to None.
		private List<TreeNode> chestNodes;

		// Gives us a list of nodes that pass the chest finder filter.
		private List<TreeNode> filteredChestNodes;

		// We use this to see if something new got added to the item list when we are done
		// drawing/loading information.
		private Int32 filterCount;

		// We'll use this to refilter the list on the World Information page if we've
		// changed what's in the filter since we switched tabs.
		private Boolean filterUpdated;

		private FormProgressDialog progForm;

        public FormWorldView()
        {
            InitializeComponent();

			labelCustomResources.Text = "If you wish to change the icons that are shown on the " +
				                        "drawn map you can do so by replacing the corresponding file " +
										"found in the resource folder.";

            labelSpecialThanks.Text = Constants.Credits + Environment.NewLine + Environment.NewLine +
                                      "And special thanks to kdfb for donating a copy of the game!";

			tmrMapperProgress = new System.Timers.Timer();
            tmrMapperProgress.Elapsed += new ElapsedEventHandler(tmrMapperProgress_Tick);
            tmrMapperProgress.Enabled = false;
            tmrMapperProgress.Interval = 333;

			// If we've updated the software push it into the settings file.
			if (SettingsManager.Instance.TopVersion < Constants.currentVersion)
				SettingsManager.Instance.TopVersion = Constants.currentVersion;

			// Populate Symbol Properties
            Dictionary<string, bool> symbolStates = SettingsManager.Instance.SymbolStates;

			if (SettingsManager.Instance.FilterItemStates != null)
				ResetFilterLists();

			Boolean checkParent;
			Int32 index = 0;
			TreeNode tNode;
			TreeNode baseNode;
			treeViewMarkerList.ImageList = iList;

			markerNodes = new Dictionary<String, TreeNode>();

			foreach (KeyValuePair<string, string[]> kvp in Constants.SymbolDict)
			{
				baseNode = new TreeNode(kvp.Key);
				iList.Images.Add((Image)Properties.Resources.ResourceManager.GetObject(kvp.Key));
				baseNode.ImageIndex = index;
				baseNode.SelectedImageIndex = index++;
				this.treeViewMarkerList.Nodes.Add(baseNode);
				checkParent = true;

				for (Int32 i = 0; i < kvp.Value.Length; i++)
				{
					iList.Images.Add((Image)Properties.Resources.ResourceManager.GetObject(kvp.Value[i]));
					tNode = new TreeNode(kvp.Value[i]);
					tNode.ImageIndex = index;
					tNode.SelectedImageIndex = index++;
					markerNodes.Add(kvp.Value[i], tNode);
					baseNode.Nodes.Add(tNode);

					if (symbolStates.ContainsKey(kvp.Value[i]))
						tNode.Checked = symbolStates[kvp.Value[i]];

					if (tNode.Checked == false)
						checkParent = false;
				}

				baseNode.Checked = checkParent;
			}

			treeViewMarkerList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMarkerList_AfterCheck);
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
				SettingsManager.Instance.InputWorldDirectory = folder;
            }

            if (folder != string.Empty)
				this.worldDirectoryChanged();

			checkBoxOpenImage.Checked = SettingsManager.Instance.OpenImage;
			checkBoxDrawWalls.Checked = SettingsManager.Instance.DrawWalls;
            checkBoxFilterChests.Checked = SettingsManager.Instance.FilterChests;
			checkBoxScanForItems.Checked = SettingsManager.Instance.ScanForNewItems;

			switch (SettingsManager.Instance.SortChestsBy)
			{
				case 1:
					radioButtonSortByX.Checked = true;
					break;
				case 2:
					radioButtonSortByY.Checked = true;
					break;
				default:
					radioButtonSortByNone.Checked = true;
					break;
			}
        }

        private void comboBoxWorldFilePath_TextChanged(object sender, EventArgs e)
        {
            worldPath = comboBoxWorldFilePath.Text;

            if (File.Exists(comboBoxWorldFilePath.Text) && Directory.Exists(SettingsManager.Instance.OutputPreviewDirectory))
            {
                textBoxOutputFile.Text = Path.Combine(SettingsManager.Instance.OutputPreviewDirectory, Path.GetFileNameWithoutExtension(comboBoxWorldFilePath.Text) + ".png");
            }

			if (worldNames.ContainsKey(worldPath) == true)
			{
				labelWorldName.Text = "World Name: " + worldNames[worldPath];
			} else {
				labelWorldName.Text = "World Name: ";
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
			if (!(SettingsManager.Instance.InputWorldDirectory == Path.GetDirectoryName(filePath)))
			{
				SettingsManager.Instance.InputWorldDirectory = Path.GetDirectoryName(filePath);
				worldDirectoryChanged(filePath);
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
			if (SettingsManager.Instance.FilterChests && !SettingsManager.Instance.DrawMarker("Chest"))
			{
				DialogResult markers = MessageBox.Show("You have enabled Chest Filtering but have disabled drawing Chest Markers. " +
													   "No Chests will show up, even if they contain the items you are looking for.\r\n\r\n" +
													   "Would you like to enable Markers for Chests before continuing?", "Chest Markers disabled",
													   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

				if (markers == System.Windows.Forms.DialogResult.Yes)
				{
					SettingsManager.Instance.MarkerVisible("Chest", true);
					markerNodes["Chest"].Checked = true;
					treeViewMarkerList_updateParentNode(markerNodes["Chest"]);
				}
				else if (markers == System.Windows.Forms.DialogResult.Cancel)
				{
					return;
				}
			}

			if (SettingsManager.Instance.FilterChests && lstFilteredItems.Items.Count == 0)
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
				filterCount = SettingsManager.Instance.FilterItemStates.Count();

                groupBoxSelectWorld.Enabled = false;
                groupBoxImageOutput.Enabled = false;
                (this.tabPageSettings as Control).Enabled = false;
                (this.tabPageWorldInformation as Control).Enabled = false;

                mapper = new WorldMapper();
                mapper.Initialize();

                if (textBoxOutputFile.Text.Substring(textBoxOutputFile.Text.Length - 4).CompareTo(".png") != 0)
                {
                    textBoxOutputFile.Text += ".png";
                }

                mapperWorker = new BackgroundWorker();
				progForm = new FormProgressDialog("Draw World", false, mapperWorker);

				mapperWorker.WorkerReportsProgress = true;
				mapperWorker.WorkerSupportsCancellation = true;
				mapperWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(progForm.worker_Completed);
				mapperWorker.ProgressChanged += new ProgressChangedEventHandler(progForm.worker_ProgressChanged);
                mapperWorker.DoWork += new DoWorkEventHandler(worker_GenerateMap);
                mapperWorker.RunWorkerAsync(true);

				progForm.FormClosed += new FormClosedEventHandler(worker_Completed);
				progForm.Show();
            }
        }

        private void worker_GenerateMap(object sender, DoWorkEventArgs e)
        {
			BackgroundWorker bw = (BackgroundWorker) sender;

			tmrMapperProgress.Start();

#if  (DEBUG == false)
            try
            {
#endif
				mapper.OpenWorld();

                //we're drawing a map
				if ((bool)e.Argument == true)
					mapper.ProcessWorld(worldPath, bw);
				else 
					mapper.ReadChests(worldPath, bw);

                TreeNode[] chests = GetChests();

				PopulateWorldTree();
                PopulateChestTree(chests);

                //we're drawing a map
                if ((bool)e.Argument == true)
                {
                    mapper.CreatePreviewPNG(textBoxOutputFile.Text, bw);
                    if (SettingsManager.Instance.OpenImage)
						System.Diagnostics.Process.Start(textBoxOutputFile.Text);
                }

				tmrMapperProgress.Stop();
#if  (DEBUG == false)
            }
            catch (Exception ex)
            {
				tmrMapperProgress.Stop();
				MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Details: " + ex.ToString(),
								 "Error Opening World", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}
#endif
        }

        private void tmrMapperProgress_Tick(object sender, EventArgs e)
        {
			mapperWorker.ReportProgress(mapper.progress);
		}


		private void worker_Completed(object sender, FormClosedEventArgs e)
			{
			Boolean v = progForm.Success;
				if (filterCount != SettingsManager.Instance.FilterItemStates.Count())
					ResetFilterLists();

				buttonDrawWorld.Enabled = true;

				groupBoxSelectWorld.Enabled = true;
				groupBoxImageOutput.Enabled = true;
				(this.tabPageSettings as Control).Enabled = true;
				(this.tabPageWorldInformation as Control).Enabled = true;
				this.checkBoxScanForItems.Checked = SettingsManager.Instance.ScanForNewItems;
			}

        private void PopulateWorldTree()
        {
            if (worldPropertyGrid.InvokeRequired)
            {
                PopulateWorldTreeDelegate del = new PopulateWorldTreeDelegate(PopulateWorldTree);
                worldPropertyGrid.Invoke(del);
                return;
            }

            worldPropertyGrid.SelectedObject = mapper.World.Header;
        }

        private TreeNode[] GetChests()
        {
            List<Chest> chests = this.mapper.Chests;

			if (chestNodes == null)
			{
				chestNodes = new List<TreeNode>(chests.Count);

				foreach (Chest c in chests)
				{
					TreeNode node = new TreeNode(string.Format("Chest at ({0},{1})", c.Coordinates.X, c.Coordinates.Y, c.ChestId));
					foreach (Item i in c.Items)
					{
						node.Nodes.Add(i.ToString());
					}
					chestNodes.Add(node);
				}
			}

			if (SettingsManager.Instance.FilterChests == true)
			{
				applyChestFilter();
				return filteredChestNodes.ToArray();
			}

            return chestNodes.ToArray();
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

        private void linkLabelHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://moreterra.codeplex.com/");
        }

        private void buttonLoadInformation_Click(object sender, EventArgs e)
        {
            if (checkValidPaths(false))
            {
				chestNodes = null;
				filterCount = SettingsManager.Instance.FilterItemStates.Count;

                buttonDrawWorld.Enabled = false;

                groupBoxSelectWorld.Enabled = false;
                groupBoxImageOutput.Enabled = false;
                (this.tabPageSettings as Control).Enabled = false;
                (this.tabPageWorldInformation as Control).Enabled = false;

                mapper = new WorldMapper();
                mapper.Initialize();

                mapperWorker = new BackgroundWorker();
				progForm = new FormProgressDialog("Load World Information", false, mapperWorker);

				mapperWorker.WorkerReportsProgress = true;
				mapperWorker.WorkerSupportsCancellation = false;
				mapperWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(progForm.worker_Completed);
				mapperWorker.ProgressChanged += new ProgressChangedEventHandler(progForm.worker_ProgressChanged);
                mapperWorker.DoWork += new DoWorkEventHandler(worker_GenerateMap);
                mapperWorker.RunWorkerAsync(false);

				progForm.FormClosed += new FormClosedEventHandler(worker_Completed);
				progForm.Show();
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
			if (contextMenuStripListOperations.SourceControl is TreeView)
			{
				foreach (TreeNode p in (contextMenuStripListOperations.SourceControl as TreeView).Nodes)
				{
					p.Checked = true;

					foreach (TreeNode n in p.Nodes)
					{
						n.Checked = true;
						SettingsManager.Instance.MarkerVisible(n.Text, true);
					}
				}
			}
		}

        private void selectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
			if (contextMenuStripListOperations.SourceControl is TreeView)
			{
				foreach (TreeNode p in (contextMenuStripListOperations.SourceControl as TreeView).Nodes)
				{
					p.Checked = false;

					foreach (TreeNode n in p.Nodes)
					{
						n.Checked = false;
						SettingsManager.Instance.MarkerVisible(n.Text, false);
					}
				}
			}
		}

        private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Boolean parentChecked;
			Boolean nodeFlip;

			if (contextMenuStripListOperations.SourceControl is TreeView)
			{
				foreach (TreeNode p in (contextMenuStripListOperations.SourceControl as TreeView).Nodes)
				{
					parentChecked = true;

					foreach (TreeNode n in p.Nodes)
					{
						nodeFlip = !n.Checked;
						SettingsManager.Instance.MarkerVisible(n.Text, nodeFlip);
						n.Checked = nodeFlip;

						if (nodeFlip == false)
							parentChecked = false;
					}

					p.Checked = parentChecked;
				}
			}
		}

        private void checkBoxFilterChests_CheckedChanged(object sender, EventArgs e)
        {
			SettingsManager.Instance.FilterChests = checkBoxFilterChests.Checked;

			lstAvailableItems.Enabled = checkBoxFilterChests.Checked;
			lstFilteredItems.Enabled = checkBoxFilterChests.Checked;
			filterUpdated = true;
		}

		private void lstFilteredItems_DoubleClick(object sender, EventArgs e)
		{
			lstFilteredItems_KeyDown(sender, new KeyEventArgs(Keys.Delete));
		}

		private void lstAvailableItems_DoubleClick(object sender, EventArgs e)
		{
			String si;
			int selected = lstAvailableItems.SelectedIndex;
			
			if (selected == -1) return;

			si = lstAvailableItems.SelectedItem.ToString();

			lstFilteredItems.Items.Add(si);

			lstAvailableItems.Items.Remove(si);
			lstAvailableItems.SelectedIndex = Math.Min(selected, lstFilteredItems.Items.Count - 1);

			// Strip the * off of it, if it had one.
			si = si.Split('*')[0];
			SettingsManager.Instance.FilterItem(si, true);
			filterUpdated = true;
		}
	

		private void lstFilteredItems_KeyDown(object sender, KeyEventArgs e)
		{
			String si;
			int selected = lstFilteredItems.SelectedIndex;

			if (selected == -1) return;

			if (e.KeyCode == Keys.Delete)
			{
				si = lstFilteredItems.SelectedItem.ToString();

				lstAvailableItems.Items.Add(si);

				lstFilteredItems.Items.Remove(lstFilteredItems.SelectedItem);
				lstFilteredItems.SelectedIndex = Math.Min(selected, lstFilteredItems.Items.Count - 1);

				// Strip the * off of it, if it had one.
				si = si.Split('*')[0];
				SettingsManager.Instance.FilterItem(si, false);
				filterUpdated = true;
			}
		}

		private void treeViewMarkerList_updateParentNode(TreeNode e)
		{
			Boolean parentChecked;

			parentChecked = true;
			foreach (TreeNode n in e.Parent.Nodes)
			{
				if (n.Checked == false)
					parentChecked = false;
			}

			e.Parent.Checked = parentChecked;
		}

		private void treeViewMarkerList_AfterCheck(object sender, TreeViewEventArgs e)
		{
			// Don't do anything unless it was the user who set the checked state.
			if (e.Action == TreeViewAction.Unknown)
				return;

			if (markerNodes.ContainsKey(e.Node.Text))
			{
				SettingsManager.Instance.MarkerVisible(e.Node.Text, e.Node.Checked);
				treeViewMarkerList_updateParentNode(e.Node);
			}
			else
			{
				foreach (TreeNode n in e.Node.Nodes)
				{
					n.Checked = e.Node.Checked;
					SettingsManager.Instance.MarkerVisible(n.Text, e.Node.Checked);
				}
			}
		}

		private void worldDirectoryChanged(String selectedFile = null)
		{
			worldNames.Clear();
			Int32 i = 0;
			Int32 selection = 0;
			World w;

			comboBoxWorldFilePath.Items.Clear();

			w = new World();

			foreach (String file in Directory.GetFiles(SettingsManager.Instance.InputWorldDirectory, "*.wld"))
			{
				worldNames.Add(file, w.GetWorldName(file));
				comboBoxWorldFilePath.Items.Add(file);

				if (String.IsNullOrEmpty(selectedFile) != true && file == selectedFile)
				{
					selection = i;
				}
				i++;
			}

			if (comboBoxWorldFilePath.Items.Count > 0)
				comboBoxWorldFilePath.SelectedIndex = selection;
		}

		private void buttonCustomResources_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Constants.ApplicationResourceDirectory);
		}

		private void radioButtonSortByNone_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.SortChestsBy = 0;

			treeViewChestInformation.TreeViewNodeSorter = null;
			treeViewChestInformation.Sorted = false;

			if (chestNodes != null)
				PopulateChestTree(GetChests());
		}

		private void radioButtonSortByX_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.SortChestsBy = 1;

			treeViewChestInformation.TreeViewNodeSorter = new ChestComparerX();
		}

		private void radioButtonSortByY_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.SortChestsBy = 2;

			treeViewChestInformation.TreeViewNodeSorter = new ChestComparerY();
		}

		private void applyChestFilter()
		{
			String itemName;
			Dictionary<string, bool> itemFilters = SettingsManager.Instance.FilterItemStates;
			filteredChestNodes = new List<TreeNode>();

			foreach(TreeNode tn in chestNodes)
			{
				// If we are checking the chest node itself.
				if (tn.Parent == null)
				{
					foreach (TreeNode item in tn.Nodes)
					{
						itemName = item.Text.Split(',')[0];

						if (itemFilters.ContainsKey(itemName) && itemFilters[itemName] == true)
						{
							filteredChestNodes.Add(tn);
							break;
						}
					}
				}
			}
		}

		private void buttonAddCustomItem_Click(object sender, EventArgs e)
		{
			String newItem;
			String error = "";
			DialogResult res;
			FormEntryBox entry = new FormEntryBox();

			res = entry.ShowDialog();

			if (res == DialogResult.Cancel)
				return;

			newItem = entry.getCustomItem();

			if (SettingsManager.Instance.IsDefaultItem(newItem))
			{
				error = String.Format("Item '{0}' is already coded into the program.", newItem);
			}
			else if (SettingsManager.Instance.FilterItemStates.ContainsKey(newItem))
			{
				error = String.Format("Item '{0}' was already added as a custom item.", newItem);
			}

			if (error == "")
			{
				SettingsManager.Instance.FilterItemStates.Add(newItem, false);
				lstAvailableItems.Items.Add(newItem + "*");
			}
			else
			{
				MessageBox.Show(error, "Item already exists!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void buttonRemoveCustomItem_Click(object sender, EventArgs e)
		{
			String si;
			Int32 selection = lstFilteredItems.SelectedIndex;

			if (selection != -1)
			{
				si = lstFilteredItems.SelectedItem.ToString();

				si = si.Split('*')[0];

				// I don't know how someone would get here with a non-custom item but just to be safe.
				if (SettingsManager.Instance.IsDefaultItem(si))
					return;

				SettingsManager.Instance.FilterItemStates.Remove(si);

				lstFilteredItems.Items.RemoveAt(selection);

				if (lstFilteredItems.Items.Count != 0)
					lstFilteredItems.SelectedIndex = Math.Min(selection, lstFilteredItems.Items.Count);

				filterUpdated = true;
			}
			else
			{
				selection = lstAvailableItems.SelectedIndex;

				if (selection == -1)
					return;

				si = lstAvailableItems.SelectedItem.ToString();

				si = si.Split('*')[0];

				// I don't know how someone would get here with a non-custom item but just to be safe.
				if (SettingsManager.Instance.IsDefaultItem(si))
					return;

				SettingsManager.Instance.FilterItemStates.Remove(si);

				lstAvailableItems.Items.RemoveAt(selection);

				if (lstAvailableItems.Items.Count != 0)
				lstAvailableItems.SelectedIndex = Math.Min(selection, lstFilteredItems.Items.Count);
			}

		}

		private void lstFilteredItems_SelectionChanged(object sender, EventArgs e)
		{
			String si;
			Int32 selection = lstFilteredItems.SelectedIndex;

			if (selection == -1)
			{
				buttonRemoveCustomItem.Enabled = false;
				return;
			}

			si = lstFilteredItems.SelectedItem.ToString();

			si = si.Split('*')[0];

			buttonRemoveCustomItem.Enabled = !SettingsManager.Instance.IsDefaultItem(si);

			if (lstAvailableItems.SelectedIndex != -1)
				lstAvailableItems.SelectedIndex = -1;
		}

		private void lstAvailableItems_SelectionChanged(object sender, EventArgs e)
		{
			String si;
			Int32 selection = lstAvailableItems.SelectedIndex;

			if (selection == -1)
			{
				buttonRemoveCustomItem.Enabled = false;
				return;
			}

			si = lstAvailableItems.SelectedItem.ToString();

			si = si.Split('*')[0];

			buttonRemoveCustomItem.Enabled = !SettingsManager.Instance.IsDefaultItem(si);

			if (lstFilteredItems.SelectedIndex != -1)
				lstFilteredItems.SelectedIndex = -1;
		}

		private void buttonMoveAllToFiltered_Click(object sender, EventArgs e)
		{
			Int32 i, count;
			List<String> keys;

			if (lstAvailableItems.Items.Count == 0)
				return;

			keys = SettingsManager.Instance.FilterItemStates.Keys.ToList();
			count = keys.Count;

			for (i = 0; i < count; i++)
				SettingsManager.Instance.FilterItemStates[keys[i]] = true;

			ResetFilterLists();
		}

		private void buttonMoveAllToAvailable_Click(object sender, EventArgs e)
		{
			Int32 i, count;
			List<String> keys;

			if (lstFilteredItems.Items.Count == 0)
				return;

			keys = SettingsManager.Instance.FilterItemStates.Keys.ToList();
			count = keys.Count;

			for (i = 0; i < count; i++)
				SettingsManager.Instance.FilterItemStates[keys[i]] = false;

			ResetFilterLists();
		}

		private void ResetFilterLists()
		{
			String lstItem;

			lstFilteredItems.Items.Clear();
			lstAvailableItems.Items.Clear();

			foreach (KeyValuePair<string, bool> kvp in SettingsManager.Instance.FilterItemStates)
			{
				if (SettingsManager.Instance.IsDefaultItem(kvp.Key))
					lstItem = kvp.Key;
				else
					lstItem = kvp.Key + "*";

				if (kvp.Value == true)
					lstFilteredItems.Items.Add(lstItem);
				else
					lstAvailableItems.Items.Add(lstItem);
			}

			buttonMoveAllToAvailable.Enabled = (lstFilteredItems.Items.Count != 0);
			buttonMoveAllToFiltered.Enabled = (lstAvailableItems.Items.Count != 0);

			filterUpdated = true;
		}

		private void checkBoxScanForItems_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.ScanForNewItems = checkBoxScanForItems.Checked;
		}

		private void tabControlSettings_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControlSettings.SelectedTab.Text == "World Information")
			{
				if (this.mapper == null)
					return;

				if (filterUpdated == true)
				{
					PopulateChestTree(GetChests());
					filterUpdated = false;
				}
			}
		}

		private void checkBoxOpenImage_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.OpenImage = checkBoxOpenImage.Checked;
		}
	}
}