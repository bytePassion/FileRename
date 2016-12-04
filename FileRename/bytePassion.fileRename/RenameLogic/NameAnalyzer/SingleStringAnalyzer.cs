using System;
using System.Collections.Generic;
using bytePassion.FileRename.RenameLogic.Helper;


namespace bytePassion.FileRename.RenameLogic.NameAnalyzer
{
	public class SingleStringAnalyzer : INameAnalyzer
	{
		private readonly string searchString;
		private readonly bool searchCaseSensitive;

		public SingleStringAnalyzer(string searchString, bool searchCaseSensitive)
		{
			this.searchCaseSensitive = searchCaseSensitive;
			this.searchString = searchCaseSensitive ? searchString : searchString.ToLower();
		}

		public bool IsMatch(string name)
		{
			var nameToAnalyze = searchCaseSensitive ? name : name.ToLower();
			return nameToAnalyze.Contains(searchString);			
		}

		public IEnumerable<Tuple<int, int>> ReplacementIndecies(string name)
		{
			var nameToAnalyze = searchCaseSensitive ? name : name.ToLower();
			return IndexSearcher.GetReplacementIndexTuples(nameToAnalyze, searchString);
		}
	}
}
