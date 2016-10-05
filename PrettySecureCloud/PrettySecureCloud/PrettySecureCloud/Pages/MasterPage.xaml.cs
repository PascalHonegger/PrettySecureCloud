using System.Threading.Tasks;
using PrettySecureCloud.Model;
using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public partial class MasterPage
	{
		public MasterPage()
		{
			InitializeComponent();

			NavPage.ListView.ItemSelected += NavListViewOnItemSelected;
			NavPage.ListView.Footer = string.Empty;
		}

		private async void NavListViewOnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;
			var item = (Startscreen) e.SelectedItem;

			switch (item.Title)
			{
				case "Dienste":
					Detail = new MasterPage();
					break;
				default:
					await DisplayAlert("Fehler", "Fehler beim Laden der Seite", "OK");
					break;
			}
			((ListView) sender).SelectedItem = null;
			IsPresented = false;
		}

		private async Task PushAsync(Page page, bool clearStack = true)
		{
			if (clearStack)
			{
				await NavigationStack.PopToRootAsync(true);
			}

			await NavigationStack.PushAsync(page);
		}
	}
}