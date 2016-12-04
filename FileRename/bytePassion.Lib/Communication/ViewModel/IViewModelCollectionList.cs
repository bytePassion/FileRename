namespace bytePassion.Lib.Communication.ViewModel
{
	public interface IViewModelCollectionList
	{
		void CreateViewModelCollection<TViewModel, TIdent>(string identifier)
			where TViewModel : IViewModelCollectionItem<TIdent>;

		void RemoveViewModelCollection(string identifier);

		ViewModelCollection<TIdent> GetViewModelCollection<TIdent>(string identifier);
	}

}