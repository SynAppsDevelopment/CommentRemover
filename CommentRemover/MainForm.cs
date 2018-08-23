using System;
using System.IO;
using System.Windows.Forms;

namespace CommentRemover
{
	public partial class MainForm : Form
	{
		private int FilesChanged;
		private int FilesChecked;

		public MainForm()
		{
			InitializeComponent();
			if (!String.IsNullOrWhiteSpace(FolderTextBox.Text))
				FBDialog.SelectedPath = FolderTextBox.Text;
		}

		private void RemoveCommentsButton_Click(object sender, EventArgs e)
		{
			#region Initialization

			Cursor = Cursors.WaitCursor;

			const string FILE_EXT = "cs";

			FilesChanged = 0;
			FilesChecked = 0;

			if (String.IsNullOrWhiteSpace(FolderTextBox.Text))
			{
				Cursor = Cursors.Default;
				MessageBox.Show("Please select a solution folder.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!Directory.Exists(FolderTextBox.Text))
			{
				Cursor = Cursors.Default;
				MessageBox.Show("This folder does not exist.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string[] FileNames = Directory.GetFiles(FolderTextBox.Text, "*." + FILE_EXT, SearchOption.AllDirectories);

			if (0 == FileNames.Length)
			{
				Cursor = Cursors.Default;
				MessageBox.Show("There are no *." + FILE_EXT + " files within this folder.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Cursor = Cursors.Default;

			if (DialogResult.No == MessageBox.Show("This operation will overwrite all *." + FILE_EXT + " files in your selected folder that have comments in them, " +
			                                       "except *.Designer." + FILE_EXT + " files and files in folders Properties, bin, obj, .vs, .git, .hg, and .svn." +
																Environment.NewLine + Environment.NewLine + "Do you want to continue?",
																this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				return;

			Cursor = Cursors.WaitCursor;

			FilesProgressBar.Value = FilesProgressBar.Minimum;
			FilesProgressBar.Maximum = FileNames.Length;
			FilesProgressBar.Visible = true;

			string ExFileName = null;

			#endregion Initialization

			try
			{
				foreach (string FileName in FileNames)
				{
					ExFileName = FileName;

					// Only process files that pass the following exclusions.
					// 1. Don't modify designer files.
					if (!FileName.EndsWith(".Designer." + FILE_EXT, StringComparison.CurrentCultureIgnoreCase) &&
						// 2. Exclude folders.
						!(FileName.Contains(@"\Properties\") || // AssemblyInfo.cs
						  FileName.Contains(@"\obj\") ||
						  FileName.Contains(@"\bin\") ||
						  FileName.Contains(@"\.vs\") ||
						  FileName.Contains(@"\.git\") ||
						  FileName.Contains(@"\.hg\") ||
						  FileName.Contains(@"\.svn\")))
					{
						RemoveComments(FileName);
					}

					FilesProgressBar.PerformStep();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error processing file '" + ExFileName + "'." + Environment.NewLine + Environment.NewLine + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			finally
			{
				FilesProgressBar.Visible = false;
				Cursor = Cursors.Default;
			}

			MessageBox.Show("Comments removed in " + FilesChanged + " files of " + FilesChecked + " examined files of " + FileNames.Length + " total files.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void RemoveComments(string FileName)
		{
			const string TWO_SLASH = "//";
			const string DOUBLE_QUOTE = "\"";

			FilesChecked++;
			bool ChangedFile = false;

			Int64 FileSize = new FileInfo(FileName).Length;
			System.Text.StringBuilder sb = new System.Text.StringBuilder(Convert.ToInt32(FileSize));

			using (StreamReader sr = new StreamReader(FileName))
			{
				string Line;
				int TwoSlashPos;
				int LastDoubleQuotePos;
				string LeftLine;

				while ((Line = sr.ReadLine()) != null)
				{
					// Check for two slashes. Don't check for last in case there is (1) a commented comment,
					// (2) other in-comment usage like http, or (3) a C# documentation triple slash.
					// Example 1: //int i = 2; // Start loop at 2.
					// Example 2: // http://www.microsoft.com
					// Example 3: /// <summary>Retrieve all LabTech Computers.</summary>

					TwoSlashPos = Line.IndexOf(TWO_SLASH, StringComparison.CurrentCulture);

					// If comment not found, write (keep) line. The file will not be changed unless we detect a comment.
					if (TwoSlashPos == -1)
						sb.AppendLine(Line);

					else
					{
						// Get left side of two slashes.
						LeftLine = Line.Substring(0, TwoSlashPos);

						// If left side of the two slashes is whitespace, do nothing with this detected whole-line comment.
						// It will not be added to the StringBuilder and thus removed
						// from the output file because there appears to be no code.
						if (String.IsNullOrWhiteSpace(LeftLine))
						{
							ChangedFile = true;
						}
						else
						{
							// If left side of two slashes is not whitespace (probable code),
							// process and add non-comment portion to StringBuilder.
							LastDoubleQuotePos = Line.LastIndexOf(DOUBLE_QUOTE);
							if (LastDoubleQuotePos == -1)
							{
								// If no double quote, this is a simple comment. Just add code (non-comment) portion of Line.
								sb.AppendLine(LeftLine);
								ChangedFile = true;
							}
							else
							{
								// If left side contains a double quote (usually a string literal), re-check for comment
								// at end (after last double quote), because it could be the string that contains two slashes.
								// Example of comment: string uri = "http://www.microsoft.com/"; // Website.
								// Example of code only: UriAuthority = Protocol + "://" + ServerName + ":" + PortNumber + "/";

								// TODO: This won't handle " in the comment, such as with the examples below.
								//       It will write the entire line without stripping the comment.
								//       To handle this, remove everything after the final two slashes.
								// Example: s = "\""; // Use " character.
								// Example: s = "Text"; // Use "Text" string.

								// TODO: This won't handle mmultiple comments with double quotes, such as the examples below.
								// Example: s = "Text"; // Strings are enclosed within ". // Reset the "handler" string.
								// Example: s = "Text"; // Reset the "handler" string. // Strings are enclosed within ".

								// TODO: This won't handle /* */ comments.

								// Search for first two slashes after last double quote.
								TwoSlashPos = Line.IndexOf(TWO_SLASH, LastDoubleQuotePos, StringComparison.CurrentCulture);

								// If no two slashes after last double quote, there is no comment (detected, see TODO),
								// so add entire line. The file is not changed.
								if (TwoSlashPos == -1)
								{
									sb.AppendLine(Line);
								}
								else
								{
									// Found two slashes (comment) after last double quote (end of string literal).
									// Add code (non-comment) portion of Line, the left side before the two slashes.
									LeftLine = Line.Substring(0, TwoSlashPos);
									sb.AppendLine(LeftLine);
									ChangedFile = true;
								}
							}
						}
					}
				}
			}

			if (ChangedFile)
			{
				File.WriteAllText(FileName, sb.ToString());
				FilesChanged++;
			}
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == FBDialog.ShowDialog())
				FolderTextBox.Text = FBDialog.SelectedPath;
		}

	}
}