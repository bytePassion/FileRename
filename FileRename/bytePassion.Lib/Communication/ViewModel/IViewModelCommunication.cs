using bytePassion.Lib.Communication.ViewModel.Messages;


namespace bytePassion.Lib.Communication.ViewModel
{
	public interface IViewModelCommunication
    {

		///////////////////////////////////////////////////////////////////////////////////////////////////////
		/////////                                                                                   ///////////
		/////////                               viewModelCollections                                ///////////
		/////////                                                                                   ///////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////


		void CreateViewModelCollection<TViewModel, TIdent>(string identifier)
			where TViewModel : IViewModelCollectionItem<TIdent>;

		void RemoveViewModelCollection(string identifier);

		void RegisterViewModelAtCollection<TViewModel, TIdent>(string collectionIdentifier, 
		                                                       TViewModel viewModel)
			where TViewModel : IViewModelCollectionItem<TIdent>;

		void DeregisterViewModelAtCollection<TViewModel, TIdent>(string collectionIdentifier, TViewModel viewModel)
			where TViewModel : IViewModelCollectionItem<TIdent>;

		void SendTo<TIdent, TMessage>(string viewModelCollectionIdentifier, 
		                              TIdent viewModelIdentifier, 
		                              TMessage message)
			where TMessage : ViewModelMessage;

	    void SendToCollection<TIdent, TMessage>(string viewModelCollectionIdentifier,
	                                            TMessage message)
	        where TMessage : ViewModelMessage;       


		///////////////////////////////////////////////////////////////////////////////////////////////////////
		/////////                                                                                   ///////////
		/////////                               viewModelMessageBus                                 ///////////
		/////////                                                                                   ///////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////


		void RegisterViewModelMessageHandler<TMessage>(IViewModelMessageHandler<TMessage> viewModelMessageHandler)
			where TMessage: ViewModelMessage;

		void DeregisterViewModelMessageHandler<TMessage>(IViewModelMessageHandler<TMessage> viewModelMessageHandler)
			where TMessage : ViewModelMessage;

		void Send<TMessage>(TMessage message) 
			where TMessage : ViewModelMessage;
	}
}