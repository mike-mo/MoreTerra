using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
using System.Timers;
using MoreTerra.Properties;
using MoreTerra.Structures;
using MoreTerra.Utilities;
using MoreTerra.Structures.TerraInfo;

namespace MoreTerra
{
	public partial class FormWorldView : Form
	{
		private delegate void PopulateWorldTreeDelegate();
		private delegate void PopulateChestTreeDelegate(TreeNode[] node_array);
		private delegate DialogResult MessageBoxShowDelegate(String text);
		private delegate DialogResult MessageBoxShowFullDelegate(String text, String caption,
			MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton);
		private delegate DialogResult MessageBoxWithCheckBoxShowDelegate(FormMessageBoxWithCheckBox box);


		#region Variable definitions
		private WorldMapper mapper = null;
		private BackgroundWorker mapperWorker = null;
		private System.Timers.Timer tmrMapperProgress;

		// Used to store the images for our TreeView marker list.
		private ImageList markerImageList;
		private ImageList markerThreeStateList;
		private ImageList chestImageList;
		private ImageList colorImageList;
		private ImageList buttonImageList;

		private string worldPath = string.Empty;

		// Used to store the real names of each .WLD file in the chosen directory.
		private Dictionary<String, String> worldNames = new Dictionary<String, String>();

		// This stores the nodes for easy lookup.  Only the subnodes are stored.
		private Dictionary<String, TreeNode> markerNodes;

		// We use this to see if something new got added to the item list when we are done
		// drawing/loading information.
		private Int32 filterCount;

		// We'll use this to refilter the list on the World Information page if we've
		// changed what's in the filter since we switched tabs.
		private Boolean filterUpdated;

		// This is used to hold the data for the color TreeView item.
		private ColorListData colorData;

		private FormProgressDialog progForm;
		#endregion

		public FormWorldView()
		{
			FormWorldView.Form = this;

			InitializeComponent();
			RegisterEventHandlers();

			this.Icon = Properties.Resources.Cannon;
		}

