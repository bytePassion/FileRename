using System.Collections.Generic;
using System.Linq;


namespace bytePassion.Lib.Communication.ViewModel
{
	public class ViewModelCollection <TIdent> 
	{		
		private readonly IList<IViewModelCollectionItem<TIdent>> viewModels;

		public ViewModelCollection()
		{			
			viewModels = new List<IViewModelCollectionItem<TIdent>>();
		}

		public void AddViewModel<TViewModel>(TViewModel viewModel)
			where TViewModel : IViewModelCollectionItem<TIdent>
		{
			viewModels.Add(viewModel);
		}

		public void RemoveViewModel<TViewModel>(TViewModel viewModel)
			where TViewModel : IViewModelCollectionItem<TIdent>
		{
			viewModels.Remove(viewModel);
		}

		public IViewModelCollectionItem<TIdent> GetViewModel (TIdent identifier)
		{
			return viewModels.FirstOrDefault(viewModel => viewModel.Identifier.Equals(identifier));
		}

	    public IEnumerable<IViewModelCollectionItem<TIdent>> GetAllViewModelsFromCollection()
	    {
	        return viewModels.ToList();
	    } 		
	}
}