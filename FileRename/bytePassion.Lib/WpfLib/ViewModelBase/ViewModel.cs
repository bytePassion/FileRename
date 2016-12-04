using System.ComponentModel;
using bytePassion.Lib.FrameworkExtensions;

namespace bytePassion.Lib.WpfLib.ViewModelBase
{
	public abstract class ViewModel : DisposingObject, 
                                      IViewModel                                      
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
    }
}