		// Done here instead of by the automatic code generator as I'm tired of the code
		// Generator removing my event handlers when I try to move items around.
		private void RegisterEventHandlers()
		{
			#region Global Handlers
			this.Load += new System.EventHandler(this.WorldViewForm_Load);

			// Select World groupbox
			this.comboBoxWorldFilePath.SelectedIndexChanged += new System.EventHandler(this.comboBoxWorldFilePath_TextChanged);
			this.comboBoxWorldFilePath.TextChanged += new System.EventHandler(this.comboBoxWorldFilePath_TextChanged);
			this.buttonBrowseWorld.Click += new System.EventHandler(this.buttonBrowseWorld_Click);

			// Settings groupbox
			this.comboBoxSettings.SelectedIndexChanged += new System.EventHandler(this.comboBoxSettings_SelectedIndexChanged);
			this.buttonSettingsAddNew.Click += new System.EventHandler(this.buttonSettingsAddNew_Click);
			this.buttonSettingsDelete.Click += new System.EventHandler(this.buttonSettingsDelete_Click);

			// Tab Control
			this.tabControlSettings.SelectedIndexChanged += new System.EventHandler(this.tabControlSettings_SelectedIndexChanged);
			#endregion

			#region Draw World tabPage Handlers
			// Output Image groupbox
			this.buttonBrowseOutput.Click += new System.EventHandler(this.buttonBrowseOutput_Click);

            this.checkBoxOfficialColors.CheckedChanged += new System.EventHandler(this.checkBoxOfficialColors_CheckedChanged);
			this.checkBoxDrawWires.CheckedChanged += new System.EventHandler(this.checkBoxDrawWires_CheckedChanged);
			this.checkBoxDrawWalls.CheckedChanged += new System.EventHandler(this.checkBoxDrawWalls_CheckedChanged);
			this.checkBoxOpenImage.CheckedChanged += new System.EventHandler(this.checkBoxOpenImage_CheckedChanged);

			this.comboBoxCropImage.SelectedIndexChanged += new System.EventHandler(this.comboBoxCropImage_SelectedIndexChanged);

			this.buttonDrawWorld.Click += new System.EventHandler(this.buttonDrawWorld_Click);

			// Unsupported World Version groupbox
			#endregion

			#region Markers tabPage Handlers
			this.checkBoxShowChestTypes.CheckedChanged += new System.EventHandler(this.checkBoxShowChestTypes_CheckedChanged);

			this.treeViewMarkerList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMarkerList_AfterCheck);
			this.treeViewMarkerList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewMarkerList_NodeMouseClick);
			this.treeViewMarkerList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeViewMarkerList_KeyPress);

			// Use Custom Images groupbox
			this.checkBoxCustomMarkers.CheckedChanged += new System.EventHandler(this.checkBoxCustomMarkers_CheckedChanged);
			this.buttonResetCustomImages.Click += new System.EventHandler(this.buttonResetCustomImages_Click);
			this.buttonCustomResources.Click += new System.EventHandler(this.buttonCustomResources_Click);

			// ContextMenu for treeViewmarkerList
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			this.selectNoneToolStripMenuItem.Click += new System.EventHandler(this.selectNoneToolStripMenuItem_Click);
			this.invertSelectionToolStripMenuItem.Click += new System.EventHandler(this.invertSelectionToolStripMenuItem_Click);
			#endregion

			#region Colors tabPage Handlers
			this.treeViewColorList.AfterSelect += new TreeViewEventHandler(treeViewColorList_AfterSelect);

			this.radioButtonColorDefault.CheckedChanged += new EventHandler(radioButtonColorDefault_CheckedChanged);
			this.radioButtonColorPreset.CheckedChanged += new EventHandler(radioButtonColorName_CheckedChanged);
			this.radioButtonColorColor.CheckedChanged += new EventHandler(radioButtonColorColor_CheckedChanged);

			this.textBoxColorColor.TextChanged += new EventHandler(textBoxColorColor_TextChanged);
			this.comboBoxColorName.SelectedIndexChanged += new EventHandler(comboBoxColorName_SelectedIndexChanged);
			this.buttonColorColor.Click += buttonColorColor_Click;
			#endregion

			#region Chest Finder tabPage Handlers
			this.checkBoxFilterChests.CheckedChanged += new System.EventHandler(this.checkBoxFilterChests_CheckedChanged);
			this.checkBoxShowChestItems.CheckedChanged +=new EventHandler(checkBoxShowChestItems_CheckedChanged);
			this.checkBoxShowNormalItems.CheckedChanged +=new EventHandler(checkBoxShowNormalItems_CheckedChanged);

			this.lstAvailableItems.DoubleClick += new System.EventHandler(this.lstAvailableItems_DoubleClick);
			this.buttonMoveAllToFiltered.Click += new System.EventHandler(this.buttonMoveAllToFiltered_Click);

			this.lstFilteredItems.DoubleClick += new System.EventHandler(this.lstFilteredItems_DoubleClick);
			this.lstFilteredItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFilteredItems_KeyDown);
			this.buttonMoveAllToAvailable.Click += new System.EventHandler(this.buttonMoveAllToAvailable_Click);
			#endregion

			#region World Information tabPage Handlers
			this.buttonLoadInformation.Click += new System.EventHandler(this.buttonLoadInformation_Click);

			// Sort By groupBox
			this.radioButtonSortByNone.CheckedChanged += new System.EventHandler(this.radioButtonSortByNone_CheckedChanged);
			this.radioButtonSortByX.CheckedChanged += new System.EventHandler(this.radioButtonSortByX_CheckedChanged);
			this.radioButtonSortByY.CheckedChanged += new System.EventHandler(this.radioButtonSortByY_CheckedChanged);
	
			// ContextMenu for Chest list
			this.saveToTextToolStripMenuItem.Click += new System.EventHandler(this.saveToTextToolStripMenuItem_Click);
			this.saveToCSVToolStripMenuItem.Click += new System.EventHandler(this.saveToCSVToolStripMenuItem_Click);
			this.saveToXMLToolStripMenuItem.Click += new System.EventHandler(this.saveToXMLtToolStripMenuItem_Click);
			#endregion

			#region About tabPage Handlers
			this.linkLabelHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHomepage_LinkClicked);
			#endregion

		}

		void buttonColorColor_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				var rgb = new byte[]{colorDialog.Color.R,colorDialog.Color.G, colorDialog.Color.B};
				textBoxColorColor.Text = string.Format("#{0}", BitConverter.ToString(rgb).Replace("-", string.Empty));
			}
				
		}

		private void WorldViewForm_Load(object sender, EventArgs e)
		{
			Int32 i;

			for (i = 0; i < SettingsManager.Instance.SettingsCount; i++)
			{
				comboBoxSettings.Items.Add(SettingsManager.Instance.SettingsName(i));
			}

			if (SettingsManager.Instance.SettingsName(SettingsManager.Instance.CurrentSettings) == "Default")
				buttonSettingsDelete.Enabled = false;
			else
				buttonSettingsDelete.Enabled = true;

			comboBoxSettings.SelectedIndex = SettingsManager.Instance.CurrentSettings;

			labelSpecialThanks.Text = Global.Credits;

			lblVersion.Text = "Version: " + GetVersion();

			tmrMapperProgress = new System.Timers.Timer();
			tmrMapperProgress.Elapsed += new ElapsedEventHandler(tmrMapperProgress_Tick);
			tmrMapperProgress.Enabled = false;
			tmrMapperProgress.Interval = 333;

			// If we've updated the software push it into the settings file.
			if (SettingsManager.Instance.TopVersion < Global.CurrentVersion)
				SettingsManager.Instance.TopVersion = Global.CurrentVersion;

			// These two lines are outside of SetupMainForm as they get called by event
			// handlers in there, but the handlers do not fire until the everything is fully
			// loaded.  So to keep them from getting double called they are here.
			ResourceManager.Instance.Custom = SettingsManager.Instance.CustomMarkers;

			SetupColorNames();
			SetupColorData();
			SetupImageLists();
			SetupColorButtons();

			SetupMainForm();
		}

		// This is everything that needs to get reset if the current settings have changed.
		private void SetupMainForm()
		{
			// These event handlers do nothing except ironically set SettingsManager back.
            checkBoxOfficialColors.Checked = SettingsManager.Instance.OfficialColors;
			checkBoxDrawWires.Checked = SettingsManager.Instance.DrawWires;
			checkBoxDrawWalls.Checked = SettingsManager.Instance.DrawWalls;
			checkBoxOpenImage.Checked = SettingsManager.Instance.OpenImage;
			checkBoxShowChestTypes.Checked = SettingsManager.Instance.ShowChestTypes;
			checkBoxFilterChests.Checked = SettingsManager.Instance.FilterChests;
			checkBoxShowChestItems.Checked = SettingsManager.Instance.ShowChestItems;
			checkBoxShowNormalItems.Checked = SettingsManager.Instance.ShowNormalItems;
			comboBoxCropImage.SelectedIndex = SettingsManager.Instance.CropImageUsing;

			// This event handler sets both ResourceManager.Custom but also calls
			// SetupImageLists.  SetupImageLists always needs to be called before
			// SetupMarkerList.
			checkBoxCustomMarkers.Checked = SettingsManager.Instance.CustomMarkers;

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


			// SetupImageLists does all boxes so it needs to come before
			// the Marker list and the Chest list.
			SetupMarkerListBox();
			SetupColorListBox();

			if (SettingsManager.Instance.FilterItemStates != null)
				ResetFilterLists();

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


		#region SelectWorld groupBox functions
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
			}
			else
			{
				labelWorldName.Text = "World Name: ";
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
		#endregion

		#region Settings groupBox functions
		private void buttonSettingsAddNew_Click(object sender, EventArgs e)
		{
			Boolean mainLoop = true;
			DialogResult result;
			FormEntryBox nameEntryBox = new FormEntryBox();

			nameEntryBox.FormText = "Add New Settings Preset";
			nameEntryBox.LabelText = "Enter the a name for a new settings preset:";

			while (mainLoop == true)
			{
				result = nameEntryBox.ShowDialog(this);

				if (result == DialogResult.Cancel)
					return;
				else
				{
					if (!SettingsManager.Instance.AddNewSettings(nameEntryBox.EntryItem))
					{
						MessageBox.Show(String.Format("Preset {0} already exists.", nameEntryBox.EntryItem),
							"Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					}
					else
					{
						mainLoop = false;
					}
				}
			}

			comboBoxSettings.Items.Add(nameEntryBox.EntryItem);
		}

		private void buttonSettingsDelete_Click(object sender, EventArgs e)
		{
			Int32 toDelete = comboBoxSettings.SelectedIndex;
			String deleteName = SettingsManager.Instance.SettingsName(toDelete);
			if ((deleteName == "Default") || (deleteName == String.Empty))
				return;

			SettingsManager.Instance.DeleteSettings(toDelete);
			comboBoxSettings.Items.RemoveAt(toDelete);

			if (toDelete >= comboBoxSettings.Items.Count)
				toDelete = comboBoxSettings.Items.Count - 1;

			comboBoxSettings.SelectedIndex = toDelete;
		}

		private void comboBoxSettings_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 index = comboBoxSettings.SelectedIndex;

			if ((index == SettingsManager.Instance.CurrentSettings) || (index < 0))
				return;

			if (SettingsManager.Instance.SettingsName(index) == "Default")
				buttonSettingsDelete.Enabled = false;
			else
				buttonSettingsDelete.Enabled = true;

			SettingsManager.Instance.CurrentSettings = comboBoxSettings.SelectedIndex;
			SetupMainForm();
		}
		#endregion

		#region Draw World tabPage functions
        private void checkBoxOfficialColors_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.Instance.OfficialColors = checkBoxOfficialColors.Checked;
        }

		private void checkBoxDrawWires_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.DrawWires = checkBoxDrawWires.Checked;
		}

		private void checkBoxDrawWalls_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.DrawWalls = checkBoxDrawWalls.Checked;
		}

		private void checkBoxOpenImage_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.OpenImage = checkBoxOpenImage.Checked;
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
				if (SettingsManager.Instance.ShowChestTypes)
					treeViewChestInformation.ImageList = chestImageList;

				buttonDrawWorld.Enabled = false;
				filterCount = SettingsManager.Instance.FilterItemStates.Count();

				groupBoxSelectWorld.Enabled = false;
				groupBoxImageOutput.Enabled = false;
				(this.tabPageMarkers as Control).Enabled = false;
				(this.tabPageWorldInformation as Control).Enabled = false;

				Point pt = this.Location;
				pt.X += (this.Size.Width / 2);
				pt.Y += (this.Size.Height / 2);

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
				progForm.Show(this);
			}
		}

		private void worker_GenerateMap(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker bw = (BackgroundWorker)sender;

			tmrMapperProgress.Start();

#if  (!DEBUG)
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
			//    pictureBox.BackgroundImageLayout = ImageLayout.
				 
				pictureBox.Image = mapper.CreatePreviewPNG(textBoxOutputFile.Text, bw);
				pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			  //  pictureBox.Update();
								   
				if (SettingsManager.Instance.OpenImage)
					System.Diagnostics.Process.Start(textBoxOutputFile.Text);
			}

			tmrMapperProgress.Stop();
