using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	public class FileChooserViewModel : ViewModelBase
	{
		public const string FileExtension = ".aes";

		private readonly ICloudService _cloudService;
		public ObservableCollection<IFile> FilledListView { get; } = new ObservableCollection<IFile>();

		public IFile SelectedFile
		{
			get { return null; }
			set
			{
				OnPropertyChanged();

				PushView(this, new FileDetailsPage(value, _cloudService));
			}
		}

		public FileChooserViewModel(ICloudService cloudService)
		{
			_cloudService = cloudService;

			RefreshFilesCommand = new Command(async () => await ShowDirectory());

			RefreshFilesCommand.Execute(null);
		}

		private async Task ShowDirectory()
		{
			Workers++;

			FilledListView.Clear();

			var files = await _cloudService.FileStructure();

			Device.BeginInvokeOnMainThread(() =>
			{
				foreach (var file in files)
				{
					FilledListView.Add(file);
				}

				Workers--;
			});
		}

		public async Task UploadFileAsync(Func<Task<MediaFile>> getFileAction)
		{
			Workers++;

			await CrossMedia.Current.Initialize();

			try
			{
				var file = await getFileAction();

				if (file == null)
				{
					DisplayAlert(this, new MessageData("Fehler", "Kein Bild ausgewählt", "Ok"));
					return;
				}

				var onlyFileName = Path.GetFileName(file.Path);
				var toBeUploaded = new DirectoryElement(onlyFileName, onlyFileName + FileExtension);

				using (var ms = new MemoryStream())
				{
					await file.GetStream().CopyToAsync(ms);

					var encrypted = CurrentSession.Encryptor.Encrypt(ms.ToArray(), CurrentSession.CurrentUser.EncryptionKey);

					await _cloudService.UploadFile(new MemoryStream(encrypted), toBeUploaded);
				}
			}
			catch (Exception e)
			{
				DisplayAlert(this, new MessageData("Fehler", e.Message, "OK"));
			}
			finally
			{
				await ShowDirectory();
				Workers--;
			}
		}

		public Command RefreshFilesCommand { get; }
	}
}