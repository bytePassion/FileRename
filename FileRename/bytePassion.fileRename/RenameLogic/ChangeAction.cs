
using bytePassion.FileRename.RenameLogic.Enums;


namespace bytePassion.FileRename.RenameLogic
{
	public class ChangeAction
	{
		public ChangeAction (string originalFileOrDirectoryName,
							 string renamedFileOrDirectoryName,
							 ItemType changedNameType)
		{
			OriginalFileOrDirectoryName = originalFileOrDirectoryName;
			RenamedFileOrDirectoryName  = renamedFileOrDirectoryName;
			ChangedNameType             = changedNameType;
		}

		public string   OriginalFileOrDirectoryName { get; }
		public string   RenamedFileOrDirectoryName  { get; }
		public ItemType ChangedNameType             { get; }
	}
}
