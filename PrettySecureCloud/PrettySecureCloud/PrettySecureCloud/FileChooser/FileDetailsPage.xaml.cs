using PrettySecureCloud.CloudServices;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	/// <summary>
	/// Displays the details of a file
	/// </summary>
	public partial class FileDetailsPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FileDetailsPage" /> class.
		/// </summary>
		/// <param name="selectedFile"></param>
		/// <param name="cloudService"></param>
		public FileDetailsPage(IFile selectedFile, ICloudService cloudService)
		{
			InitializeComponent();

			var viewModel = new FileDetailsViewModel(selectedFile, cloudService);

			BindingContext = viewModel;

			viewModel.DownloadCommand.ChangeCanExecute();
		}

		/// <inheritdoc />
		protected override void OnAppearing()
		{
			this.Subscribe<FileDetailsViewModel, FileDetailsPage>();
			base.OnAppearing();
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			this.Unsubscribe<FileDetailsViewModel, FileDetailsPage>();
			base.OnDisappearing();
		}
	}
}
