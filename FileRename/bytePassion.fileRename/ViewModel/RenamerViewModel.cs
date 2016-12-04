using bytePassion.FileRename.RenameLogic;
using bytePassion.FileRename.RenameLogic.Enums;
using bytePassion.FileRename.ViewModel.Helper;
using bytePassion.Lib.FrameworkExtensions;
using bytePassion.Lib.WpfLib;
using bytePassion.Lib.WpfLib.Commands;
using bytePassion.Lib.WpfLib.Commands.Updater;
using Ookii.Dialogs.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using static System.String;


namespace bytePassion.FileRename.ViewModel
{
	public class RenamerViewModel : IRenamerViewModel
	{

		private Renamer renamer;				

		private SearchType  searchType;
		private ReplaceType replaceType;

		private string startDirectory;
		private string searchString;
		private string replaceWidthString;

		private bool searchParameterCaseSensitivity;
		private bool searchParameterIncludeSubfolers;
		private bool searchParameterChangeFolderNames;
		
		private readonly Command startCommand;
		private readonly Command abortCommand;
		private readonly Command selectFolderCommand;
		private readonly Command undoLastRenamingCommand;

		private bool isProcessStartable;
		private bool isProcessAbortable;
		private bool isProcessUndoable;
		private bool isProcessRunning;
		


		public RenamerViewModel(IEnumerable<string> lastExecutedStartFolders)
		{			
			startCommand = new Command(
				() => { 

					SetUpRenamingProcess(); 
					ListItems.Clear();
					renamer.StartRenaming();

					LastExecutedStartFolders.Add(StartDirectory);

					IsProcessStartable = false;
					IsProcessAbortable = true;
					IsProcessRunning = true;					
				},

				() => {

					if (!IsProcessStartable) return false;
					if (SearchType   == SearchType.Characters && IsNullOrEmpty(SearchString))  return false;
					if (ReplaceType == ReplaceType.Characters && IsNullOrEmpty(ReplaceString)) return false;
					if (IsNullOrEmpty(StartDirectory)) return false;

					return true;
				},
				new PropertyChangedCommandUpdater(this, nameof(IsProcessStartable), 
												  nameof(IsProcessStartable),
												  nameof(ReplaceString),
												  nameof(ReplaceType),
												  nameof(SearchType),
												  nameof(SearchString))
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
            
			Columns = new ObservableCollection<ColumnDescriptor>()
			{
				new ColumnDescriptor { Header = "Aktueller Ordner", DisplayMember = FileListItem.CurrentDirectoryVariableName },
				new ColumnDescriptor { Header = "Alter Dateiname",  DisplayMember = FileListItem.OldFileNameVariableName      },
				new ColumnDescriptor { Header = "Neuer DateiName",  DisplayMember = FileListItem.NewFileNameVariableName      }
			};

			ListItems = new ObservableCollection<FileListItem>();
			LastExecutedStartFolders = new ObservableCollection<string>(lastExecutedStartFolders);

			IsProcessAbortable = false;
			IsProcessStartable = true;
			IsProcessRunning   = false;
			IsProcessUndoable  = false;

			SearchParameterCaseSensitivity  = false;
			SearchParameterIncludeSubfolders = true;
			SearchParameterChangeFolderNames = true;

			SearchType  = SearchType.WhiteSpace;
			ReplaceType = ReplaceType.Delete;
		}
		
		private void SetUpRenamingProcess()
		{
			renamer = new Renamer(new DirectoryInfo(StartDirectory), 
								  RenameProcessorBuilder.Build(SearchType, SearchString, SearchParameterCaseSensitivity, 
															   ReplaceType, ReplaceString),
								  SearchParameterIncludeSubfolders, 
								  SearchParameterChangeFolderNames);

			renamer.ItemProcessed += (directory, oldFilename, newFilename) =>
			{
				Application.Current.Dispatcher.Invoke(() => ListItems.Add(new FileListItem(directory, oldFilename, newFilename)));
			};

			renamer.ProcessFinished += (successful, message) =>
			{
				var stringBuilder = new StringBuilder();

				stringBuilder.Append(successful
					               ? "Der Prozess wurde erfolgreich beendet"
					               : "Der Prozess wurde mit folgender Meldung abgebrochen");

				if (!IsNullOrEmpty(message))
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
			get {return startDirectory; }
			set
			{
				PropertyChanged.ChangeAndNotify(this, ref startDirectory, value);

				IsProcessStartable = Directory.Exists(StartDirectory);
			}
		}

		public string SearchString
		{
			get { return searchString; }
			set { PropertyChanged.ChangeAndNotify(this, ref searchString, value); }
		}

		public string ReplaceString
		{
			get { return replaceWidthString; }
			set { PropertyChanged.ChangeAndNotify(this, ref replaceWidthString, value);	}
		}

		public SearchType SearchType
		{
			get { return searchType; }
			set { PropertyChanged.ChangeAndNotify(this, ref searchType, value); }
		}

		public ReplaceType ReplaceType 
		{
			get { return replaceType; }
			set { PropertyChanged.ChangeAndNotify(this, ref replaceType, value); } 
		}

		public bool SearchParameterCaseSensitivity
		{
			get { return searchParameterCaseSensitivity; }
			set { PropertyChanged.ChangeAndNotify(this, ref searchParameterCaseSensitivity, value); }
		}

		public bool SearchParameterIncludeSubfolders
		{
			get { return searchParameterIncludeSubfolers; }
			set { PropertyChanged.ChangeAndNotify(this, ref searchParameterIncludeSubfolers, value); }
		}

		public bool SearchParameterChangeFolderNames
		{
			get { return searchParameterChangeFolderNames; }
			set { PropertyChanged.ChangeAndNotify(this, ref searchParameterChangeFolderNames, value); }
		}

		public ObservableCollection<ColumnDescriptor> Columns                  { get; }
		public ObservableCollection<FileListItem>     ListItems                { get; }
		public ObservableCollection<string>           LastExecutedStartFolders { get; }

		public event PropertyChangedEventHandler PropertyChanged;		
	}
}
