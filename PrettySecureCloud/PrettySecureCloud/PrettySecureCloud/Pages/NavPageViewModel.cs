using System.Collections.ObjectModel;
using System.Linq;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Model;
using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public class NavPageViewModel : ViewModelBase
	{
		public const string CloudServices = "Dienste";
		public const string Settings = "Einstellungen";

		public NavPageViewModel()
		{
			AvailablePages = new ObservableCollection<NavigationItem>
			{
				new NavigationItem
				{
					Title = CloudServices,
					Image = "home.png"
				},
				new NavigationItem
				{
					Title = Settings
				}
			};

			SelectedPage = AvailablePages.First();
		}

		public ObservableCollection<NavigationItem> AvailablePages { get; }

		private NavigationItem _selectedPage;

		public NavigationItem SelectedPage
		{
			get { return _selectedPage; }
			set
			{
				_selectedPage = value;
				OnPropertyChanged();

				if (_selectedPage == null) return;

				switch (_selectedPage.Title)
				{
					case CloudServices:
						PushView(this, new NavigationPage(new ServiceChooser()));
						break;
					case Settings:
						PushView(this, new SettingsPage());
						break;
					default:
						DisplayAlert(this, new MessageData("Fehler", "Fehler beim Laden der Seite", "OK"));
						break;
				}
			}
		}

	}
}
