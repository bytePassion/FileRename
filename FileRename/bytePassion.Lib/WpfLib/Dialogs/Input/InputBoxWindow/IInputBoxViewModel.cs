using System.Windows.Input;


namespace bytePassion.Lib.WpfLib.Dialogs.Input.InputBoxWindow
{
	public interface IInputBoxViewModel
	{
		ICommand ConfirmInput { get; }
		ICommand Abort        { get; }

		string Title { get; }
		string Promt { get; }
		string Input { set; }
	}
}