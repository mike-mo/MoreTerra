namespace MoreTerra
{
	partial class FormEntryBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelMessage = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.textBoxItem = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// labelMessage
			// 
			this.labelMessage.Location = new System.Drawing.Point(12, 18);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(320, 45);
			this.labelMessage.TabIndex = 0;
			this.labelMessage.Text = "labelMessage";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(187, 105);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(72, 105);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// textBoxItem
			// 
			this.textBoxItem.Location = new System.Drawing.Point(15, 66);
			this.textBoxItem.Name = "textBoxItem";
			this.textBoxItem.Size = new System.Drawing.Size(317, 20);
			this.textBoxItem.TabIndex = 0;
			this.textBoxItem.Text = "textBoxItem";
			// 
			// FormEntryBox
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(344, 140);
			this.Controls.Add(this.textBoxItem);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.labelMessage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEntryBox";
			this.Text = "Dialog Text";
			this.Load += new System.EventHandler(this.FormEntryBox_Load);
			this.Shown += new System.EventHandler(this.FormEntryBox_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TextBox textBoxItem;
	}
}