using bytePassion.FileRename.RenameLogic.Enums;
using bytePassion.FileRename.ViewModel.Helper;
using bytePassion.Lib.WpfLib;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

#pragma warning disable 0067

namespace bytePassion.FileRename.ViewModel.SampleData
{
	public class RenamerViewModelSampleData : IRenamerViewModel
	{
		public RenamerViewModelSampleData()
		{		
			IsProcessStartable = true;
			IsProcessAbortable = false;
			IsProcessUndoable  = true;
			IsProcessRunning   = false;

			StartDirectory = "Ordner wählen ....";
			SearchParameterIncludeSubfolders = true;
			SearchParameterChangeFolderNames = true;			

			Columns = new ObservableCollection<ColumnDescriptor>()
			{
				new ColumnDescriptor { Header = "Aktueller Ordner", DisplayMember = FileListItem.CurrentDirectoryVariableName },
				new ColumnDescriptor { Header = "Alter Dateiname",  DisplayMember = FileListItem.OldFileNameVariableName      },
				new ColumnDescriptor { Header = "Neuer DateiName",  DisplayMember = FileListItem.NewFileNameVariableName      }
			};

			LastExecutedStartFolders = new ObservableCollection<string>
			                           {
				                           @"C:\test.txt",
										   @"D:\test2.txt"
			                           };
		}

		public ICommand Start            => null;
		public ICommand Abort            => null;
		public ICommand SelectFolder     => null;
		public ICommand UndoLastRenaming => null;

		public bool IsProcessStartable { get; }
		public bool IsProcessAbortable { get; }
		public bool IsProcessUndoable  { get; }
		public bool IsProcessRunning   { get; }					

		public string StartDirectory { get; set; }

		public string SearchString  { get; set; }
		public string ReplaceString { get; set; }

		public SearchType SearchType   { get; set; }
		public ReplaceType ReplaceType { get; set; }

		public bool SearchParameterCaseSensitivity   { get; set; }
		public bool SearchParameterIncludeSubfolders { get; set; }
		public bool SearchParameterChangeFolderNames { get; set; }

		public ObservableCollection<ColumnDescriptor> Columns   { get; set; }
		public ObservableCollection<FileListItem>     ListItems { get; set; }

		public ObservableCollection<string> LastExecutedStartFolders { get; }

		public event PropertyChangedEventHandler PropertyChanged;		
	}
}
