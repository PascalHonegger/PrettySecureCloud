using System.ComponentModel;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Login;
using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public class SettingsViewModel : ViewModelBase
	{
		private string _oldPassword = "";
		private string _newPassword1 = "";
		private string _newPassword2 = "";

		public SettingsViewModel()
		{
			ChangePasswordCommand = new Command(ChangePassword, CanChangePassword);
			LogoutCommand = new Command(Logout);
		}

		private void Logout()
		{
			CurrentSession.CurrentUser = null;
			PushViewModal(new NavigationPage(new LoginPage()));
		}

		public Command ChangePasswordCommand { get; }
		public Command LogoutCommand { get; }

		public string OldPassword
		{
			get { return _oldPassword; }
			set
			{
				if (Equals(_oldPassword, value)) return;
				_oldPassword = value;
				OnPropertyChanged();
				ChangePasswordCommand.ChangeCanExecute();
			}
		}

		public string NewPassword1
		{
			get { return _newPassword1; }
			set
			{
				if (Equals(_newPassword1, value)) return;
				_newPassword1 = value;
				OnPropertyChanged();
				ChangePasswordCommand.ChangeCanExecute();
			}
		}

		public string NewPassword2
		{
			get { return _newPassword2; }
			set
			{
				if (Equals(_newPassword2, value)) return;
				_newPassword2 = value;
				OnPropertyChanged();
				ChangePasswordCommand.ChangeCanExecute();
			}
		}

		private bool CanChangePassword()
			=> !string.IsNullOrEmpty(OldPassword) && RegistrationViewModel.PasswordValid(NewPassword1, NewPassword2);

		private void ChangePassword()
		{
			Workers++;

			Service.ChangePasswordCompleted += ChangePasswordCompleted;

			Service.ChangePasswordAsync(CurrentSession.CurrentUser.Id, OldPassword, NewPassword1);
		}

		private void ChangePasswordCompleted(object sender, AsyncCompletedEventArgs args)
		{
			Service.ChangePasswordCompleted -= ChangePasswordCompleted;

			if (HandleException(this, args))
			{
				DisplayAlert(this, new MessageData("Passwort geändert", "Dein Passwort wurde erfolgreich geändert", "Ok"));
			}

			Workers--;
		}
	}
}