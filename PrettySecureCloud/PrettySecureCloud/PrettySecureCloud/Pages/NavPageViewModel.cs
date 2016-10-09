using System.Collections.ObjectModel;
using System.Linq;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.Infrastructure;
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
					Image = "home.png",
					Page = new NavigationPage(new ServiceChooserPage())
				},
				new NavigationItem
				{
					Title = Settings,
					Image = "settings.png",
					Page = new SettingsPage()
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

				if (_selectedPage == null) return;

				PushView(this, _selectedPage.Page);

				OnPropertyChanged();
			}
		}

	}
}
