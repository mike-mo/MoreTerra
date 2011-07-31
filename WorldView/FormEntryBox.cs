using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoreTerra
{
	public partial class FormEntryBox : Form
	{
		public String getCustomItem()
		{
			return textBoxItem.Text;
		}

		public FormEntryBox()
		{
			InitializeComponent();
		}

		private void FormEntryBox_Load(object sender, EventArgs e)
		{
			textBoxItem.Text = "";
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (textBoxItem.Text == "")
			{
				DialogResult = DialogResult.Cancel;
				return;
			}

			DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}


	}
}
