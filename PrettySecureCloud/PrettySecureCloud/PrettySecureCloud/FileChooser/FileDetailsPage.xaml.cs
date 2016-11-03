using PrettySecureCloud.CloudServices;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	public partial class FileDetailsPage
	{
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
