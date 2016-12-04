using System.Linq;
using bytePassion.Lib.Communication.MessageBus;
using bytePassion.Lib.Communication.ViewModel.Messages;
using bytePassion.Lib.FrameworkExtensions;


namespace bytePassion.Lib.Communication.ViewModel
{

	public class ViewModelCommunication : IViewModelCommunication
	{
		private readonly IMessageBus<ViewModelMessage> viewModelMessageBus;
		private readonly IViewModelCollectionList      viewModelCollectionList;				

		public ViewModelCommunication(IMessageBus<ViewModelMessage> viewModelMessageBus, 								
									  IViewModelCollectionList viewModelCollectionList)
		{
			this.viewModelMessageBus = viewModelMessageBus;			
			this.viewModelCollectionList = viewModelCollectionList;			
		}


		///////////////////////////////////////////////////////////////////////////////////////////////////////
		/////////                                                                                   ///////////
		/////////                               viewModelCollections                                ///////////
		/////////                                                                                   ///////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////
		
		public void CreateViewModelCollection<TViewModel, TIdent>(string identifier)
			where TViewModel : IViewModelCollectionItem<TIdent>
		{
			viewModelCollectionList.CreateViewModelCollection<TViewModel, TIdent>(identifier);
		}

		public void RemoveViewModelCollection(string identifier)
		{
			viewModelCollectionList.RemoveViewModelCollection(identifier);
		}

		public void RegisterViewModelAtCollection<TViewModel, TIdent>(string collectionIdentifier, 
																	  TViewModel viewModel)
			where TViewModel : IViewModelCollectionItem<TIdent>
		{
			var viewModelCollection = viewModelCollectionList.GetViewModelCollection<TIdent>(collectionIdentifier);
			viewModelCollection.AddViewModel(viewModel);
		}

		public void DeregisterViewModelAtCollection<TViewModel, TIdent>(string collectionIdentifier, TViewModel viewModel)
			where TViewModel : IViewModelCollectionItem<TIdent>
		{
			var viewModelCollection = viewModelCollectionList.GetViewModelCollection<TIdent>(collectionIdentifier);
			viewModelCollection.RemoveViewModel(viewModel);
		}

		public void SendTo<TIdent, TMessage>(string viewModelCollectionIdentifier, 
											 TIdent viewModelIdentifier, 
											 TMessage message)
			where TMessage : ViewModelMessage
		{
			var viewModelCollection = viewModelCollectionList.GetViewModelCollection<TIdent>(viewModelCollectionIdentifier);

			var viewModel = viewModelCollection.GetViewModel(viewModelIdentifier);

			var viewModelAsMessageHandler = viewModel as IViewModelMessageHandler<TMessage>;
			viewModelAsMessageHandler?.Process(message);
		}

	    public void SendToCollection<TIdent, TMessage>(string viewModelCollectionIdentifier, TMessage message) 
            where TMessage : ViewModelMessage
	    {
	        var viewModelCollection = viewModelCollectionList.GetViewModelCollection<TIdent>(viewModelCollectionIdentifier);

	        viewModelCollection.GetAllViewModelsFromCollection()
                               .Select(viewModel => viewModel as IViewModelMessageHandler<TMessage>)
                               .Do(viewModelAsMessageHandler => viewModelAsMessageHandler?.Process(message));
	    }
	   

		///////////////////////////////////////////////////////////////////////////////////////////////////////
		/////////                                                                                   ///////////
		/////////                               viewModelMessageBus                                 ///////////
		/////////                                                                                   ///////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////

		public void RegisterViewModelMessageHandler<TMessage>(IViewModelMessageHandler<TMessage> viewModelMessageHandler)
			where TMessage: ViewModelMessage
		{
			viewModelMessageBus.RegisterMessageHandler(viewModelMessageHandler);
		}

		public void DeregisterViewModelMessageHandler<TMessage>(IViewModelMessageHandler<TMessage> viewModelMessageHandler)
			where TMessage : ViewModelMessage
		{
			viewModelMessageBus.DeregisterMessageHander(viewModelMessageHandler);
		}

		public void Send<TMessage>(TMessage message) 
			where TMessage : ViewModelMessage
		{
			viewModelMessageBus.Send(message);
		}
		
	}
}
