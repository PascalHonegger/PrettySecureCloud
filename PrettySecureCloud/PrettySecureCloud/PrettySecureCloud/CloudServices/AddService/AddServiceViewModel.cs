using System.Threading.Tasks;
using Dropbox.Api;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.MainPages;
using PrettySecureCloud.Service_References.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.AddService
{
	/// <summary>
	/// This class adds a new Service 
	/// </summary>
	public class AddServiceViewModel : ViewModelBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AddServiceViewModel" /> class.
		/// </summary>
		/// <param name="serviceType"></param>
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

		/// <summary>
		/// Types of Services
		/// </summary>
		public ServiceTypeViewModel ServiceTypeViewModel { get; }

		/// <summary>
		/// A CloudService Object
		/// </summary>
		public ICloudService CloudService { get; }

		/// <summary>
		/// Authenticate the Service
		/// </summary>
		public Command AuthenticateCommand { get; }
	}
}
