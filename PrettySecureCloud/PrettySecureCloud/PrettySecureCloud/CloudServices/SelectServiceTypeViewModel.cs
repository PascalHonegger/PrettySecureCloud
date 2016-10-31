using System.Collections.ObjectModel;
using System.Linq;
using PrettySecureCloud.CloudServices.AddService;
using PrettySecureCloud.CloudServices.Implementations;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Service_References.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices
{
	public class SelectServiceTypeViewModel : ViewModelBase
	{
		private ServiceTypeViewModel _selectedServiceType;

		public SelectServiceTypeViewModel()
		{
			Workers++;

			ServiceTypes = new ObservableCollection<ServiceTypeViewModel>();

			Service.LoadAllServicesCompleted += LoadAllServicesCompleted;

			Service.LoadAllServicesAsync();
		}

		private void LoadAllServicesCompleted(object sender, LoadAllServicesCompletedEventArgs loadAllServicesCompletedEventArgs)
		{
			Service.LoadAllServicesCompleted -= LoadAllServicesCompleted;

			if (HandleException(this, loadAllServicesCompletedEventArgs))
			{
				foreach (var serviceType in loadAllServicesCompletedEventArgs.Result.Where(t => t.IsSupported()))
				{
					ServiceTypes.Add(new ServiceTypeViewModel(serviceType));
				}
			}

			Workers--;
		}

		public ObservableCollection<ServiceTypeViewModel> ServiceTypes { get; }

		public ServiceTypeViewModel SelectedServiceType
		{
			get { return _selectedServiceType; }
			set
			{
				if (Equals(_selectedServiceType, value)) return;
				_selectedServiceType = value;
				OnPropertyChanged();


				PushView(this, new AddService.AddServicePage(new AddServiceViewModel(SelectedServiceType)));
			}
		}
	}
}