#if  (!DEBUG)
			}
			catch (Exception ex)
			{
				tmrMapperProgress.Stop();
				FormWorldView.MessageBoxShow(ex.Message + Environment.NewLine + Environment.NewLine +
					"Details: " + ex.ToString() + Environment.NewLine + "Version: " + GetVersion(),
					"Error Opening World", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);


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

			mapper.Cleanup();
			GC.Collect();

			if (filterCount != SettingsManager.Instance.FilterItemStates.Count())
				ResetFilterLists();


			buttonDrawWorld.Enabled = true;

			groupBoxSelectWorld.Enabled = true;
			groupBoxImageOutput.Enabled = true;
			(this.tabPageMarkers as Control).Enabled = true;
			(this.tabPageWorldInformation as Control).Enabled = true;
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
			List<Chest> chests;
			List<TreeNode> chestNodes;

			chestNodes = new List<TreeNode>();

			if (this.mapper == null)
				return chestNodes.ToArray();

			if (SettingsManager.Instance.FilterChests == true)
			{
				chests = applyChestFilter(this.mapper.Chests);
			}
			else
			{
				chests = this.mapper.Chests;
			}

//			chestNodes = new List<TreeNode>(chests.Count);

			foreach (Chest c in chests)
			{
				TreeNode node = new TreeNode(string.Format("Chest at ({0},{1})", c.Coordinates.X, c.Coordinates.Y, c.ChestId));
				node.ImageIndex = (Int32) c.Type + 1;
				node.SelectedImageIndex = (Int32)c.Type + 1;

				foreach (Item i in c.Items)
				{
					node.Nodes.Add(i.ToString());
				}
				chestNodes.Add(node);
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
		#endregion

		#region Marker tabPage functions
		// This handles pressing space to check/uncheck boxes.
		private void treeViewMarkerList_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != ' ')
				return;

			if (treeViewMarkerList.SelectedNode != null)
			{
				treeViewMarkerList.SelectedNode.Checked = !treeViewMarkerList.SelectedNode.Checked;
				this.treeViewMarkerList_AfterCheck(this.treeViewMarkerList,
					new TreeViewEventArgs(treeViewMarkerList.SelectedNode, TreeViewAction.ByMouse));
			}

		}

		// This handles clicking on the check box part of the nodes.
		private void treeViewMarkerList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			Int32 xSpot = e.X - e.Node.Bounds.Left;

			if (treeViewMarkerList.ImageList != null)
			{
				if (xSpot < -34 || xSpot > -19)
					return;
			}
			else
			{
				if (xSpot < -16 || xSpot > -1)
					return;
			}

			e.Node.Checked = !e.Node.Checked;

			this.treeViewMarkerList_AfterCheck(this.treeViewMarkerList,
				new TreeViewEventArgs(e.Node, TreeViewAction.ByMouse));
		}

		// This takes a node that was clicked and goes down the list to update the parent to
		// it's correct state.
		private void treeViewMarkerList_updateParentNode(TreeNode e)
		{
			Int32 count, set;

			count = 0;
			set = 0;
			foreach (TreeNode n in e.Parent.Nodes)
			{
				count++;
				if (n.Checked == true)
					set++;
			}

			if (set == count)
				e.Parent.Checked = true;
			else
				e.Parent.Checked = false;

			if (set == 0)
				e.Parent.StateImageIndex = (Int32)CheckState.Unchecked;
			else if (set != count)
				e.Parent.StateImageIndex = (Int32)CheckState.Indeterminate;
			else
				e.Parent.StateImageIndex = (Int32)CheckState.Checked;

		}

		// This handles clicking a checkbox and correctly sets the child nodes, if there are any.
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
			Boolean nodeFlip;
			Int32 count, set;

			if (contextMenuStripListOperations.SourceControl is TreeView)
			{
				foreach (TreeNode p in (contextMenuStripListOperations.SourceControl as TreeView).Nodes)
				{
					count = 0;
					set = 0;
					foreach (TreeNode n in p.Nodes)
					{
						count++;
						nodeFlip = !n.Checked;
						SettingsManager.Instance.MarkerVisible(n.Text, nodeFlip);
						n.Checked = nodeFlip;

						if (nodeFlip == true)
							set++;
					}

					if (set == count)
						p.Checked = true;
					else
						p.Checked = false;

					if (set == 0)
						p.StateImageIndex = (Int32)CheckState.Unchecked;
					else if (set != count)
						p.StateImageIndex = (Int32)CheckState.Indeterminate;
					else
						p.StateImageIndex = (Int32)CheckState.Checked;
				}
			}
		}

		private void buttonCustomResources_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Global.ApplicationResourceDirectory);
		}

		private void checkBoxShowChestTypes_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.ShowChestTypes = checkBoxShowChestTypes.Checked;
		}

		private void checkBoxCustomMarkers_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.CustomMarkers = checkBoxCustomMarkers.Checked;
			ResourceManager.Instance.Custom = checkBoxCustomMarkers.Checked;
			SetupImageLists();
		}

		private void buttonResetCustomImages_Click(object sender, EventArgs e)
		{
			DialogResult res = MessageBox.Show("This will overwrite all files in the Resources directory.  If you have made changes to them they will be lost.  Continue?",
				"Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

			if (res == DialogResult.No)
				return;

			ResourceManager.Instance.ResetCustomMarkers();
			
			if (SettingsManager.Instance.CustomMarkers)
				SetupImageLists();
		}

		private void SetupImageLists()
		{
			Graphics graph;
			Bitmap bmp;
			if (markerThreeStateList == null)
			{
				markerThreeStateList = new ImageList();

				// Set up the images for the three-state box.
				bmp = new Bitmap(16, 16);
				graph = Graphics.FromImage(bmp);
				CheckBoxRenderer.DrawCheckBox(graph, new Point(1, 1), CheckBoxState.UncheckedNormal);
				markerThreeStateList.Images.Add(bmp);

				bmp = new Bitmap(16, 16);
				graph = Graphics.FromImage(bmp);
				CheckBoxRenderer.DrawCheckBox(graph, new Point(1, 1), CheckBoxState.CheckedNormal);
				markerThreeStateList.Images.Add(bmp);

				bmp = new Bitmap(16, 16);
				graph = Graphics.FromImage(bmp);
				CheckBoxRenderer.DrawCheckBox(graph, new Point(1, 1), CheckBoxState.MixedNormal);
				markerThreeStateList.Images.Add(bmp);
			}

			if (chestImageList == null)
				chestImageList = new ImageList();

			if (markerImageList == null)
				markerImageList = new ImageList();

			if (colorImageList == null)
			{
				colorImageList = new ImageList();

				// This is the empty one we'll use for parent objects.
				colorImageList.Images.Add(new Bitmap(16, 16));

				bmp = new Bitmap(16, 16);
				graph = Graphics.FromImage(bmp);

				graph.DrawRectangle(new Pen(Color.Black, 1), 1, 1, 14, 14);

				// We filled the image list with as many copies of the empty square bitmap as
				// we needed.
				for (Int32 i = 0; i < colorData.Count; i++)
				{
					colorImageList.Images.Add(new Bitmap(bmp));
				}
			}

			if (buttonImageList == null)
			{
				buttonImageList = new ImageList();
				buttonImageList.ImageSize = new Size(51, 23);

				bmp = new Bitmap(50, 22);

				buttonImageList.Images.Add(bmp);
				buttonImageList.Images.Add(new Bitmap(bmp));
			}

			markerImageList.Images.Clear();
			chestImageList.Images.Clear();

			// This is our blank image to keep chest list items from having images.
			bmp = new Bitmap(16, 16);
			chestImageList.Images.Add(bmp);

			foreach (KeyValuePair<String, List<MarkerInfo>> kvp in Global.Instance.Info.MarkerSets)
			{
                bmp = ResourceManager.Instance.GetMarker(kvp.Key);
                markerImageList.Images.Add(bmp);

                foreach (MarkerInfo mi in kvp.Value)
                {
                    bmp = ResourceManager.Instance.GetMarker(mi.markerImage);

				markerImageList.Images.Add(bmp);

                    if (kvp.Key == "Containers")
					chestImageList.Images.Add(bmp);
			}
		}
		}

		private void SetupMarkerListBox()
		{
			Dictionary<String, MarkerSettings> markerStates = SettingsManager.Instance.MarkerStates;
			Boolean[] expList = null;

			// This section sets up the Marker list box.  The first part draws the three
			// states of our checkboxes and then it populates the TreeView.
			Int32 index = 0;
			TreeNode node;

			if (treeViewMarkerList.Nodes.Count > 0)
			{
				expList = new Boolean[treeViewMarkerList.Nodes.Count];
				Int32 count = 0;

				foreach (TreeNode n in treeViewMarkerList.Nodes)
				{
					if (n.Parent == null)
						expList[count++] = n.IsExpanded;
				}
			}

			treeViewMarkerList.Nodes.Clear();

			treeViewMarkerList.StateImageList = markerThreeStateList;
			treeViewMarkerList.ImageList = markerImageList;

			markerNodes = new Dictionary<String, TreeNode>();

			Dictionary<String, TreeNode> parentNodes = new Dictionary<String, TreeNode>();

			foreach (KeyValuePair<String, List<MarkerInfo>> kvp in Global.Instance.Info.MarkerSets)
			{
				node = new TreeNode(kvp.Key);
				node.ImageIndex = index;
				node.SelectedImageIndex = index;

					parentNodes.Add(kvp.Key, node);

                foreach (MarkerInfo mi in kvp.Value)
				{
                    index++;
                    node = new TreeNode(mi.name);
                    node.ImageIndex = index;
                    node.SelectedImageIndex = index;
                    markerNodes.Add(mi.name, node);

					if (markerStates[mi.name].Drawing)
					{
						node.Checked = true;
						node.StateImageIndex = (Int32)CheckState.Checked;
					}
					else
					{
						node.Checked = false;
						node.StateImageIndex = (Int32)CheckState.Unchecked;
					}
				}

				index++;
			}

			// We parse the list again, this time to set the parent/child heirarchy up.
			// This makes it so that we do not have to have them in the exact order in the XML file.
			foreach(KeyValuePair<Int32, MarkerInfo> kvp in Global.Instance.Info.Markers)
			{
				parentNodes[kvp.Value.markerSet].Nodes.Add(markerNodes[kvp.Value.name]);
			}

			// Now that it is set up so all child nodes are in the right spot we add them to the
			// treeNode and force it to update the checked state.
			foreach(KeyValuePair<String, TreeNode> kvp in parentNodes)
			{
				treeViewMarkerList.Nodes.Add(kvp.Value);
				treeViewMarkerList_updateParentNode(kvp.Value.Nodes[0]);
			}

			if (expList != null)
			{
				Int32 count = 0;

				foreach (TreeNode n in treeViewMarkerList.Nodes)
				{
					if ((n.Parent == null) && (expList[count++] == true))
						n.Expand();
				}
			}



		}
		#endregion

		#region Color tabPage functions
		private void treeViewColorList_AfterSelect(object sender, TreeViewEventArgs e)
		{
			Global.Instance.SkipEvents = true;

			if ((e.Node == null) || (e.Node.Parent == null))
			{
				radioButtonColorDefault.Checked = true;
				comboBoxColorName.SelectedIndex = -1;
				textBoxColorColor.Text = String.Empty;
				groupBoxColor.Enabled = false;
				Global.Instance.SkipEvents = false;
				return;
			}

			groupBoxColor.Enabled = true;

			ColorListDataNode data = colorData.GetNode(e.Node.Text, e.Node.Parent.Text);
			if (data == null)
			{
				Global.Instance.SkipEvents = false;
				return;
			}

			if (data.defaultColor == data.currentColor)
				radioButtonColorDefault.Checked = true;
			else if (data.currentColorName != String.Empty)
				radioButtonColorPreset.Checked = true;
			else
				radioButtonColorColor.Checked = true;

			if (data.currentColorName == String.Empty)
				comboBoxColorName.SelectedIndex = -1;
			else
			{
				if (comboBoxColorName.Items.Contains(data.currentColorName))
					comboBoxColorName.SelectedItem = data.currentColorName;
				else
					comboBoxColorName.SelectedIndex = -1;
			}

			textBoxColorColor.Text = Global.ToColorString(data.currentColor);
			Global.Instance.SkipEvents = false;
		}

		private void radioButtonColorDefault_CheckedChanged(object sender, EventArgs e)
		{
			if (Global.Instance.SkipEvents)
				return;

			//MessageBox.Show("radioButtonColorDefault_CheckedChanged");
		}

		private void radioButtonColorName_CheckedChanged(object sender, EventArgs e)
		{
			if (Global.Instance.SkipEvents)
				return;

			//MessageBox.Show("radioButtonColorName_CheckedChanged");
		}

		private void radioButtonColorColor_CheckedChanged(object sender, EventArgs e)
		{
			if (Global.Instance.SkipEvents)
				return;

			//MessageBox.Show("radioButtonColorColor_CheckedChanged");
		}

		private void comboBoxColorName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Global.Instance.SkipEvents)
				return;

			MessageBox.Show("comboBoxColorName_SelectedIndexChanged");
		}

		private void textBoxColorColor_TextChanged(object sender, EventArgs e)
		{
			if (Global.Instance.SkipEvents)
				return;
            
		}

		private void SetupColorButtons()
		{
			buttonColorColor.ImageList = buttonImageList;
			buttonColorColor.ImageIndex = 0;

			buttonColorNameColor.ImageList = buttonImageList;
			buttonColorNameColor.ImageIndex = 1;
		}

		private void SetupColorNames()
		{
			Dictionary<String, ColorInfo> colors = Global.Instance.Info.Colors;

			comboBoxColorName.Items.Clear();
			comboBoxColorNamesName.Items.Clear();

			foreach (KeyValuePair<String, ColorInfo> kvp in colors)
			{
				comboBoxColorName.Items.Add(kvp.Key);
				comboBoxColorNamesName.Items.Add(kvp.Key);
			}

			comboBoxColorNamesName.SelectedIndex = 0;
		}

		private void SetupColorData()
		{
			treeViewColorList.Nodes.Clear();
			treeViewColorList.ImageList = colorImageList;

			if (colorData == null)
				colorData = new ColorListData();
			else
				colorData.Clear();

			String parent = "Tiles";
			Int32 id = 1;

			foreach (KeyValuePair<Int32, TileInfo> kvp in Global.Instance.Info.Tiles)
			{
				colorData.AddNewNode(kvp.Value.name, parent, kvp.Value.color,
					kvp.Value.colorName, id + kvp.Value.tileImage);
			}

			id += Global.Instance.Info.Tiles.Count - 1;

			parent = "Walls";

			foreach (KeyValuePair<Int32, WallInfo> kvp in Global.Instance.Info.Walls)
			{
				colorData.AddNewNode(kvp.Value.name, parent, kvp.Value.color,
					kvp.Value.colorName, id + kvp.Value.wallImage);
			}

			id += Global.Instance.Info.Walls.Count + 1;

			foreach (KeyValuePair<String, List<SpecialObjectInfo>> kvp in Global.Instance.Info.SpecialObjects)
			{
				parent = kvp.Key + "s";

				foreach (SpecialObjectInfo soi in kvp.Value)
				{
					colorData.AddNewNode(soi.name, parent, soi.color,
						soi.colorName, id);

					id++;
				}
			}
		}

		private void SetupColorListBox()
		{
			Boolean[] expList = null;
			TreeNode node, parent;

			if (treeViewColorList.Nodes.Count > 0)
			{
				expList = new Boolean[treeViewColorList.Nodes.Count];
				Int32 count = 0;

				foreach (TreeNode n in treeViewColorList.Nodes)
				{
					if (n.Parent == null)
						expList[count++] = n.IsExpanded;
				}
			}

			treeViewColorList.Nodes.Clear();
			treeViewColorList.ImageList = colorImageList;

			foreach (KeyValuePair<String, Dictionary<String, ColorListDataNode>> kvp in colorData.Data)
			{
				parent = new TreeNode(kvp.Key);
				parent.ImageIndex = -1;
				parent.SelectedImageIndex = -1;
				treeViewColorList.Nodes.Add(parent);

				foreach (KeyValuePair<String, ColorListDataNode> kvp2 in kvp.Value)
				{
					node = new TreeNode(kvp2.Key);
					node.SelectedImageIndex = kvp2.Value.nodeId;
					node.ImageIndex = node.SelectedImageIndex;

					UpdateColorImage(node.ImageIndex, kvp2.Value.currentColor);

					parent.Nodes.Add(node);
				}
			}

			if (expList != null)
			{
				Int32 count = 0;

				foreach (TreeNode n in treeViewColorList.Nodes)
				{
					if ((n.Parent == null) && (expList[count++] == true))
						n.Expand();
				}
			}
		}

		private void UpdateColorImage(Int32 useImage, Color newColor)
		{
			if ((useImage < 0) || (useImage >= colorImageList.Images.Count))
				return;

			Image image = colorImageList.Images[useImage];
			Graphics graph = Graphics.FromImage(image);

			graph.FillRectangle(new SolidBrush(newColor), 2, 2, 13, 13);

			colorImageList.Images[useImage] = image;
		}

		private void UpdateColorButton(Int32 useButton, Color newColor)
		{
			if ((useButton < 0) || (useButton >= buttonImageList.Images.Count))
				return;

			Image image = buttonImageList.Images[useButton];
			Graphics graph = Graphics.FromImage(image);

			graph.FillRectangle(new SolidBrush(newColor), 0, 0, image.Width - 1, image.Height - 1);
			buttonImageList.Images[useButton] = image;
		}
		#endregion

		#region ChestFinder tabPage functions
		private void checkBoxFilterChests_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.FilterChests = checkBoxFilterChests.Checked;

			lstAvailableItems.Enabled = checkBoxFilterChests.Checked;
			lstFilteredItems.Enabled = checkBoxFilterChests.Checked;
			filterUpdated = true;
		}

		private void checkBoxShowChestItems_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.ShowChestItems = checkBoxShowChestItems.Checked;

			ResetFilterLists();
		}

		private void checkBoxShowNormalItems_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.ShowNormalItems = checkBoxShowNormalItems.Checked;

			ResetFilterLists();
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
			lstAvailableItems.SelectedIndex = Math.Min(selected, lstAvailableItems.Items.Count - 1);

			SettingsManager.Instance.FilterItem(si, true);
			filterUpdated = true;

			buttonMoveAllToAvailable.Enabled = (lstFilteredItems.Items.Count != 0);
			buttonMoveAllToFiltered.Enabled = (lstAvailableItems.Items.Count != 0);
		}
	
		private void lstFilteredItems_KeyDown(object sender, KeyEventArgs e)
		{
			ItemInfo ii;
			String si;
			int selected = lstFilteredItems.SelectedIndex;

			if (selected == -1) return;

			if (e.KeyCode == Keys.Delete)
			{
				si = lstFilteredItems.SelectedItem.ToString();

				ii = Global.Instance.Info.GetItem(si);

				if (ii.foundIn == "Chest")
				{
					if (SettingsManager.Instance.ShowChestItems)
						lstAvailableItems.Items.Add(si);
				} else if (SettingsManager.Instance.ShowNormalItems)
					lstAvailableItems.Items.Add(si);

				lstFilteredItems.Items.Remove(lstFilteredItems.SelectedItem);
				lstFilteredItems.SelectedIndex = Math.Min(selected, lstFilteredItems.Items.Count - 1);

				SettingsManager.Instance.FilterItem(si, false);
				filterUpdated = true;

				buttonMoveAllToAvailable.Enabled = (lstFilteredItems.Items.Count != 0);
				buttonMoveAllToFiltered.Enabled = (lstAvailableItems.Items.Count != 0);
			}
		}

		private void buttonMoveAllToFiltered_Click(object sender, EventArgs e)
		{
			List<String> filter;

			if (lstAvailableItems.Items.Count == 0)
				return;

			filter = SettingsManager.Instance.FilterItemStates;

			foreach (String s in lstAvailableItems.Items)
			{
				filter.Add(s);
			}

			ResetFilterLists();
		}

		private void buttonMoveAllToAvailable_Click(object sender, EventArgs e)
		{
			if (lstFilteredItems.Items.Count == 0)
				return;

			SettingsManager.Instance.FilterItemStates.Clear();

			ResetFilterLists();
		}

		private void ResetFilterLists()
		{
			String lstItem;
			List<String> filterList;

			Boolean showInChest = SettingsManager.Instance.ShowChestItems;
			Boolean showNormal = SettingsManager.Instance.ShowNormalItems;

			lstFilteredItems.Items.Clear();
			lstAvailableItems.Items.Clear();

			filterList = SettingsManager.Instance.FilterItemStates;

			foreach (KeyValuePair<String, ItemInfo> kvp in Global.Instance.Info.Items)
			{
				lstItem = kvp.Key;

				if (filterList.Contains(kvp.Key))
				{
					lstFilteredItems.Items.Add(kvp.Key);
				}
				else
				{
					if (kvp.Value.foundIn == "Chest")
					{
						if (showInChest == false)
							continue;
					}
					else if (showNormal == false)
					{
						continue;
					}

					lstAvailableItems.Items.Add(lstItem);
				}
			}

			buttonMoveAllToAvailable.Enabled = (lstFilteredItems.Items.Count != 0);
			buttonMoveAllToFiltered.Enabled = (lstAvailableItems.Items.Count != 0);

			filterUpdated = true;
		}
		#endregion

		#region World Information tabPage functions
		private void buttonLoadInformation_Click(object sender, EventArgs e)
		{
			if (checkValidPaths(false))
			{
				treeViewChestInformation.ImageList = null;

				filterCount = SettingsManager.Instance.FilterItemStates.Count;

				buttonDrawWorld.Enabled = false;

				groupBoxSelectWorld.Enabled = false;
				groupBoxImageOutput.Enabled = false;
				(this.tabPageMarkers as Control).Enabled = false;
				(this.tabPageWorldInformation as Control).Enabled = false;

				Point pt = this.Location;
				pt.X += (this.Size.Width / 2);
				pt.Y += (this.Size.Height / 2);

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
				progForm.Show(this);
			}
		}

		private void radioButtonSortByNone_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.SortChestsBy = 0;

			treeViewChestInformation.TreeViewNodeSorter = null;
			treeViewChestInformation.Sorted = false;

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

		private List<Chest> applyChestFilter(List<Chest> chests)
		{
			List<String> itemFilters = SettingsManager.Instance.FilterItemStates;
			List<Chest> filteredChests = new List<Chest>();

			foreach(Chest c in chests)
			{
				// If we are checking the chest node itself.
				foreach (Item item in c.Items)
				{
					if (itemFilters.Contains(item.Name))
					{
						filteredChests.Add(c);
						break;
					}
				}
			}

			return filteredChests;
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

		private void saveToTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			DialogResult result;

			if (treeViewChestInformation.Nodes.Count == 0)
			{
				MessageBox.Show("No chests to save!");
				return;
			}

			dialog.FileName = String.Format("{0}Chests.txt",
				Path.GetFileNameWithoutExtension(comboBoxWorldFilePath.SelectedItem.ToString()));
			dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			dialog.FilterIndex = 1;
			dialog.RestoreDirectory = true;

			result = dialog.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			SaveChestsAsText(dialog.FileName);
		}

		private void saveToCSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			DialogResult result;

			if (treeViewChestInformation.Nodes.Count == 0)
			{
				MessageBox.Show("No chests to save!");
				return;
			}

			dialog.FileName = String.Format("{0}Chests.csv",
				Path.GetFileNameWithoutExtension(comboBoxWorldFilePath.SelectedItem.ToString()));
			dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
			dialog.FilterIndex = 1;
			dialog.RestoreDirectory = true;

			result = dialog.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			SaveChestsAsCSV(dialog.FileName);
		}

		private void saveToXMLtToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			DialogResult result;

			if (treeViewChestInformation.Nodes.Count == 0)
			{
				MessageBox.Show("No chests to save!");
				return;
			}

			dialog.FileName = String.Format("{0}Chests.xml",
				Path.GetFileNameWithoutExtension(comboBoxWorldFilePath.SelectedItem.ToString()));
			dialog.Filter = "XML files (*.XML)|*.xml|All files (*.*)|*.*";
			dialog.FilterIndex = 1;
			dialog.RestoreDirectory = true;

			result = dialog.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			SaveChestsAsXML(dialog.FileName);
		}

		private void SaveChestsAsText(String textFile)
		{
			List<Chest> chests;
			String chestType;
			StreamWriter writer = null;
			FileStream stream;

			try
			{
			stream = new FileStream(textFile, FileMode.Create, FileAccess.Write);
			writer = new StreamWriter(stream);

			if (SettingsManager.Instance.FilterChests)
			{
				chests = applyChestFilter(this.mapper.Chests);
			}
			else
			{
				chests = this.mapper.Chests;
			}

			if (radioButtonSortByX.Checked == true)
			{
				chests.Sort(new ChestListComparerX());
			}
			else if (radioButtonSortByY.Checked == true)
			{
				chests.Sort(new ChestListComparerY());
			}

			foreach (Chest c in chests)
			{
				switch (c.Type)
				{
					case ChestType.GoldChest:
						chestType = "Gold Chest";
						break;
					case ChestType.LockedGoldChest:
						chestType = "Gold Chest (Locked)";
						break;
					case ChestType.ShadowChest:
						chestType = "Shadow Chest";
						break;
					case ChestType.LockedShadowChest:
						chestType = "Shadow Chest (Locked)";
						break;
					case ChestType.Barrel:
						chestType = "Barrel";
						break;
					case ChestType.TrashCan:
						chestType = "Trash Can";
						break;
					case ChestType.Chest:
					default:
						chestType = "Chest";
						break;
				}

				if (c.Items.Count == 0)
					chestType = "Empty " + chestType;

				writer.WriteLine(String.Format("{0} at {1}, {2}", chestType, c.Coordinates.X, c.Coordinates.Y));

				foreach (Item i in c.Items)
				{
					writer.WriteLine(String.Format("  #{0} - {1} of {2}", i.Id + 1, i.Count, i.Name));
				}

				writer.WriteLine();
			}

			writer.Close();
			MessageBox.Show("Chests saved to " + Path.GetFileName(textFile));

			}
			catch (IOException e)
			{
				if (writer != null)
					writer.Close();

				MessageBox.Show(e.Message,
								 "Error writing to Textfile", MessageBoxButtons.OK, MessageBoxIcon.Error);

				if (File.Exists(textFile))
					File.Delete(textFile);
			}
		}

		private void SaveChestsAsCSV(String textFile)
		{
			List<Chest> chests;
			String chestType;
			StreamWriter writer = null;
			FileStream stream;

			try
			{
			stream = new FileStream(textFile, FileMode.Create, FileAccess.Write);
			writer = new StreamWriter(stream);

			if (SettingsManager.Instance.FilterChests)
			{
				chests = applyChestFilter(this.mapper.Chests);
			}
			else
			{
				chests = this.mapper.Chests;
			}

			if (radioButtonSortByX.Checked == true)
			{
				chests.Sort(new ChestListComparerX());
			}
			else if (radioButtonSortByY.Checked == true)
			{
				chests.Sort(new ChestListComparerY());
			}

			String header = "Chest Type, X Coordinate, Y Coordinate";

			for (Int32 i = 0; i < Global.ChestMaxItems; i++)
				header += String.Format(", Item {0} Count, Item {0} Name", i + 1);

			writer.WriteLine(header);

			foreach (Chest c in chests)
			{
				switch (c.Type)
				{
					case ChestType.GoldChest:
						chestType = "Gold Chest";
						break;
					case ChestType.LockedGoldChest:
						chestType = "Gold Chest (Locked)";
						break;
					case ChestType.ShadowChest:
						chestType = "Shadow Chest";
						break;
					case ChestType.LockedShadowChest:
						chestType = "Shadow Chest (Locked)";
						break;
					case ChestType.Barrel:
						chestType = "Barrel";
						break;
					case ChestType.TrashCan:
						chestType = "Trash Can";
						break;
					case ChestType.Chest:
					default:
						chestType = "Chest";
						break;
				}

				if (c.Items.Count == 0)
					chestType = "Empty " + chestType;

				writer.Write(String.Format("{0}, {1}, {2}", chestType, c.Coordinates.X, c.Coordinates.Y));

				List<Item>.Enumerator itemEnum = c.Items.GetEnumerator();
				itemEnum.MoveNext();
				Item i = itemEnum.Current;

				for (Int32 j = 0; j < Global.ChestMaxItems; j++)
				{
					if (i == null || i.Id != j)
					{
						writer.Write(", , ");
					}
					else
					{
						writer.Write(String.Format(", {0}, {1}", i.Count, i.Name));
						itemEnum.MoveNext();
						i = itemEnum.Current;
					}
				}

				writer.Write(Environment.NewLine);
			}

			writer.Close();
			MessageBox.Show("Chests saved to " + Path.GetFileName(textFile));
			}
			catch (IOException e)
			{
				if (writer != null)
					writer.Close();

				MessageBox.Show(e.Message,
								 "Error writing to Textfile", MessageBoxButtons.OK, MessageBoxIcon.Error);

				if (File.Exists(textFile))
					File.Delete(textFile);
			}
		}

		private void SaveChestsAsXML(String textFile)
		{
			List<Chest> chests;
			String chestType;
			StreamWriter writer = null;
			FileStream stream;

			try
			{
			stream = new FileStream(textFile, FileMode.Create, FileAccess.Write);
			writer = new StreamWriter(stream);

			if (SettingsManager.Instance.FilterChests)
			{
				chests = applyChestFilter(this.mapper.Chests);
			}
			else
			{
				chests = this.mapper.Chests;
			}

			if (radioButtonSortByX.Checked == true)
			{
				chests.Sort(new ChestListComparerX());
			}
			else if (radioButtonSortByY.Checked == true)
			{
				chests.Sort(new ChestListComparerY());
			}

			writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
			writer.WriteLine(String.Format("<chests worldName=\"{0}\">", worldNames[comboBoxWorldFilePath.SelectedItem.ToString()]));

			foreach (Chest c in chests)
			{
				switch (c.Type)
				{
					case ChestType.GoldChest:
						chestType = "Gold Chest";
						break;
					case ChestType.LockedGoldChest:
						chestType = "Gold Chest (Locked)";
						break;
					case ChestType.ShadowChest:
						chestType = "Shadow Chest";
						break;
					case ChestType.LockedShadowChest:
						chestType = "Shadow Chest (Locked)";
						break;
					case ChestType.Barrel:
						chestType = "Barrel";
						break;
					case ChestType.TrashCan:
						chestType = "Trash Can";
						break;
					case ChestType.Chest:
					default:
						chestType = "Chest";
						break;
				}

				writer.WriteLine(String.Format("  <chest type=\"{0}\" xPosition=\"{1}\" yPosition=\"{2}\"{3}",
					chestType, c.Coordinates.X, c.Coordinates.Y,
					(c.Items.Count == 0) ? " />" : ">"));

				foreach (Item i in c.Items)
				{
					writer.WriteLine(String.Format("    <item id=\"{0}\" name=\"{1}\" count=\"{2}\" />",
						i.Id, i.Name, i.Count));
				}

				if (c.Items.Count != 0)
					writer.WriteLine("  </chest>");
			}
			writer.WriteLine("</chests>");

			writer.Close();
			MessageBox.Show("Chests saved to " + Path.GetFileName(textFile));
			}
			catch (IOException e)
			{
				if (writer != null)
					writer.Close();

				MessageBox.Show(e.Message,
								 "Error writing to Textfile", MessageBoxButtons.OK, MessageBoxIcon.Error);

				if (File.Exists(textFile))
					File.Delete(textFile);
			}
		}
		#endregion

		#region About tabPage functions
		private void linkLabelHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://moreterra.codeplex.com/");
		}

		private String GetVersion()
		{
			string ver = Application.ProductVersion;

			while (ver.Length > 3 && ver.Substring(ver.Length - 2) == ".0")
			{
				ver = ver.Substring(0, ver.Length - 2);
			}

			return ver;
		}
		#endregion

		#region MessageBox delegate calls
		// This whole section is for handling MessageBox and other DialogBox calls.
		// They like to stay disconnected from the main form because they are on a seperate
		// thread.  This means not only that we can not set the IWin32Window owner target
		// on them without causing a cross-thread issue but they are not considered Modal
		// so the main window still has focus and can be moved/played with.
		// I made these calls all static in order to make it so that they can be called
		// from anywhere in the program without having to putz around with passing the
		// FormWorldView instance.
		private static FormWorldView selfReference;
		public static FormWorldView Form
		{
			get
			{
				return selfReference;
			}
			set
			{
				selfReference = value;
			}
		}

		public static DialogResult MessageBoxShow(String text, String caption, MessageBoxButtons buttons,
			MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			if (FormWorldView.Form.InvokeRequired)
			{
				MessageBoxShowFullDelegate del = new MessageBoxShowFullDelegate(MessageBoxShow);
				return (DialogResult) FormWorldView.Form.Invoke(del, new Object[] 
				{ text, caption, buttons, icon, defaultButton });
			}

			return MessageBox.Show(FormWorldView.Form, text, caption, buttons, icon, defaultButton);
		}

		public static DialogResult MessageBoxShow(String text)
		{
			if (FormWorldView.Form.InvokeRequired)
			{
				MessageBoxShowDelegate del = new MessageBoxShowDelegate(MessageBoxShow);
				return (DialogResult) FormWorldView.Form.Invoke(del, new Object[] { text });
			}

			return MessageBox.Show(FormWorldView.Form, text);
		}

		public static DialogResult MessageBoxWithCheckBoxShow(FormMessageBoxWithCheckBox box)
		{
			if (FormWorldView.Form.InvokeRequired)
			{
				MessageBoxWithCheckBoxShowDelegate del = 
					new MessageBoxWithCheckBoxShowDelegate(MessageBoxWithCheckBoxShow);
				return (DialogResult) FormWorldView.Form.Invoke(del, new Object[] { box });
			}

			return box.ShowDialog(FormWorldView.Form);
		}
		#endregion

		private void comboBoxCropImage_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.CropImageUsing = comboBoxCropImage.SelectedIndex;
		}

	}
}