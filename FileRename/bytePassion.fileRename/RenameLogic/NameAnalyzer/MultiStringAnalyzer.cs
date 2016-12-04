using System;
using System.Collections.Generic;
using System.Linq;
using bytePassion.FileRename.RenameLogic.Helper;


namespace bytePassion.FileRename.RenameLogic.NameAnalyzer
{
	public class MultiStringAnalyzer : INameAnalyzer
	{
		private readonly bool searchCaseSensitive;
		private readonly IEnumerable<string> searchStrings; 

		public MultiStringAnalyzer(string searchString, bool searchCaseSensitive)
		{
			this.searchCaseSensitive = searchCaseSensitive;

			var actualSearchString = searchCaseSensitive ? searchString : searchString.ToLower();

			searchStrings = actualSearchString.Split(',')
											  .Select(split => split.Trim())
											  .Select(split => split.Substring(1, split.Length-2));			
		}

		public bool IsMatch(string name)
		{
			var nameToAnalyze = searchCaseSensitive ? name : name.ToLower();
			return searchStrings.Any(nameToAnalyze.Contains);			
		}

		public IEnumerable<Tuple<int, int>> ReplacementIndecies(string name)
		{
			var nameToAnalyze = searchCaseSensitive ? name : name.ToLower();

			var allIndecies = searchStrings.Select(search => IndexSearcher.GetReplacementIndexTuples(nameToAnalyze, search))
			                               .SelectMany(indexList => indexList)
			                               .OrderBy(tuple => tuple.Item1)
			                               .ToList();

			int currentEnd = allIndecies[0].Item2;

			var resultList = new List<Tuple<int, int>> {allIndecies[0]};

			for (int i = 1; i < allIndecies.Count; i++)
			{
				var item = allIndecies[i];
				if (item.Item1 >= currentEnd)
				{
					currentEnd = item.Item2;
					resultList.Add(item);
				}
			}

			return resultList;
		}	
	}
}
