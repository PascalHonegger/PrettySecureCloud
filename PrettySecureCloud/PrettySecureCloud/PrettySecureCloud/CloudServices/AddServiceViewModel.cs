using System.Collections.ObjectModel;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Service_References.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices
{
	public class AddServiceViewModel : ViewModelBase
	{
		private ServiceType _selectedServiceType;

		public AddServiceViewModel()
		{
			Workers++;

			AddServiceCommand = new Command(AddService, CanAddService);

			ServiceTypes = new ObservableCollection<ServiceTypeViewModel>();

			Service.LoadAllServicesCompleted += ServiceOnLoadAllServicesCompleted;

			Service.LoadAllServicesAsync();
		}

		private bool CanAddService() => SelectedServiceType != null;

		private void AddService()
		{
			DisplayAlert(this, new MessageData("TODO", "Diese Funktion ist noch in Entwicklung", "Ok :("));
		}

		private void ServiceOnLoadAllServicesCompleted(object sender, LoadAllServicesCompletedEventArgs loadAllServicesCompletedEventArgs)
		{
			Workers--;
			foreach (var serviceType in loadAllServicesCompletedEventArgs.Result)
			{
				ServiceTypes.Add(new ServiceTypeViewModel(serviceType));
			}
		}

		public Command AddServiceCommand { get; }

		public ObservableCollection<ServiceTypeViewModel> ServiceTypes { get; }

		public ServiceType SelectedServiceType
		{
			get { return _selectedServiceType; }
			set
			{
				if (Equals(_selectedServiceType, value)) return;
				_selectedServiceType = value;
				OnPropertyChanged();
				AddServiceCommand.ChangeCanExecute();
			}
		}
	}
}
