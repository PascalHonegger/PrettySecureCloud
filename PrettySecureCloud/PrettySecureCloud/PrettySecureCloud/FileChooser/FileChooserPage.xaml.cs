using System;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.FileChooser
{
	public partial class FileChooserPage
	{
		private FileChooserViewModel _viewModel;

		public FileChooserPage(ICloudService selectedCloudService)
		{
			InitializeComponent();

			BindingContext = _viewModel =  new FileChooserViewModel(selectedCloudService);
		}


		/// <inheritdoc />
		protected override void OnAppearing()
		{
			this.Subscribe<FileChooserViewModel, FileChooserPage>();
			base.OnAppearing();
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			this.Unsubscribe<FileChooserViewModel, FileChooserPage>();
			base.OnDisappearing();
		}

		private async void Button_OnClicked(object sender, EventArgs e)
		{
			Func<Task<MediaFile>> func;

			const string gallery = "Fotogalerie";
			const string takePicture = "Foto aufnehmen";

			var action = await DisplayActionSheet("Quelle auswählen", "Abbrechen", null, gallery, takePicture);

			switch (action)
			{
				case gallery:
					func = async () => await CrossMedia.Current.PickPhotoAsync();
					break;
				case takePicture:
					var options = new StoreCameraMediaOptions
					{
						Directory = "LocalData",
						Name = "bild_" + DateTime.Now + ".jpg"
					};
					func = async () => await CrossMedia.Current.TakePhotoAsync(options); ;
					break;
				default:
					return;
			}

			await _viewModel.UploadFileAsync(func);
		}
	}
}
