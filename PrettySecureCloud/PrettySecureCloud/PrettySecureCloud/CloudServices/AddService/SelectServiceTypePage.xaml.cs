using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices.AddService
{
	/// <summary>
	/// The Page where all Services are Listed
	/// </summary>
	public partial class SelectServiceTypePage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SelectServiceTypePage" /> class.
		/// </summary>
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
