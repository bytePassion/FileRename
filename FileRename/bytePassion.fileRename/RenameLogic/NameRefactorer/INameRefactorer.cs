using System;
using System.Collections.Generic;


namespace bytePassion.FileRename.RenameLogic.NameRefactorer
{

	public interface INameRefactorer
	{
		string GetRefactoredName(string name, IEnumerable<Tuple<int, int>> replacementIndecies);
	}

}