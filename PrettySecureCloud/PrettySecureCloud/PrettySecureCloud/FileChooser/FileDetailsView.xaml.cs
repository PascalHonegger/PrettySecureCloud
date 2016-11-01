using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	public partial class FileDetailsView : ContentPage
	{
		private FileDetailsViewModel ViewModel;
		public FileDetailsView(IFile selectedFile, ICloudService cloudService)
		{
			InitializeComponent();

			ViewModel = new FileDetailsViewModel(selectedFile, cloudService);
			BindingContext = ViewModel;
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

		private async void Button_OnClicked(object sender, EventArgs e)
		{
			await ViewModel.DownloadFile();
		}
	}
}
