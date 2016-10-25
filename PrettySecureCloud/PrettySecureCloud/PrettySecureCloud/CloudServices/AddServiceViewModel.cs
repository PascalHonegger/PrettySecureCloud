using PrettySecureCloud.CloudServices.Implementations;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.LoginService;
using PrettySecureCloud.Pages;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices
{
	public class AddServiceViewModel : ViewModelBase
	{
		public AddServiceViewModel(ServiceTypeViewModel serviceType)
		{
			ServiceTypeViewModel = serviceType;

			CloudService = serviceType.Type.ToICloudService();

			AuthenticateCommand = new Command(Authenticate);
		}

		private string _loginToken;

		private async void Authenticate()
		{
			Workers++;

			_loginToken = await CloudService.AuthenticateLoginTokenAsync();

			Service.AddServiceCompleted += AddServiceCompleted;
			Service.AddServiceAsync(CurrentSession.CurrentUser.Id, ServiceTypeViewModel.Type.Id, CloudService.CustomName, _loginToken);
		}

		private void AddServiceCompleted(object sender, AddServiceCompletedEventArgs addServiceCompletedEventArgs)
		{
			Service.AddServiceCompleted -= AddServiceCompleted;

			if (HandleException(this, addServiceCompletedEventArgs))
			{
				//Add service so it's visible to the user
				var addedService = new CloudService
				{
					Id = addServiceCompletedEventArgs.Result,
					LoginToken = _loginToken,
					Name = CloudService.CustomName,
					Type = CloudService.CloudServiceType.Type
				};

				CurrentSession.CurrentUser.Services.Add(addedService);

				PushViewModal(this, new MasterPage());
			}

			Workers--;
		}

		public ServiceTypeViewModel ServiceTypeViewModel { get; }

		public ICloudService CloudService { get; }

		public Command AuthenticateCommand { get; }
	}
}
