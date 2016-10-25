using System.ComponentModel;
using System.ServiceModel;
using System.Text.RegularExpressions;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.Login
{
	public class RegistrationViewModel : ViewModelBase
	{
		private string _email = "";
		private string _password1 = "";
		private string _password2 = "";
		private string _username = "";

		public RegistrationViewModel()
		{
			RegisterCommand = new Command(Register, CanRegister);
		}

		public Command RegisterCommand { get; }

		public string Username
		{
			get { return _username; }
			set
			{
				if (Equals(_username, value)) return;
				_username = value;
				OnPropertyChanged();
				RegisterCommand.ChangeCanExecute();
			}
		}

		public string Email
		{
			get { return _email; }
			set
			{
				if (Equals(_email, value)) return;
				_email = value;
				OnPropertyChanged();
				RegisterCommand.ChangeCanExecute();
			}
		}

		public string Password1
		{
			get { return _password1; }
			set
			{
				if (Equals(_password1, value)) return;
				_password1 = value;
				OnPropertyChanged();
				RegisterCommand.ChangeCanExecute();
			}
		}

		public string Password2
		{
			get { return _password2; }
			set
			{
				if (Equals(_password2, value)) return;
				_password2 = value;
				OnPropertyChanged();
				RegisterCommand.ChangeCanExecute();
			}
		}

		private void Register()
		{
			Service.RegisterCompleted += RegisterCompleted;

			Workers++;

			Service.RegisterAsync(Username, Email, Password1);
		}

		private void RegisterCompleted(object sender, AsyncCompletedEventArgs args)
		{
			Service.RegisterCompleted -= RegisterCompleted;

			if (HandleException(this, args))
			{
				var LoginViewModel = new LoginViewModel();
				LoginViewModel.Username = Username;
				LoginViewModel.Password = Password1;
				LoginViewModel.Login();
			}

			Workers--;
		}

		public bool CanRegister()
		{
			return IsInputValid();
		}

		private bool IsInputValid()
		{
			const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

			var isUsernameValid = Username.Length > 3 && Username.Length < 20;
			var regex = new Regex(emailRegex);
			var isEmailValid = regex.IsMatch(Email);
			var isPasswordValid = PasswordValid(Password1, Password2);
			return isPasswordValid && isUsernameValid && isEmailValid;
		}

		public static bool PasswordValid(string password1, string password2)
		{
			return Equals(password1, password2) && password1.Length > 8;
		}
	}
}