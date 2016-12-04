namespace bytePassion.FileRename2.RenameLogic
{
	public class ChangeAction
	{
		public ChangeAction (string originalFileOrDirectoryName,
							 string renamedFileOrDirectoryName)
		{
			OriginalFileOrDirectoryName = originalFileOrDirectoryName;
			RenamedFileOrDirectoryName  = renamedFileOrDirectoryName;			
		}

		public string   OriginalFileOrDirectoryName { get; }
		public string   RenamedFileOrDirectoryName  { get; }		
	}
}
