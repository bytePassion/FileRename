using System;

namespace bytePassion.Lib.Communication.MessageBus
{
	public interface IMessageBus<in TMessageBase> : IDisposable
	{
		void RegisterMessageHandler<TMessage>(IMessageHandler<TMessage> messageHandler) 
			where TMessage : TMessageBase;

		void DeregisterMessageHander<TMessage>(IMessageHandler<TMessage> messageHandler)
			where TMessage : TMessageBase;

		void Send<TMessage>(TMessage message) 
			where TMessage : TMessageBase;		
	}
}