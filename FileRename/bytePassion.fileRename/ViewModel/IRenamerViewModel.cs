using bytePassion.FileRename.RenameLogic.Enums;
using bytePassion.FileRename.ViewModel.Helper;
using bytePassion.Lib.WpfLib;
using System.Collections.ObjectModel;
using System.Windows.Input;
using bytePassion.Lib.WpfLib.ViewModelBase;


namespace bytePassion.FileRename.ViewModel
{
	public interface IRenamerViewModel : IViewModel
	{
		ICommand Start            { get; }
		ICommand Abort            { get; }
		ICommand SelectFolder     { get; }
		ICommand UndoLastRenaming { get; }

		bool IsProcessStartable { get; }
		bool IsProcessAbortable { get; }
		bool IsProcessUndoable  { get; }		
		bool IsProcessRunning   { get; }		

		string StartDirectory { get; set; }

		string SearchString  { set; }
		string ReplaceString { set; }

		SearchType  SearchType  { set; get; }
		ReplaceType ReplaceType { set; get; }

		bool SearchParameterCaseSensitivity   { get; set; }
		bool SearchParameterIncludeSubfolders { get; set; }
		bool SearchParameterChangeFolderNames { get; set; }

		ObservableCollection<ColumnDescriptor> Columns   { get; }
		ObservableCollection<FileListItem>     ListItems { get; }

		ObservableCollection<string> LastExecutedStartFolders { get; } 
	}
}
