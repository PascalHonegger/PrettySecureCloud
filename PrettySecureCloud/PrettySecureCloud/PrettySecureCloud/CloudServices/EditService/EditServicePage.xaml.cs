using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices.EditService
{
	/// <summary>
	/// Class for editing the ServicePage
	/// </summary>
	public partial class EditServicePage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EditServicePage" /> class.
		/// </summary>
		/// <param name="clickedService"></param>
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
