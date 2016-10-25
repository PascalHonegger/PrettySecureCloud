using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices
{
	public partial class SelectServiceTypePage
	{
		public SelectServiceTypePage()
		{
			InitializeComponent();

			BindingContext = new SelectServiceTypeViewModel();
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
