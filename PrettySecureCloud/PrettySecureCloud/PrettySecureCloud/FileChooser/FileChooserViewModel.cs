using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using PrettySecureCloud.FileChooser;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.Files
{
	public class FileChooserViewModel : ViewModelBase
	{
		private const string FileExtension = ".aes";

		private readonly ICloudService _cloudService;
		private IFile _selectedFile;
		public ObservableCollection<IFile> FilledListView { get; } = new ObservableCollection<IFile>();

		public IFile SelectedFile
		{
			get { return _selectedFile; }
			set
			{
				_selectedFile = value;
				OnPropertyChanged();

				PushView(this, new FileDetailsView(_selectedFile, _cloudService));
			}
		}

		public Command UploadCommand { get; }

		public FileChooserViewModel(ICloudService cloudService)
		{
			_cloudService = cloudService;

			UploadCommand = new Command(async () => await UploadFileAsync());
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

		private async Task UploadFileAsync()
		{
			Workers++;

			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				DisplayAlert(this, new MessageData("Fehler", "Kamera nicht verfügbar.", "OK"));
				return;
			}

			var file = await CrossMedia.Current.PickPhotoAsync();

			//TODO Take photo with camera as option
			/*file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
			{
				Directory = "LocalData",
				Name = "bill_" + DateTime.Now + ".jpg"
			});*/

			if (file == null)
			{
				throw new FileNotFoundException();
			}


			var onlyFileName = Path.GetFileName(file.Path);
			var toBeUploaded = new DirectoryElement
			{
				Path = onlyFileName + FileExtension,
				FileName = onlyFileName,
				FileType = Path.GetExtension(onlyFileName)
			};

			using (var ms = new MemoryStream())
			{
				await file.GetStream().CopyToAsync(ms);

				var encrypted = CurrentSession.Encryptor.Encrypt(ms.ToArray(), CurrentSession.CurrentUser.EncryptionKey);

				await _cloudService.UploadFile(new MemoryStream(encrypted), toBeUploaded);
			}

			Workers--;
		}

		public Command RefreshFilesCommand { get; }
	}
}