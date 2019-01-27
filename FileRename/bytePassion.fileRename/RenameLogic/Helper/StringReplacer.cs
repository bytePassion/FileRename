using System;
using System.Collections.Generic;
using System.Text;


namespace bytePassion.FileRename.RenameLogic.Helper
{
    public static class StringReplacer
	{
		public static string GetReplacedString(string name, string replacement, IEnumerable<StringIntervalIndecies> replacementIndecies)
		{
			var stringBuilder = new StringBuilder();

			var currentIndex = 0;

			foreach (var intervalIndecies in replacementIndecies)
			{				
				stringBuilder.Append(name.Substring(currentIndex, intervalIndecies.StartIndex - currentIndex));
				stringBuilder.Append(replacement);

				currentIndex = intervalIndecies.EndIndex;
			}

			stringBuilder.Append(name.Substring(currentIndex, name.Length - currentIndex));

			return stringBuilder.ToString();
		}
	}
}
