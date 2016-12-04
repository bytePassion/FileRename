using bytePassion.Lib.WpfLib.Dialogs.Input.InputBoxWindow;
using bytePassion.Lib.WpfLib.Dialogs.Input.InputBoxWindow.Helper;
using System.Windows;


namespace bytePassion.Lib.WpfLib.Dialogs.Input
{
    public static class InputBox
    {
	    public static string GetString(string title, string promt, Window owner=null)
	    {
			var result = new InputBoxResult();

		    new InputBoxWindow.InputBoxWindow
		    {
			    DataContext = new InputBoxViewModel(title, promt, result),
				Owner = owner ?? Application.Current.MainWindow,			
		    }
			.ShowDialog();		    			

		    return result.Result;
	    }
    }
}
