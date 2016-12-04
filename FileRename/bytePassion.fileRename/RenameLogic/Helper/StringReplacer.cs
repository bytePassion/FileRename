using System;
using System.Collections.Generic;
using System.Text;


namespace bytePassion.FileRename.RenameLogic.Helper
{
	public static class StringReplacer
	{
		public static string GetReplacedString(string name, string replacement, IEnumerable<Tuple<int, int>> replacementIndecies)
		{
			var stringBuilder = new StringBuilder();

			int currentIndex = 0;

			foreach (var indexTuple in replacementIndecies)
			{
				int startIndex = indexTuple.Item1;
				int endIndex = indexTuple.Item2;

				stringBuilder.Append(name.Substring(currentIndex, startIndex - currentIndex));
				stringBuilder.Append(replacement);

				currentIndex = endIndex;
			}

			stringBuilder.Append(name.Substring(currentIndex, name.Length - currentIndex));

			return stringBuilder.ToString();
		}
	}
}
