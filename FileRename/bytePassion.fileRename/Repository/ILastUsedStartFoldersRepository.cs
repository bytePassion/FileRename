using System.Collections.Generic;


namespace bytePassion.FileRename.Repository
{
	public interface ILastUsedStartFoldersRepository
	{
		IEnumerable<string> GetAll();
		void Add(IEnumerable<string> lastUsedFolders);

		void SaveToXml();
		void LoadFromXml();
	}
}
