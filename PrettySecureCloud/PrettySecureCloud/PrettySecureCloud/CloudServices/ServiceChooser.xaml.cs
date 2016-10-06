using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices
{
	public partial class ServiceChooser
	{
		public ServiceChooser()
		{
			InitializeComponent();

			BindingContext = new ServiceChooserViewModel();

			this.Subscribe<ServiceChooserViewModel, ServiceChooser>();
		}
	}
}