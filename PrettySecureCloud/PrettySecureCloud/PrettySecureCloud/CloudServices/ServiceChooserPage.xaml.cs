using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices
{
	public partial class ServiceChooserPage
	{
		public ServiceChooserPage()
		{
			InitializeComponent();

			BindingContext = new ServiceChooserViewModel();

			this.Subscribe<ServiceChooserViewModel, ServiceChooserPage>();
		}
	}
}