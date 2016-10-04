using System.ServiceModel;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.LoginService;
using PrettySecureCloud.Pages;
using Xamarin.Forms;

namespace PrettySecureCloud.Login
{
	public class LoginViewModel : ViewModelBase
	{
		private string _username;
		private string _password;
		public Command LoginCommand { get; }
		public Command RegisterCommand { get; }

		public LoginViewModel()
		{
			LoginCommand = new Command(Login, CanLogin);
			RegisterCommand = new Command(Register);
		}

		public bool CanLogin()
		{
			//TODO 
			// var entry = (Entry)sender;
			// entry.PlaceholderColor = !string.IsNullOrEmpty(entry.Text) ? Color.Default : Color.Red;
			return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
		}

		public string Username
		{
			get { return _username; }
			set
			{
				if (Equals(_username, value)) return;
				_username = value;
				OnPropertyChanged();
				LoginCommand.ChangeCanExecute();
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				if (Equals(_password, value)) return;
				_password = value;
				OnPropertyChanged();
				LoginCommand.ChangeCanExecute();
			}
		}

		private void Login()
		{
			Service.LoginCompleted += LoginCompleted;

			Workers++;

			Service.LoginAsync(Username, Password);
		}

		private void LoginCompleted(object sender, LoginCompletedEventArgs args)
		{
			Workers--;

			Service.LoginCompleted -= LoginCompleted;
			Device.BeginInvokeOnMainThread(() =>
			{
				if (args.Error != null)
				{
					try
					{
						throw args.Error;
					}
					catch (FaultException fault)
					{
						DisplayAlert(this, new MessageData("Failure", fault.Message, "Ok"));
					}
				}
				else
				{
					var result = args.Result;
					DisplayAlert(this, new MessageData(result.Username, "Da hetts di gnoh!", "Ja muesch ahneh"));
				}
			});
		}

		private void Register()
		{
			PushView(this, new RegistrationPage());
		}
	}
}
