using System;
using System.Collections.Generic;
using bytePassion.FileRename.RenameLogic.Helper;


namespace bytePassion.FileRename.RenameLogic.NameRefactorer
{

	public interface INameRefactorer
	{
		string GetRefactoredName(string name, IEnumerable<StringIntervalIndecies> replacementIndecies);
	}
}