using System;
using System.Collections.Generic;


namespace bytePassion.FileRename.RenameLogic.Helper
{
	public static class IndexSearcher
	{
		public static IEnumerable<Tuple<int, int>> GetReplacementIndexTuples(string name, string search)
		{
			IList<Tuple<int, int>> indecies = new List<Tuple<int, int>>();

			int currentIndex = 0;
			while (currentIndex <= name.Length - search.Length)
			{
				if (name[currentIndex] == search[0])
				{
					if (search.Length == 1)
						indecies.Add(new Tuple<int, int>(currentIndex, (currentIndex + 1)));
					else
					{
						for (int localIndex = 1; localIndex < search.Length; localIndex++)
						{
							if (name[(currentIndex + localIndex)] != search[localIndex])
								goto NextIteration;
						}

						indecies.Add(new Tuple<int, int>(currentIndex, (currentIndex+search.Length)));
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
