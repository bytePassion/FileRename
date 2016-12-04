namespace bytePassion.Lib.Communication.ViewModel
{
	public interface IViewModelCollectionItem<out TIdent>
	{
		TIdent Identifier { get; }
	}
}
