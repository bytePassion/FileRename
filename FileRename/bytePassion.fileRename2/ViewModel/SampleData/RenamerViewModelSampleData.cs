using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

#pragma warning disable 0067

namespace bytePassion.FileRename2.ViewModel.SampleData
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
			
			LastExecutedStartFolders = new ObservableCollection<string>
			                           {
				                           @"C:\test.txt",
										   @"D:\test2.txt"
			                           };

			OriginalNames = 
				"fileOriginal1\n"+
				"fileOriginal2\n"+
				"fileOriginal3\n"+
				"fileOriginal4\n";

			NewNames = 
				"fileNew1\n"+
				"fileNew2\n"+
				"fileNew3\n"+
				"fileNew4";
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

		public string OriginalNames { get; set; }
		public string NewNames      { get; set; }
		
		public ObservableCollection<string> LastExecutedStartFolders { get; }

		public event PropertyChangedEventHandler PropertyChanged;		
	}
}
