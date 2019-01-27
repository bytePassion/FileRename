using System;
using System.Collections.Generic;
using bytePassion.FileRename.RenameLogic.Helper;


namespace bytePassion.FileRename.RenameLogic.NameAnalyzer
{

	public interface INameAnalyzer
	{
		bool IsMatch(string name);
		IEnumerable<StringIntervalIndecies> ReplacementIndecies(string name);
	}
}