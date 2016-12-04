namespace bytePassion.Lib.Communication.MessageBus
{
	public interface IMessageHandler<in TMessage>
	{
		void Process(TMessage message);
	}
}