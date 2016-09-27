using System;
using System.Collections.Generic;
using PrettySecureCloud.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		void OnLoginClicked(object sender, EventArgs e)
		{
			var service = new LoginService.LoginServiceClient(LoginServiceClient.EndpointConfiguration.BasicHttpBinding_ILoginService);
		    DisplayAlert("Fähler", "Da hetts di gnoh!", "Ja muesch ahneh");
		}

		void OnRegistrationClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new NavigationPage(new RegistrationPage()));

		}

	}
}
