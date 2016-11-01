using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	public class FileDetailsViewModel : ViewModelBase
	{
		private IFile _selectedFile;
		private readonly ICloudService _cloudService;

		public IFile SelectedFile
		{
			get
			{
				return _selectedFile;
			}
			set
			{
				if (Equals(_selectedFile, value)) return;
				_selectedFile = value;
				OnPropertyChanged();
				DownloadCommand.ChangeCanExecute();
			}
		}

		public Command DownloadCommand { get; }

		public FileDetailsViewModel(IFile selectedFile, ICloudService cloudService)
		{
			DownloadCommand = new Command(async () => await DownloadFileAsync(), CanDownloadFile);

			SelectedFile = selectedFile;
			_cloudService = cloudService;
		}

		private bool CanDownloadFile() => SelectedFile != null;

		public async Task DownloadFileAsync()
		{
			Workers++;
			var file = await _cloudService.DownloadFile(SelectedFile);

			using (var ms = new MemoryStream())
			{
				await file.CopyToAsync(ms);

				var decrypted = CurrentSession.Encryptor.Decrypt(ms.ToArray(), CurrentSession.CurrentUser.EncryptionKey);

				DependencyService.Get<IPicture>().SavePictureToDisk(Path.GetFileNameWithoutExtension(SelectedFile.FileName), decrypted);
			}

			Workers--;
		}
	}
}
