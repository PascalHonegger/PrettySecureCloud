using PrettySecureCloud.CloudServices;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	public partial class FileDetailsView
	{
		public FileDetailsView(IFile selectedFile, ICloudService cloudService)
		{
			InitializeComponent();

			var viewModel = new FileDetailsViewModel(selectedFile, cloudService);

			BindingContext = viewModel;

			viewModel.DownloadCommand.ChangeCanExecute();
		}

		/// <inheritdoc />
		protected override void OnAppearing()
		{
			this.Subscribe<FileDetailsViewModel, FileDetailsView>();
			base.OnAppearing();
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			this.Unsubscribe<FileDetailsViewModel, FileDetailsView>();
			base.OnDisappearing();
		}
	}
}
