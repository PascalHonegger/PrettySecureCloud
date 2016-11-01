using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices.AddService
{
	public partial class SelectServiceTypePage
	{
		public SelectServiceTypePage()
		{
			InitializeComponent();

			BindingContext = new SelectServiceTypeViewModel();
			ListViewServiceType.Footer = string.Empty;
		}

		protected override void OnAppearing()
		{
			this.Subscribe<SelectServiceTypeViewModel, SelectServiceTypePage>();
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			this.Unsubscribe<SelectServiceTypeViewModel, SelectServiceTypePage>();
			base.OnDisappearing();
		}
	}
}
