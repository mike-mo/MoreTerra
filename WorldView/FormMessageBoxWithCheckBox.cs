using System;
using System.Windows.Forms;

namespace MoreTerra
{
	public partial class FormMessageBoxWithCheckBox : Form
	{
		private String lText;
		private String cbText;
		private String tText;

		public FormMessageBoxWithCheckBox(String labelText, String checkBoxText, String titleText)
		{
			lText = labelText;
			cbText = checkBoxText;
			tText = titleText;
			InitializeComponent();
		}

		private void FormMessageBoxWithCheckBox_Load(object sender, EventArgs e)
		{
			labelDialogText.Text = lText;
			checkBoxDialogItem.Text = cbText;
			Text = tText;
		}

		private void buttonYes_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void buttonNo_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		public Boolean checkBoxChecked
		{
			get
			{
				return checkBoxDialogItem.Checked;
			}
			set
			{
				checkBoxDialogItem.Checked = value;
			}
		}

		public String labelText
		{
			get
			{
				return labelDialogText.Text;
			}
			set
			{
				lText = value;
			}
		}

		public String titleText
		{
			get
			{
				return Text;
			}
			set
			{
				tText = value;
			}
		}

		public String checkBoxText
		{
			set
			{
				cbText = value;
			}
		}
	}
}
