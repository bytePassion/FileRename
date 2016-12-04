using System.Windows;
using bytePassion.FileRename2.Repository;
using bytePassion.FileRename2.ViewModel;

namespace bytePassion.FileRename2
{

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

			var readModel = new RenamerViewModel(repo.GetAll());



			// create and show main Window

			var mainWindow = new MainWindow()
			{
				DataContext = readModel
			};

			mainWindow.ShowDialog();





			///////////////////////////////////////////////////////////////////////////////////////////////
			////////                                                                             //////////
			////////             Clean Up and store data after main Window was closed            //////////
			////////                                                                             //////////
			///////////////////////////////////////////////////////////////////////////////////////////////

			repo.Add(readModel.LastExecutedStartFolders);
			repo.SaveToXml();
		}		
	}
}
