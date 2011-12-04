using System;
using System.Drawing;
using System.Windows.Forms;

namespace MoreTerra
{
	public partial class FormEntryBox : Form
	{
		private String fText;
		private String lText;
		private String iText;

		#region Constructors
		public FormEntryBox()
		{
			this.Icon = Properties.Resources.Cannon;
			this.ShowInTaskbar = false;
			InitializeComponent();
		}
		#endregion

		#region Event Handlers
		private void FormEntryBox_Load(object sender, EventArgs e)
		{
			Point pt;
			Size size;
			this.Text = fText;
			this.labelMessage.Text = lText;
			textBoxItem.Text = iText;

			// Set the box to the center of the window.
			pt = this.Owner.Location;
			size = this.Owner.Size;

			pt.X = pt.X + (size.Width / 2) - (this.Size.Width / 2);
			pt.Y = pt.Y + (size.Height / 2) - (this.Size.Height / 2);

			this.Location = pt;
		}

		private void FormEntryBox_Shown(object sender, EventArgs e)
		{
			textBoxItem.Focus();
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
		#endregion

		#region GetSet Functions
		public String EntryItem
		{
			get
			{
				return textBoxItem.Text;
			}
			set
			{
				iText = value;
			}
		}

		public String LabelText
		{
			set
			{
				lText = value;
			}
		}

		public String FormText
		{
			set
			{
				fText = value;
			}
		}
		#endregion


	}
}
