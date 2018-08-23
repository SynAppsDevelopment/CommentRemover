namespace CommentRemover
{
	partial class MainForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.FolderTextBox = new System.Windows.Forms.TextBox();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.RemoveCommentsButton = new System.Windows.Forms.Button();
			this.FBDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.FilesProgressBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(17, 16);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Solution Folder";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(139, 39);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(300, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "This folder and all subfolders will be searched.";
			// 
			// FolderTextBox
			// 
			this.FolderTextBox.Location = new System.Drawing.Point(142, 13);
			this.FolderTextBox.Name = "FolderTextBox";
			this.FolderTextBox.Size = new System.Drawing.Size(500, 23);
			this.FolderTextBox.TabIndex = 2;
			// 
			// BrowseButton
			// 
			this.BrowseButton.Location = new System.Drawing.Point(648, 12);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(75, 27);
			this.BrowseButton.TabIndex = 3;
			this.BrowseButton.Text = "Browse..";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// RemoveCommentsButton
			// 
			this.RemoveCommentsButton.Location = new System.Drawing.Point(142, 73);
			this.RemoveCommentsButton.Name = "RemoveCommentsButton";
			this.RemoveCommentsButton.Size = new System.Drawing.Size(151, 35);
			this.RemoveCommentsButton.TabIndex = 4;
			this.RemoveCommentsButton.Text = "Remove Comments";
			this.RemoveCommentsButton.UseVisualStyleBackColor = true;
			this.RemoveCommentsButton.Click += new System.EventHandler(this.RemoveCommentsButton_Click);
			// 
			// FBDialog
			// 
			this.FBDialog.Description = "Select Solution Folder";
			this.FBDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.FBDialog.ShowNewFolderButton = false;
			// 
			// FilesProgressBar
			// 
			this.FilesProgressBar.Location = new System.Drawing.Point(300, 73);
			this.FilesProgressBar.Minimum = 1;
			this.FilesProgressBar.Name = "FilesProgressBar";
			this.FilesProgressBar.Size = new System.Drawing.Size(423, 34);
			this.FilesProgressBar.Step = 1;
			this.FilesProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.FilesProgressBar.TabIndex = 6;
			this.FilesProgressBar.Value = 1;
			this.FilesProgressBar.Visible = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(742, 123);
			this.Controls.Add(this.FilesProgressBar);
			this.Controls.Add(this.RemoveCommentsButton);
			this.Controls.Add(this.BrowseButton);
			this.Controls.Add(this.FolderTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "C# Comment Remover";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox FolderTextBox;
		private System.Windows.Forms.Button BrowseButton;
		private System.Windows.Forms.Button RemoveCommentsButton;
		private System.Windows.Forms.FolderBrowserDialog FBDialog;
		private System.Windows.Forms.ProgressBar FilesProgressBar;
	}
}

