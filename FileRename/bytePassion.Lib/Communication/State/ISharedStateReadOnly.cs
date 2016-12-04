using System;


namespace bytePassion.Lib.Communication.State
{
	public interface ISharedStateReadOnly<out T>
    {
        event Action<T> StateChanged;

        T Value { get; }
    }
}