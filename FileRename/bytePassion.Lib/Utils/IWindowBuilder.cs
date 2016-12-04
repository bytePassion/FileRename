namespace bytePassion.Lib.Utils
{
	public interface IWindowBuilder<TWindow>
	{
		TWindow BuildWindow ();
		void DisposeWindow(TWindow buildedWindow);
	}
}
