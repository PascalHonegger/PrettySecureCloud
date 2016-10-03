using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.Login
{
	public class RegisterViewModel : ViewModelBase
	{
		public Command RegisterCommand { get; }

		public RegisterViewModel()
		{
			RegisterCommand = new Command(Register, CanRegister);
		}

		private void Register()
		{
			//TODO
		}

		private bool CanRegister()
		{
			//TODO
			return true;
		}
	}
}
