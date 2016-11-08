using PrettySecureCloud.Login;
using Xamarin.Forms;

namespace PrettySecureCloud
{
	/// <summary>
	///class App gets called by startup
	/// </summary>
	public class App : Application
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="App" /> class.
		/// </summary>
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