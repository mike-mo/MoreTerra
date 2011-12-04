using System;
using System.Drawing;
using System.Windows.Forms;

namespace MoreTerra
{
	public partial class FormMessageBoxWithCheckBox : Form
	{
		private String lText;
		private String cbText;
		private String tText;
		private Point sPoint;

		public FormMessageBoxWithCheckBox(String labelText, String checkBoxText, String titleText)
		{
			lText = labelText;
			cbText = checkBoxText;
			tText = titleText;
			sPoint.X = -1;
			sPoint.Y = -1;

			this.ShowInTaskbar = false;
			InitializeComponent();

			this.Icon = Properties.Resources.Cannon;
		}

		private void FormMessageBoxWithCheckBox_Load(object sender, EventArgs e)
		{
			Point pt = new Point(0, 0);
			Size size;

			labelDialogText.Text = lText;
			checkBoxDialogItem.Text = cbText;
			Text = tText;

			// Set the box to the center of the window.
			if ((sPoint.X != -1) && (sPoint.Y != -1))
			{
				pt.X = sPoint.X - (this.Size.Width / 2);
				pt.Y = sPoint.Y - (this.Size.Height / 2);
				this.Location = pt;
			} else if (this.Owner != null)
			{
				pt = this.Owner.Location;
				size = this.Owner.Size;

				pt.X = pt.X + (size.Width / 2) - (this.Size.Width / 2);
				pt.Y = pt.Y + (size.Height / 2) - (this.Size.Height / 2);

				this.Location = pt;
			}
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

		public Point parentCenter
		{
			set
			{
				sPoint = value;
			}
		}
	}
}
