using System;


namespace bytePassion.Lib.WpfLib.Commands.Updater
{
	public interface ICommandUpdater : IDisposable
    {
        event EventHandler UpdateOfCanExecuteChangedRequired;
    }
}