
namespace bytePassion.FileRename.ViewModel.Helper
{
	public class FileListItem
	{
		public const string CurrentDirectoryVariableName = "CurrentDirectory";
		public const string OldFileNameVariableName      = "OldFileName";
		public const string NewFileNameVariableName      = "NewFileName";

		public FileListItem(string currentDirectory, string oldName, string newName)
		{
			CurrentDirectory = currentDirectory;
			OldFileName = oldName;
			NewFileName = newName;
		}
						
		public string CurrentDirectory { get; set; }						
		public string OldFileName      { get; set; }
		public string NewFileName      { get; set; }			
	}
}
