using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace bytePassion.FileRename2.ViewModel
{
	public interface IRenamerViewModel : INotifyPropertyChanged
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

		string OriginalNames  { get;      }
		string NewNames       { get; set; }				

		ObservableCollection<string> LastExecutedStartFolders { get; } 
	}
}
