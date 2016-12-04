using System;
using System.Collections.Generic;
using bytePassion.Lib.FrameworkExtensions;


namespace bytePassion.Lib.Communication.MessageBus.HandlerCollection
{

	public class SingleHandlerCollection<TMessageBase> : DisposingObject, 
													     IHandlerCollection<TMessageBase>
	{
		private readonly IDictionary<Type, object> messageHandlers;

		public SingleHandlerCollection ()
		{
			messageHandlers = new Dictionary<Type, object>();
		}		

		public void Add<TCommand>(IMessageHandler<TCommand> newCommandHandler) where TCommand : TMessageBase
		{
			if (messageHandlers.ContainsKey(typeof(TCommand)))
				throw new InvalidOperationException("There can only be one handler per MessageType");

			messageHandlers.Add(typeof(TCommand), newCommandHandler);
		}

		public void Remove<TCommand> (IMessageHandler<TCommand> messageHandlerToRemove) where TCommand : TMessageBase
		{
			if (!messageHandlers.ContainsKey(typeof(TCommand)))
				throw new InvalidOperationException("The handler to be deleted does not exist");

			messageHandlers.Remove(typeof (TCommand));
		}

		public IEnumerable<IMessageHandler<TCommand>> GetMessageHandler<TCommand>() where TCommand : TMessageBase
		{
			if (!messageHandlers.ContainsKey(typeof(TCommand)))
				return null;

			return new List<IMessageHandler<TCommand>>
			{
				(IMessageHandler<TCommand>)messageHandlers[typeof(TCommand)]
			};
		}

		public void RemoveAllHandler()
		{
			messageHandlers.Clear();
		}

		protected override void CleanUp()
		{
			RemoveAllHandler();
		}
	}

}