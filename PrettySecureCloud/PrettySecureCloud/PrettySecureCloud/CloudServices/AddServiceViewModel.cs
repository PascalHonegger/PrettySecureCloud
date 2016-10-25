using PrettySecureCloud.CloudServices.Implementations;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices
{
	public class AddServiceViewModel : ViewModelBase
	{
		private string _loginToken;

		public AddServiceViewModel(ServiceTypeViewModel serviceType)
		{
			ServiceTypeViewModel = serviceType;

			CloudService = serviceType.Type.ToICloudService();

			AuthenticateCommand = new Command(Authenticate);
		}

		private async void Authenticate()
		{
			Workers++;

			_loginToken = await CloudService.AuthenticateLoginTokenAsync();

			Service.AddServiceCompleted += ServiceOnAddServiceCompleted;
			Service.AddServiceAsync(CurrentSession.CurrentUser.Id, ServiceTypeViewModel.Type.Id, CloudService.CustomName, _loginToken);
		}

		private void ServiceOnAddServiceCompleted(object sender, AddServiceCompletedEventArgs addServiceCompletedEventArgs)
		{
			if (HandleException(this, addServiceCompletedEventArgs))
			{
				Workers--;

				//TODO Return back
				DisplayAlert(this, new MessageData("Yay", $"You are now authenticated! {_loginToken}", "YAY!!"));
			}
		}

		public ServiceTypeViewModel ServiceTypeViewModel { get; }

		public ICloudService CloudService { get; }

		public Command AuthenticateCommand { get; }
	}
}
