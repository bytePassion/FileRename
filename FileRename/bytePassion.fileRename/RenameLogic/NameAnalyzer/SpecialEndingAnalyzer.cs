using System;
using System.Collections.Generic;


namespace bytePassion.FileRename.RenameLogic.NameAnalyzer
{
	public class SpecialEndingAnalyzer : INameAnalyzer
	{
		// conditions for special ending
		//  - complete name has more than 4 characters
		//  - name without ending has more than 2 character
		//	- the special ending beginns with '-' or '_'
		//	- minimal length 4
		//	- maximal length 10
		//	- more hex-numbers than letters

		public bool IsMatch(string name)
		{
			if (name.Length < 5)
				return false;

			int i = name.Length - 1;
			while (i > 2)
			{
				char c = name[i];

				if (c == ' ') return false;
				if (c == '-' || c == '_') break;
				i--;
			}

			if (i == 2)
				return false;

			var specialEnding = name.Substring(i);

			if (specialEnding.Length > 4 && specialEnding.Length < 12)
			{
				var numberCount = 0;
				var letterCount = 0;

				foreach (char c in specialEnding.ToLower())
				{
					     if (c >= 'a' && c <= 'f') numberCount++;					
					else if (c >= '0' && c <= '9') numberCount++;
					else if (c >= 'g' && c <= 'z') letterCount++;
				}

				if (numberCount > letterCount)
					return true;
			}

			return false;
		}

		public IEnumerable<Tuple<int, int>> ReplacementIndecies(string name)
		{
			int i = name.Length - 1;
			while (i > 2)
			{
				char c = name[i];

				if (c == '-' || c == '_')
					break;

				i--;
			}

			return new List<Tuple<int, int>> { new Tuple<int, int>(i, name.Length) };
		}
	}
}
