using bytePassion.Lib.Communication.MessageBus;


namespace bytePassion.Lib.Communication.ViewModel.Messages
{
	public interface IViewModelMessageHandler<in TViewModelMessage> : IMessageHandler<TViewModelMessage>
		where TViewModelMessage : ViewModelMessage
	{
	}
}
