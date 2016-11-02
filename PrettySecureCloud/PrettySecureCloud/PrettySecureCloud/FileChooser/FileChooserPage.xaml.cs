using System;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
					func = PickPhotoAsync;
					break;
				case takePicture:
					func = TakePhotoAsync;
					break;
				default:
					return;
			}

			await _viewModel.UploadFileAsync(func);
		}

		private async Task<MediaFile> PickPhotoAsync()
		{
			await TryGetPermissions(Permission.Storage);

			return await CrossMedia.Current.PickPhotoAsync();
		}

		private async Task<MediaFile> TakePhotoAsync()
		{
			await TryGetPermissions(Permission.Camera);

			var options = new StoreCameraMediaOptions
			{
				Directory = "LocalData",
				Name = "bild_" + DateTime.Now + ".jpg"
			};

			return await CrossMedia.Current.TakePhotoAsync(options);
		}

		private async Task TryGetPermissions(Permission permission)
		{
			var permissions = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
			if (permissions != PermissionStatus.Granted)
			{
				//Try get permissions
				var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
				permissions = results[permission];
			}

			//Still no permissions
			if (permissions != PermissionStatus.Granted)
			{
				CrossPermissions.Current.OpenAppSettings();
				var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
				permissions = results[permission];
				if (permissions != PermissionStatus.Granted)
				{
					throw new Exception("Konnte kein Bild laden. Überprüfe die App-Berechtigungen");
				}
			}
		}
	}
}
