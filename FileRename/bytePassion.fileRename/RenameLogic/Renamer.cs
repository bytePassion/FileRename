using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bytePassion.FileRename.RenameLogic.Enums;


namespace bytePassion.FileRename.RenameLogic
{
	public class Renamer
	{
		
		private readonly Tuple<bool, string> abortReturn = new Tuple<bool, string>(false, "Manuell abgebrochen");

		private readonly DirectoryInfo startFolder;
		private readonly RenameProcessor renameProcessor;		
		private readonly bool includeSubfolders;
		private readonly bool changeFolderNames;

		private volatile bool abortSearch;
		private readonly IList<ChangeAction> changes;		

		public delegate void ItemProcessedEventHandler(string directory, string oldFileName, string newFilename);
		public delegate void ProcessFinishedEventHandler(bool finishedSuccessful, string errorMessage);

		public event ItemProcessedEventHandler ItemProcessed;
		public event ProcessFinishedEventHandler ProcessFinished;

		public Renamer(DirectoryInfo startFolder,
					   RenameProcessor renameProcessor, 
					   bool includeSubfolders, bool changeFolderNames)
		{
			this.startFolder       = startFolder;
			this.renameProcessor   = renameProcessor;				
			this.includeSubfolders = includeSubfolders;
			this.changeFolderNames = changeFolderNames;			

			changes = new List<ChangeAction>();

			abortSearch = false;
			UndoAvailable = false;
		}

		public async void StartRenaming()
		{
			var result = await RecursiveRenamerAsync(startFolder);

			ProcessFinished?.Invoke(result.Item1, result.Item2);
		}

		public void AbortRenaming()
		{
			abortSearch = true;
		}

		public bool UndoAvailable { get; private set; }

		public async void UndoRenaming()
		{
			UndoAvailable = false;

			var result = await UndoRenamingAsync();

			ProcessFinished?.Invoke(result.Item1, result.Item2);
		}

		private Task<Tuple<bool, string>> UndoRenamingAsync ()
		{
			return Task.Factory.StartNew(() =>
			{

				foreach (var changeAction in changes.Reverse())
				{
					if (changeAction.ChangedNameType == ItemType.File)
						File.Move(changeAction.RenamedFileOrDirectoryName, changeAction.OriginalFileOrDirectoryName);
					else
						Directory.Move(changeAction.RenamedFileOrDirectoryName, changeAction.OriginalFileOrDirectoryName);											
				}
				
				return new Tuple<bool, string>(true, "Rückgänigmachung erfolgreich");
			});
		}

		private void AddFileToLastChanges(string originalFileName, string renamedFileName)
		{
			changes.Add(new ChangeAction(originalFileName, renamedFileName, ItemType.File));			
			UndoAvailable = true;
		}

		private void AddDirectoryToLastChanges(string originalDirectoryName, string renamedDirectoryName)
		{
			changes.Add(new ChangeAction(originalDirectoryName, renamedDirectoryName, ItemType.Directory));		
			UndoAvailable = true;
		}

		private Task<Tuple<bool, string>> RecursiveRenamerAsync(DirectoryInfo directory)
		{
			return Task.Factory.StartNew(() => RecursiveRenamer(directory));
		}

		private Tuple<bool, string> RecursiveRenamer(DirectoryInfo directory)
		{
			try
			{
				if (abortSearch)
					return abortReturn;

				foreach (var currentFile in directory.GetFiles())
				{					
					if (abortSearch)
						return abortReturn;

					if (renameProcessor.IsMatch(currentFile.Name, ItemType.File))
					{
						var oldName = currentFile.Name;
						var newName = renameProcessor.RefactoredName(currentFile.Name, ItemType.File);

						while (File.Exists(currentFile.DirectoryName + "\\" + newName))
							newName = Path.GetFileNameWithoutExtension(newName) + "_2" + Path.GetExtension(newName);

						currentFile.MoveTo(currentFile.DirectoryName + "\\" + newName);

						AddFileToLastChanges(currentFile.DirectoryName + "\\" + oldName,
											 currentFile.DirectoryName + "\\" + newName);

						ItemProcessed?.Invoke(directory.FullName, oldName, newName);
					}
					else
					{
						ItemProcessed?.Invoke(directory.FullName, currentFile.Name, "Name nicht geändert");
					}
				}

				if (includeSubfolders)
				{
					if (abortSearch)
						return abortReturn;

					foreach (var currentDirectory in directory.GetDirectories())
					{
						var retVal = RecursiveRenamer(currentDirectory);

						if (!retVal.Item1)
							return retVal;
					}
				}

				if (abortSearch)
					return abortReturn;

				if (changeFolderNames)
					if (renameProcessor.IsMatch(directory.Name, ItemType.Directory))
					{
						var oldName = directory.Name;
						var newName = renameProcessor.RefactoredName(directory.Name, ItemType.Directory);
						
						if (directory.Parent != null)
						{
							var fullOldName = directory.Parent.FullName + "\\" + oldName;
							var fullNewName = directory.Parent.FullName + "\\" + newName;

							while (Directory.Exists(fullNewName))
								fullNewName += "_2";

							directory.MoveTo(fullNewName);

							AddDirectoryToLastChanges(fullOldName, fullNewName);						
						}

						ItemProcessed?.Invoke(directory.FullName + "\t [Ordnername geändert]", oldName, newName);
					}				
			}
			catch (Exception except)
			{
				return new Tuple<bool, string>(false, except.Message);
			}
			return new Tuple<bool, string>(true, String.Empty);
		}
	}	
}
