using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public class SettingsViewModel : ViewModelBase
	{
		public Command ChangePasswordCommand { get; }

		public SettingsViewModel()
		{
			ChangePasswordCommand = new Command(ChangePassword, CanChangePassword);
		}

		private bool CanChangePassword()
		{
			//TODO
			return true;
		}

		private void ChangePassword()
		{
			//TODO
			throw new System.NotImplementedException();
		}
	}
}