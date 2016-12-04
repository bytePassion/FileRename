using System.Linq;
using bytePassion.FileRename.RenameLogic.Enums;
using bytePassion.FileRename.RenameLogic.NameAnalyzer;
using bytePassion.FileRename.RenameLogic.NameRefactorer;


namespace bytePassion.FileRename.RenameLogic
{
	public static class RenameProcessorBuilder
	{
		public static RenameProcessor Build(SearchType  searchType,  string searchString, bool searchCaseSensitive,
											ReplaceType replaceType, string replaceString)
		{ 
			INameAnalyzer nameAnalyzer = null;

			switch (searchType)
			{
				case SearchType.WhiteSpace: { nameAnalyzer = new WhiteSpaceAnalyzer();    break; }
				case SearchType.Special:    { nameAnalyzer = new SpecialEndingAnalyzer(); break; }	
				case SearchType.Characters:
				{

					var splitParts = searchString.Split(',')
					                             .Select(split => split.Trim());

					var isMultiSearch = false;
					
					if (splitParts.Count() > 1)
						if (splitParts.All(split => split.StartsWith("\"") && split.EndsWith("\"")))
							isMultiSearch = true;

					if (isMultiSearch)
						nameAnalyzer = new MultiStringAnalyzer (searchString, searchCaseSensitive);
					else
						nameAnalyzer = new SingleStringAnalyzer(searchString, searchCaseSensitive);
					break;
				}							
			}

			INameRefactorer nameRefactorer = null;

			switch (replaceType)
			{
				case ReplaceType.Delete:     { nameRefactorer = new StringRefactorer("");            break; }
				case ReplaceType.WhiteSpace: { nameRefactorer = new StringRefactorer(" ");           break; }
				case ReplaceType.Characters: { nameRefactorer = new StringRefactorer(replaceString); break; }								
			}

			return new RenameProcessor(nameAnalyzer, nameRefactorer);
		}
	}
}
