using System.ComponentModel;


namespace bytePassion.Lib.Utils
{
	public interface IUndoRedo : INotifyPropertyChanged
	{
		void Undo();
		void Redo();

		bool UndoPossible { get; }
		bool RedoPossible { get; }
	}
}
