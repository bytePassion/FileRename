using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using bytePassion.FileRename2.RenameLogic;
using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.WpfLib.Commands;
using bytePassion.Lib.WpfLib.Commands.Updater;
using Ookii.Dialogs.Wpf;

namespace bytePassion.FileRename2.ViewModel
{
	public class RenamerViewModel : IRenamerViewModel
	{

		private Renamer renamer;				
				
		private readonly Command startCommand;
		private readonly Command abortCommand;
		private readonly Command selectFolderCommand;
		private readonly Command undoLastRenamingCommand;

		private bool isProcessStartable;
		private bool isProcessAbortable;
		private bool isProcessUndoable;
		private bool isProcessRunning;

		private string startDirectory;
		private string originalNames;
		private string newNames;

		public RenamerViewModel(IEnumerable<string> lastExecutedStartFolders)
		{			
			startCommand = new Command(
				() => 
				{ 

					SetUpRenamingProcess(); 					
					renamer.StartRenaming();

					LastExecutedStartFolders.Add(StartDirectory);

					IsProcessStartable = false;
					IsProcessAbortable = true;
					IsProcessRunning = true;					
				},

				() => 
				{

					if (!IsProcessStartable) return false;					
					if (string.IsNullOrEmpty(StartDirectory)) return false;

					return true;
				},
				new PropertyChangedCommandUpdater(this, nameof(IsProcessStartable), 
												  nameof(IsProcessStartable))
												 
			);

			abortCommand = new Command(
				() =>  renamer.AbortRenaming(),						
				() =>  IsProcessAbortable,
				new PropertyChangedCommandUpdater(this, nameof(IsProcessAbortable))
			);

			selectFolderCommand = new Command(
				ShowDirectoryDialog,
				() => !IsProcessRunning,
				new PropertyChangedCommandUpdater(this, nameof(IsProcessRunning))
			);

			undoLastRenamingCommand = new Command(
				() => {

					renamer.UndoRenaming();
					IsProcessStartable = false;
					IsProcessAbortable = false;
					IsProcessRunning = true;
				},
				() => IsProcessUndoable,
				new PropertyChangedCommandUpdater(this, nameof(IsProcessUndoable))
			);
            			
			LastExecutedStartFolders = new ObservableCollection<string>(lastExecutedStartFolders);

			IsProcessAbortable = false;
			IsProcessStartable = true;
			IsProcessRunning   = false;
			IsProcessUndoable  = false;						
		}
		
		private void SetUpRenamingProcess()
		{
			var filenameAssosiactions = new Dictionary<string, string>();

			var originalNameList = OriginalNames.Split('\n')
												.Select(name => name.Trim())
												.ToList();

			var newNameList = NewNames.Split('\n')
									  .Select(name => name.Trim())
									  .ToList();

			for (var i = 0; i < originalNameList.Count; i++)
			{
				if (newNameList.Count > i)				
					filenameAssosiactions.Add(originalNameList[i], newNameList[i]);				
			}

			renamer = new Renamer(new DirectoryInfo(StartDirectory), 
								  filenameAssosiactions);			

			renamer.ProcessFinished += (successful, message) =>
			{
				var stringBuilder = new StringBuilder();

				stringBuilder.Append(successful
					               ? "Der Prozess wurde erfolgreich beendet"
					               : "Der Prozess wurde mit folgender Meldung abgebrochen");

				if (!string.IsNullOrEmpty(message))
					stringBuilder.Append(":\n\n" + message);

				MessageBox.Show(stringBuilder.ToString());

				IsProcessUndoable = renamer.UndoAvailable;
				IsProcessStartable = true;
				IsProcessAbortable = false;
				IsProcessRunning = false;

				StartDirectory = StartDirectory; // to check weather current folder is still available
			};
		}
	

		public ICommand Start            => startCommand;
		public ICommand Abort            => abortCommand;
		public ICommand SelectFolder     => selectFolderCommand;
		public ICommand UndoLastRenaming => undoLastRenamingCommand;


		public bool IsProcessRunning
		{
			get { return isProcessRunning;}
			private set { PropertyChanged.ChangeAndNotify(this, ref isProcessRunning, value); }
		}

		public bool IsProcessStartable
		{
			get { return isProcessStartable; }
			private set { PropertyChanged.ChangeAndNotify(this, ref isProcessStartable, value);	}
		}

		public bool IsProcessAbortable
		{
			get { return isProcessAbortable; }
			private set { PropertyChanged.ChangeAndNotify(this, ref isProcessAbortable, value); }
		}

		public bool IsProcessUndoable
		{
			get { return isProcessUndoable; }
			private set { PropertyChanged.ChangeAndNotify(this, ref isProcessUndoable, value); }
		}

		private void ShowDirectoryDialog()
		{
			var dialog = new VistaFolderBrowserDialog
			{
				Description = @"Bitte Ordner Auswählen",
				UseDescriptionForTitle = true
			};
			
			var showDialogResult = dialog.ShowDialog(null);
			if (showDialogResult != null && (bool)showDialogResult)
				StartDirectory = dialog.SelectedPath;
		}		

		public string StartDirectory
		{
			get { return startDirectory; }
			set
			{
				PropertyChanged.ChangeAndNotify(this, ref startDirectory, value);

				IsProcessStartable = Directory.Exists(StartDirectory);

				if (IsProcessStartable)
				{
					var sb = new StringBuilder();

					var fileList = new DirectoryInfo(StartDirectory)
											.GetFiles()
											.Select(currentFile => Path.GetFileNameWithoutExtension(currentFile.FullName))
											.ToList();

					fileList.Sort(new LikeWindowsComparer());
					fileList.Do(name => sb.Append(name + "\n"));

					if (sb.Length > 0)
						sb.Remove(sb.Length - 1, 1);

					OriginalNames = sb.ToString();
					NewNames = "";
				}
			}
		}
		
		private class LikeWindowsComparer : IComparer<string>
		{
			[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			private static extern int StrCmpLogicalW (string x, string y);

			public int Compare (string x, string y)
			{
				return StrCmpLogicalW(x, y);
			}

		}

		public string OriginalNames
		{
			get { return originalNames; }
			private set { PropertyChanged.ChangeAndNotify(this, ref originalNames, value); }
		}

		public string NewNames
		{
			get { return newNames; }
			set { PropertyChanged.ChangeAndNotify(this, ref newNames, value); }
		}

		public ObservableCollection<string> LastExecutedStartFolders { get; }

		public event PropertyChangedEventHandler PropertyChanged;		
	}

	

}
