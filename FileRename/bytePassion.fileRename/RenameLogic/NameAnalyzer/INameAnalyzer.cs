using System;
using System.Collections.Generic;


namespace bytePassion.FileRename.RenameLogic.NameAnalyzer
{

	public interface INameAnalyzer
	{
		bool IsMatch(string name);
		IEnumerable<Tuple<int, int>> ReplacementIndecies(string name);
	}
}