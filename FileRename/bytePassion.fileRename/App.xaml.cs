using System.Windows;
using bytePassion.FileRename.Repository;
using bytePassion.FileRename.ViewModel;


namespace bytePassion.FileRename {
	
	public partial class App //: Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			///////////////////////////////////////////////////////////////////////////////////////////////
			////////                                                                             //////////
			////////                          Composition Root and Setup                         //////////
			////////                                                                             //////////
			///////////////////////////////////////////////////////////////////////////////////////////////

			// LastUsedFolders-Repository

			var xmlStringDataStore = new XmlStringDataStore("lastProcessedFolders.xml");
			ILastUsedStartFoldersRepository repo = new LastUsedStartFoldersRepository(xmlStringDataStore);
			repo.LoadFromXml();

			// create permanent ViewModels

			var renamerViewModel = new RenamerViewModel(repo.GetAll());

			// create and show main Window

			var mainWindow = new MainWindow()
			{
				DataContext = renamerViewModel
			};

			mainWindow.ShowDialog();


			///////////////////////////////////////////////////////////////////////////////////////////////
			////////                                                                             //////////
			////////             Clean Up and store data after main Window was closed            //////////
			////////                                                                             //////////
			///////////////////////////////////////////////////////////////////////////////////////////////

			repo.Add(renamerViewModel.LastExecutedStartFolders);
			repo.SaveToXml();
		}		
	}
}
