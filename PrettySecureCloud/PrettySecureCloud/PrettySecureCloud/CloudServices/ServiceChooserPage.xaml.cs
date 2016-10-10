using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices
{
	public partial class ServiceChooserPage
	{
		public ServiceChooserPage()
		{
			InitializeComponent();

			BindingContext = new ServiceChooserViewModel();
		}

		protected override void OnAppearing()
		{
			this.Subscribe<ServiceChooserViewModel, ServiceChooserPage>();
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			this.Unsubscribe<ServiceChooserViewModel, ServiceChooserPage>();
			base.OnDisappearing();
		}
	}
}