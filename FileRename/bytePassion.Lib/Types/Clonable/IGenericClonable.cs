
namespace bytePassion.Lib.Types.Clonable {

	public interface IGenericClonable<out T> 
	{
		T Clone ();
	}
}
