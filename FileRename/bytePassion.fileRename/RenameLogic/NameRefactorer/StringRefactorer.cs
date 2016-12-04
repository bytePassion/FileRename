using System;
using System.Collections.Generic;
using bytePassion.FileRename.RenameLogic.Helper;


namespace bytePassion.FileRename.RenameLogic.NameRefactorer
{
	public class StringRefactorer : INameRefactorer
	{
		private readonly string replaceString;

		public StringRefactorer(string replaceString)
		{
			this.replaceString = replaceString;
		}

		public string GetRefactoredName(string name, IEnumerable<Tuple<int, int>> replacementIndecies)
		{
			return StringReplacer.GetReplacedString(name, replaceString, replacementIndecies);
		}
	}
}
