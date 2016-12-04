using bytePassion.Lib.WpfLib.Commands;
using bytePassion.Lib.WpfLib.Dialogs.Input.InputBoxWindow.Helper;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace bytePassion.Lib.WpfLib.Dialogs.Input.InputBoxWindow
{
	public class InputBoxViewModel : IInputBoxViewModel
    {
	    private string input;
	    
        
	    public InputBoxViewModel(string title, string promt, InputBoxResult result)
	    {
		    this.Title = title;
		    this.Promt = promt;
			
		    ConfirmInput = new Command(() =>
			{
				result.Result = input;
				CloseDialog();
			});

			Abort = new Command(() =>
			{
				result.Result = null;
				CloseDialog();
			});
	    }

	    public ICommand ConfirmInput { get; }
	    public ICommand Abort        { get; }

	    public string Title { get; }
	    public string Promt { get; }

	    public string Input { set { input = value; }}

	    private static void CloseDialog()
	    {
			var windows = Application.Current.Windows
											 .OfType<InputBoxWindow>()
											 .ToList();

			if (windows.Count() == 1)
				windows[0].Close();
	    }
    }
}
