using System;
using System.Collections.Generic;


namespace bytePassion.FileRename.RenameLogic.Helper
{
	public static class IndexSearcher
	{
		public static IEnumerable<StringIntervalIndecies> GetReplacementIndexTuples(string name, string search)
		{
			IList<StringIntervalIndecies> indecies = new List<StringIntervalIndecies>();

			var currentIndex = 0;
			while (currentIndex <= name.Length - search.Length)
			{
				if (name[currentIndex] == search[0])
				{
					if (search.Length == 1)
						indecies.Add(new StringIntervalIndecies(currentIndex, (currentIndex + 1)));
					else
					{
						for (var localIndex = 1; localIndex < search.Length; localIndex++)
						{
							if (name[(currentIndex + localIndex)] != search[localIndex])
								goto NextIteration;
						}

						indecies.Add(new StringIntervalIndecies(currentIndex, (currentIndex+search.Length)));
						currentIndex += search.Length;
						continue;
					}
				}

			NextIteration:
				currentIndex++;
			}

			return indecies;
		}
	}
}
