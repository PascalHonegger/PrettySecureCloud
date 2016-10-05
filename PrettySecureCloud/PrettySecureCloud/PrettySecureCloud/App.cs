﻿using PrettySecureCloud.Pages;
using Xamarin.Forms;
using LoginPage = PrettySecureCloud.Login.LoginPage;

namespace PrettySecureCloud
{
	public class App : Application
	{
		public App()
		{
			// The root page of your application
			MainPage = new NavigationPage(new LoginPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}