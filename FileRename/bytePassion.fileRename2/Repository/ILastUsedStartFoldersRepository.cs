using System.Collections.Generic;

namespace bytePassion.FileRename2.Repository
{
	public interface ILastUsedStartFoldersRepository
	{
		IEnumerable<string> GetAll();
		void Add(IEnumerable<string> lastUsedFolders);

		void SaveToXml();
		void LoadFromXml();
	}
}
