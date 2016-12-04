namespace bytePassion.Lib.Types.Repository
{
	public interface IPersistenceService<T>
	{
		void Persist(T data);
		T Load();
	}
}
