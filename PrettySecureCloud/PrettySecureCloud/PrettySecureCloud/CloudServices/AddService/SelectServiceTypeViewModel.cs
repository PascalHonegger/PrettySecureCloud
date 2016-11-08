using System.Collections.ObjectModel;
using System.Linq;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.CloudServices.AddService
{
	/// <summary>
	/// Class for selectig the Service Type
	/// </summary>
	public class SelectServiceTypeViewModel : ViewModelBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SelectServiceTypeViewModel" /> class.
		/// </summary>
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

		/// <summary>
		/// List of Service Types
		/// </summary>
		public ObservableCollection<ServiceTypeViewModel> ServiceTypes { get; }

		/// <summary>
		/// The selected service Type
		/// </summary>
		public ServiceTypeViewModel SelectedServiceType
		{
			get { return null; }
			set
			{
				OnPropertyChanged();

				PushView(this, new AddServicePage(new AddServiceViewModel(value)));
			}
		}
	}
}
