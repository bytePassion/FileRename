using System;
using System.Collections.Generic;
using System.Linq;
using bytePassion.FileRename.RenameLogic.Helper;


namespace bytePassion.FileRename.RenameLogic.NameAnalyzer
{
	public class WhiteSpaceAnalyzer : INameAnalyzer
	{		
		
		public bool IsMatch(string name)
		{
			return name.Contains(' ');
		}

		public IEnumerable<Tuple<int, int>> ReplacementIndecies(string name)
		{
			return IndexSearcher.GetReplacementIndexTuples(name, " ");
		}
	}
}
