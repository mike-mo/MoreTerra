namespace MoreTerra
{
	partial class FormMessageBoxWithCheckBox
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
			this.labelDialogText = new System.Windows.Forms.Label();
			this.checkBoxDialogItem = new System.Windows.Forms.CheckBox();
			this.buttonNo = new System.Windows.Forms.Button();
			this.buttonYes = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelDialogText
			// 
			this.labelDialogText.Location = new System.Drawing.Point(22, 14);
			this.labelDialogText.Name = "labelDialogText";
			this.labelDialogText.Size = new System.Drawing.Size(317, 71);
			this.labelDialogText.TabIndex = 1;
			this.labelDialogText.Text = "labelWorldWarning";
			// 
			// checkBoxDialogItem
			// 
			this.checkBoxDialogItem.AutoSize = true;
			this.checkBoxDialogItem.Location = new System.Drawing.Point(25, 88);
			this.checkBoxDialogItem.Name = "checkBoxDialogItem";
			this.checkBoxDialogItem.Size = new System.Drawing.Size(189, 17);
			this.checkBoxDialogItem.TabIndex = 3;
			this.checkBoxDialogItem.Text = "Do not show for this version again.";
			this.checkBoxDialogItem.UseVisualStyleBackColor = true;
			// 
			// buttonNo
			// 
			this.buttonNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonNo.Location = new System.Drawing.Point(183, 116);
			this.buttonNo.Name = "buttonNo";
			this.buttonNo.Size = new System.Drawing.Size(75, 23);
			this.buttonNo.TabIndex = 2;
			this.buttonNo.Text = "No";
			this.buttonNo.UseVisualStyleBackColor = true;
			this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
			// 
			// buttonYes
			// 
			this.buttonYes.Location = new System.Drawing.Point(81, 116);
			this.buttonYes.Name = "buttonYes";
			this.buttonYes.Size = new System.Drawing.Size(75, 23);
			this.buttonYes.TabIndex = 0;
			this.buttonYes.Text = "Yes";
			this.buttonYes.UseVisualStyleBackColor = true;
			this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
			// 
			// FormMessageBoxWithCheckBox
			// 
			this.AcceptButton = this.buttonYes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonNo;
			this.ClientSize = new System.Drawing.Size(351, 151);
			this.Controls.Add(this.buttonYes);
			this.Controls.Add(this.checkBoxDialogItem);
			this.Controls.Add(this.buttonNo);
			this.Controls.Add(this.labelDialogText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMessageBoxWithCheckBox";
			this.Text = "MessageBoxWithCheckBox";
			this.Load += new System.EventHandler(this.FormMessageBoxWithCheckBox_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelDialogText;
		private System.Windows.Forms.CheckBox checkBoxDialogItem;
		private System.Windows.Forms.Button buttonNo;
		private System.Windows.Forms.Button buttonYes;
	}
}