using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bytePassion.FileRename2.RenameLogic
{
	public class Renamer
	{
		
		private readonly Tuple<bool, string> abortReturn = new Tuple<bool, string>(false, "Manuell abgebrochen");

		private readonly DirectoryInfo startFolder;
		private readonly IDictionary<string, string> filenameAssosiactions;
		private readonly IList<ChangeAction> changes;

		private volatile bool abortSearch;
		
		public delegate void ItemProcessedEventHandler(string directory, string oldFileName, string newFilename);
		public delegate void ProcessFinishedEventHandler(bool finishedSuccessful, string errorMessage);
		
		public event ProcessFinishedEventHandler ProcessFinished;

		public Renamer(DirectoryInfo startFolder,
					   IDictionary<string, string> filenameAssosiactions)
		{
			this.startFolder       = startFolder;
			this.filenameAssosiactions = filenameAssosiactions;

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
					File.Move(changeAction.RenamedFileOrDirectoryName, changeAction.OriginalFileOrDirectoryName);					
				}
				
				return new Tuple<bool, string>(true, "Rückgänigmachung erfolgreich");
			});
		}

		private void AddFileToLastChanges(string originalFileName, string renamedFileName)
		{
			changes.Add(new ChangeAction(originalFileName, renamedFileName));			
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

					var nameWithoutExtension = Path.GetFileNameWithoutExtension(currentFile.FullName);
					var nameWithExtension = currentFile.Name;
					var extension = Path.GetExtension(currentFile.FullName);

					if (filenameAssosiactions.ContainsKey(nameWithoutExtension))
					{																													
						var newName = filenameAssosiactions[nameWithoutExtension];

						while (File.Exists(currentFile.DirectoryName + "\\" + newName + extension))
							newName = Path.GetFileNameWithoutExtension(newName) + "_2" + extension;

						currentFile.MoveTo(currentFile.DirectoryName + "\\" + newName + extension);

						AddFileToLastChanges(currentFile.DirectoryName + "\\" + nameWithExtension,
											 currentFile.DirectoryName + "\\" + newName + extension);						
					}					
				}					
			}
			catch (Exception except)
			{
				return new Tuple<bool, string>(false, except.Message);
			}

			return new Tuple<bool, string>(true, string.Empty);
		}
	}	
}
