using System;
using PrettySecureCloud.LoginService;
using Xamarin.Forms;
using PrettySecureCloud.Theme;
namespace PrettySecureCloud.Pages
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		private void OnLoginClicked(object sender, EventArgs e)
		{
			var service = new LoginServiceClient(LoginServiceClient.EndpointConfiguration.BasicHttpsBinding_ILoginService);
			service.LoginCompleted += (o, args) =>
			{
				var result = args.Result;
				DisplayAlert(result.Username, "Da hetts di gnoh!", "Ja muesch ahneh");
			};
			service.LoginAsync("Random user", "123");
		}

		private void OnRegistrationClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new NavigationPage(new RegistrationPage()));

		}

		void OnComplete(object sender, EventArgs e)
		{
			Entry entry = (Entry)sender;
			entry.PlaceholderColor = !string.IsNullOrEmpty(entry.Text) ? Color.Default : Color.Red;
			if (!string.IsNullOrEmpty(UsernameEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text))
			{
				Login.IsEnabled = true;
				Login.BackgroundColor = Colors.LogoBlue;
			}
			else {
				Login.IsEnabled = false;
				Login.BackgroundColor = Color.Transparent;
			}
		}

	}
}
