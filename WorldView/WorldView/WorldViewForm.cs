using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

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
        }

        private void WorldViewForm_Load(object sender, EventArgs e)
        {

        }

        private void WorldViewForm_OpenWorldClicked(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Terraria World (*.wld)|*.wld|Terraria Backup World (*.wld.bak)|*.wld.bak";
            dialog.Title = "Select World File";
            string filePath = (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : string.Empty;

            if (filePath == string.Empty)
            {
                return;
            }
            this.worldFilePathTextBox.Text = filePath;

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

        private void outputFileBrowseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG (*.png)|*.png";
            dialog.Title = "Select World File";
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
            mapper.CloseWorld();
            outputFileTextBox.Text = string.Empty;
            worldFilePathTextBox.Text = string.Empty;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}