using bytePassion.Lib.Communication.MessageBus.HandlerCollection;
using bytePassion.Lib.FrameworkExtensions;


namespace bytePassion.Lib.Communication.MessageBus
{
	public class LocalMessageBus<TMessageBase> : DisposingObject, IMessageBus<TMessageBase>
	{
		private readonly IHandlerCollection<TMessageBase> handlerCollection; 

		public LocalMessageBus(IHandlerCollection<TMessageBase> handlerCollection)
		{
			this.handlerCollection = handlerCollection;
		}

		public void RegisterMessageHandler<TMessage>(IMessageHandler<TMessage> messageHandler) 
			where TMessage : TMessageBase
		{
			handlerCollection.Add(messageHandler);
		}

		public void DeregisterMessageHander<TMessage>(IMessageHandler<TMessage> messageHandler) where TMessage : TMessageBase
		{
			handlerCollection.Remove(messageHandler);
		}

		public void Send<TMessage>(TMessage message) 
			where TMessage : TMessageBase
		{
			handlerCollection.GetMessageHandler<TMessage>()
							?.Do(handler => handler.Process(message));
        }

		protected override void CleanUp()
		{		
			handlerCollection.Dispose();
		}
	}
}