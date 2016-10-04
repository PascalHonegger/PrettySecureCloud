using System.ComponentModel;
using System.ServiceModel;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.Login
{
	public class RegisterViewModel : ViewModelBase
	{
		private string _email;
		private string _password1;
		private string _password2;
		private string _username;

		public RegisterViewModel()
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
			Workers--;
			Service.RegisterCompleted -= RegisterCompleted;

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
		}

		private bool CanRegister()
		{
			return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password1) &&
			       Equals(Password1, Password2);
		}
	}
}