using System.Threading.Tasks;
using Dropbox.Api;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.MainPages;
using PrettySecureCloud.Service_References.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.AddService
{
	public class AddServiceViewModel : ViewModelBase
	{
		public AddServiceViewModel(ServiceTypeViewModel serviceType)
		{
			ServiceTypeViewModel = serviceType;

			CloudService = serviceType.Type.ToICloudService();

			AuthenticateCommand = new Command(async () => await AuthenticateAsync());
		}

		private string _loginToken;

		private async Task AuthenticateAsync()
		{
			Workers++;

			try
			{
				_loginToken = await CloudService.AuthenticateLoginTokenAsync();
			}
			catch (AuthException)
			{
				DisplayAlert(this, new MessageData("Fehler", "Fehler beim Authentifizieren", "Ok"));
				return;
			}

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

				PushViewModal(new MasterPage());
			}

			Workers--;
		}

		public ServiceTypeViewModel ServiceTypeViewModel { get; }

		public ICloudService CloudService { get; }

		public Command AuthenticateCommand { get; }
	}
}
