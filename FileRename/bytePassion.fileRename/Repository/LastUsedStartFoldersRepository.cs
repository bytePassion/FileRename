using System.Collections.Generic;
using System.Linq;


namespace bytePassion.FileRename.Repository
{
	public class LastUsedStartFoldersRepository : ILastUsedStartFoldersRepository
	{
		private readonly XmlStringDataStore xmlStringDataStore;

		private ISet<string> lastUsedStartFolders; 

		public LastUsedStartFoldersRepository(XmlStringDataStore xmlStringDataStore)
		{
			this.xmlStringDataStore = xmlStringDataStore;
			lastUsedStartFolders = new HashSet<string>();
		}

		public IEnumerable<string> GetAll()
		{
			return lastUsedStartFolders.ToList();
		}

		public void Add(IEnumerable<string> lastUsedFolders)
		{
			var newLastUsedFolders = lastUsedFolders.Where(lastUsedFolder => !lastUsedStartFolders.Contains(lastUsedFolder)).ToList();

			var newItemsCount = newLastUsedFolders.Count;
			var storedItemsCount = lastUsedStartFolders.Count;

			if (newItemsCount + storedItemsCount > 15)			
				for (int i = 0; i < newItemsCount + storedItemsCount - 15; i++)				
					lastUsedStartFolders.Remove(lastUsedStartFolders.Last());							

			foreach (var lastUsedFolder in newLastUsedFolders)			
				lastUsedStartFolders.Add(lastUsedFolder);				
		}

		public void SaveToXml()
		{
			xmlStringDataStore.Persist(lastUsedStartFolders);
		}

		public void LoadFromXml()
		{
			lastUsedStartFolders = new HashSet<string>();
			Add(xmlStringDataStore.Load());
		}
	}
}