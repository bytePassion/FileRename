namespace bytePassion.Lib.Communication.MessageBus
{
	/// <summary>
	///		NOT IN USE ... JUST AN IDEA
	/// </summary>	
	public interface IRequestHandler<in TMessage, out TResult>
	{
		TResult Process(TMessage message);
	}
}
