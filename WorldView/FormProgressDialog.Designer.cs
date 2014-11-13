namespace MoreTerra
{
	partial class FormProgressDialog
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
			this.progressBarTotal = new System.Windows.Forms.ProgressBar();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxOutput = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// progressBarTotal
			// 
			this.progressBarTotal.Location = new System.Drawing.Point(12, 157);
			this.progressBarTotal.Name = "progressBarTotal";
			this.progressBarTotal.Size = new System.Drawing.Size(262, 19);
			this.progressBarTotal.TabIndex = 0;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(106, 182);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// textBoxOutput
			// 
			this.textBoxOutput.Location = new System.Drawing.Point(12, 12);
			this.textBoxOutput.Multiline = true;
			this.textBoxOutput.Name = "textBoxOutput";
			this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxOutput.Size = new System.Drawing.Size(262, 139);
			this.textBoxOutput.TabIndex = 2;
			// 
			// FormProgressDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(286, 217);
			this.Controls.Add(this.textBoxOutput);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.progressBarTotal);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProgressDialog";
			this.Text = "FormProgressDialog";
			this.Load += new System.EventHandler(this.FormProgressDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBarTotal;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxOutput;
	}
}