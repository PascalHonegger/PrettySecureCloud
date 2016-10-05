using System.Collections.ObjectModel;
using System.Linq;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.CloudServices
{
	public class ServiceChooserViewModel : ViewModelBase
	{
		public ServiceChooserViewModel()
		{
			/* Testdata:
			var dropbox = new ServiceType
			{
				Id = 1,
				Key = "ADSFASDFASF",
				Name = "DropBox",
				Secret = "687968795689"
			};

			CurrentSession.CurrentUser.Services.Add(new CloudService
			{
				Id = 1,
				LoginToken = "QWERQWER",
				Name = "Dropbox Privat",
				Type = dropbox
			});

			CurrentSession.CurrentUser.Services.Add(new CloudService
			{
				Id = 2,
				LoginToken = "QWERQWER",
				Name = "Dropbox Arbeit",
				Type = dropbox
			});

			dropbox.Id = 2;
			dropbox.Name = "OneDrive";

			CurrentSession.CurrentUser.Services.Add(new CloudService
			{
				Id = 3,
				LoginToken = "QWERQWER",
				Name = "OneDrive",
				Type = dropbox
			});
			*/
			CloudServices = new ObservableCollection<CloudService>(CurrentSession.CurrentUser.Services);
		}

		private string _searchText;
		private CloudService _selectedCloudService;
		public ObservableCollection<CloudService> CloudServices { get; }

		public CloudService SelectedCloudService
		{
			get { return _selectedCloudService; }
			set
			{
				if (Equals(_selectedCloudService, value)) return;
				_selectedCloudService = value;
				DisplayAlert(this, new MessageData("Ausgewählt", $"Sie haben {_selectedCloudService.Name} ausgewählt", "Ok"));
			}
		}

		public string SearchText
		{
			get { return _searchText; }
			set
			{
				if (Equals(_searchText, value)) return;
				_searchText = value;
				OnPropertyChanged();

				CloudServices.Clear();

				foreach (var service in CurrentSession.CurrentUser.Services.Where(s => s.Name.ToLower().Contains(_searchText.ToLower())))
				{
					CloudServices.Add(service);
				}
			}
		}
	}
}
