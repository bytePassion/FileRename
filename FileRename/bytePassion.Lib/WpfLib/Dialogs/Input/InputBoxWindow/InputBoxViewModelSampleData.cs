using System.Windows.Input;


namespace bytePassion.Lib.WpfLib.Dialogs.Input.InputBoxWindow
{
    
	public class InputBoxViewModelSampleData : IInputBoxViewModel
	{
		public InputBoxViewModelSampleData()
		{
			Title = "demoTitle";
			Promt = "demoPromt";
		}

	    public ICommand ConfirmInput { get; } = null;
	    public ICommand Abort        { get; } = null;
	    
		public string Title { get; }
		public string Promt { get; }

		public string Input { set {} }
	}

}