using System;
using System.Collections.Generic;


namespace bytePassion.Lib.Communication.MessageBus.HandlerCollection
{
	public interface IHandlerCollection<in TMessageBase> : IDisposable
	{
		void Add <TMessage> (IMessageHandler<TMessage> newMessageHandler) 
			where TMessage : TMessageBase;
		 
		void Remove<TMessage> (IMessageHandler<TMessage> messageHandlerToRemove)
			where TMessage : TMessageBase;

		IEnumerable<IMessageHandler<TMessage>> GetMessageHandler<TMessage> () 
			where TMessage : TMessageBase;

		void RemoveAllHandler();
	}
}