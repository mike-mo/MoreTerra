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
	public partial class FormProgressDialog : Form
	{
		private Boolean cancel;
		private String name;
		private BackgroundWorker bw;
		private Boolean success;

		public FormProgressDialog(String titleName, Boolean allowsCancel, BackgroundWorker worker)
		{
			bw = worker;
			cancel = allowsCancel;
			name = titleName;

			InitializeComponent();
		}

		private void FormProgressDialog_Load(object sender, EventArgs e)
		{
			Text = name;

			buttonCancel.Enabled = cancel;
			textBoxOutput.Text = "";
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			// ?
			// Somehow I need to pass back that we need to cancel.
			// Although we could handle it ourselves.
		}


		public void worker_Completed(object sender, RunWorkerCompletedEventArgs e)
		{

			if (e.Cancelled)
				success = false;

			success = true;

			this.Close();
		}

		public void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage > progressBarTotal.Value)
				progressBarTotal.Value = e.ProgressPercentage;

			if (e.UserState != null)
			{
				textBoxOutput.Text += ((String)e.UserState + Environment.NewLine);
				textBoxOutput.Select(textBoxOutput.TextLength, 0);
				textBoxOutput.ScrollToCaret();
			}
		}

		public Boolean Success
		{
			get
			{
				return success;
			}
		}
	}
}
