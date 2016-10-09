using System.ServiceModel;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Pages;
using PrettySecureCloud.LoginService;
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
						DisplayAlert(this, new MessageData("Fehler", fault.Message, "Ok"));
					}
					catch (CommunicationException)
					{
						DisplayAlert(this, new MessageData("Keine Verbindung", "Konnte keine Verbindung zum Server herstellen", "Ok"));
					}
				}
				else
				{
					var result = args.Result;
					CurrentSession.CurrentUser = result;
					PushViewModal(this, new MasterPage());
				}
			});
		}

		private void Register()
		{
			PushView(this, new RegistrationPage());
		}
	}
}
