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

		public IEnumerable<StringIntervalIndecies> ReplacementIndecies(string name)
		{
			var nameToAnalyze = searchCaseSensitive ? name : name.ToLower();

			var allIndecies = searchStrings.Select(search => IndexSearcher.GetReplacementIndexTuples(nameToAnalyze, search))
			                               .SelectMany(indexList => indexList)
			                               .OrderBy(tuple => tuple.StartIndex)
			                               .ToList();

			int currentEnd = allIndecies[0].EndIndex;

			var resultList = new List<StringIntervalIndecies> {allIndecies[0]};

			for (int i = 1; i < allIndecies.Count; i++)
			{
				var item = allIndecies[i];
				if (item.StartIndex >= currentEnd)
				{
					currentEnd = item.EndIndex;
					resultList.Add(item);
				}
			}

			return resultList;
		}	
	}
}
