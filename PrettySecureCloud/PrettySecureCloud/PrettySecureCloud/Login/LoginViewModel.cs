using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.MainPages;
using PrettySecureCloud.Service_References.LoginService;
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

		public void Login()
		{
			Service.LoginCompleted += LoginCompleted;

			Workers++;

			Service.LoginAsync(Username, Password);
		}

		private void LoginCompleted(object sender, LoginCompletedEventArgs args)
		{
			Service.LoginCompleted -= LoginCompleted;

			if (HandleException(this, args))
			{
				var result = args.Result;
				CurrentSession.CurrentUser = result;
				PushViewModal(new MasterPage());
			}

			Workers--;
		}

		private void Register()
		{
			PushView(this, new RegistrationPage());
		}
	}
}
