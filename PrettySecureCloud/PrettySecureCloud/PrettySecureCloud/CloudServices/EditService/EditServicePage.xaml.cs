using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices.EditService
{
	public partial class EditServicePage
	{
		public EditServicePage(ICloudService clickedService)
		{
			InitializeComponent();

			BindingContext = new EditServiceViewModel(clickedService);
		}

		/// <inheritdoc />
		protected override void OnAppearing()
		{
			this.Subscribe<EditServiceViewModel, EditServicePage>();
			base.OnAppearing();
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			this.Unsubscribe<EditServiceViewModel, EditServicePage>();
			base.OnDisappearing();
		}
	}
}
