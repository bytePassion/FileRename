using System.IO;
using bytePassion.FileRename.RenameLogic.Enums;
using bytePassion.FileRename.RenameLogic.NameAnalyzer;
using bytePassion.FileRename.RenameLogic.NameRefactorer;


namespace bytePassion.FileRename.RenameLogic
{
	public class RenameProcessor
	{		
		private readonly INameAnalyzer   analyzer;
		private readonly INameRefactorer refactorer;

		public RenameProcessor(INameAnalyzer analyzer, INameRefactorer refactorer)
		{			
			this.analyzer = analyzer;
			this.refactorer = refactorer;			
		}

		public bool IsMatch(string name, ItemType type)
		{
			if (type == ItemType.Directory)
			{
				return analyzer.IsMatch(name);
			}
			else // (type == ItemType.File)
			{				
				var nameWithoutExtension = Path.GetFileNameWithoutExtension(name);
				return analyzer.IsMatch(nameWithoutExtension);
			}
		}

		public string RefactoredName(string name, ItemType type)
		{
			if (type == ItemType.Directory)
			{
				return ProcessRefactoring(name);
			}
			else // (type == ItemType.File)
			{
				var extension = Path.GetExtension(name);
				var nameWithoutExtension = Path.GetFileNameWithoutExtension(name);

				return ProcessRefactoring(nameWithoutExtension) + extension;
			}

		}

		private string ProcessRefactoring(string nameToRefactor)
		{
			var replacementIndecies = analyzer.ReplacementIndecies(nameToRefactor);
			return refactorer.GetRefactoredName(nameToRefactor, replacementIndecies);
		}		
	}
}
