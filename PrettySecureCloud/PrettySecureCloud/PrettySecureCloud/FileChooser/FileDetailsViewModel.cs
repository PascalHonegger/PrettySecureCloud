using System;
using System.IO;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	/// <summary>
	/// ViewModel for displaying the file details
	/// </summary>
	public class FileDetailsViewModel : ViewModelBase
	{
		private IFile _selectedFile;
		private readonly ICloudService _cloudService;

		/// <summary>
		/// The selected file
		/// </summary>
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

		/// <summary>
		/// Command for <see cref="DownloadFileAsync"/>
		/// </summary>
		public Command DownloadCommand { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDetailsViewModel" /> class.
		/// </summary>
		/// <param name="selectedFile"></param>
		/// <param name="cloudService"></param>
		public FileDetailsViewModel(IFile selectedFile, ICloudService cloudService)
		{
			DownloadCommand = new Command(async () => await DownloadFileAsync(), CanDownloadFile);

			SelectedFile = selectedFile;
			_cloudService = cloudService;
		}

		private bool CanDownloadFile() => SelectedFile != null;

		private async Task DownloadFileAsync()
		{

			try
			{
				Workers++;
				var file = await _cloudService.DownloadFile(SelectedFile);

				using (var ms = new MemoryStream())
				{
					await file.CopyToAsync(ms);

					var data = ms.ToArray();

					string fileName;

					if (Equals(SelectedFile.FileType, FileChooserViewModel.FileExtension))
					{
						fileName = Path.GetFileNameWithoutExtension(SelectedFile.FileName);
						data = CurrentSession.Encryptor.Decrypt(data, CurrentSession.CurrentUser.EncryptionKey);
					}
					else
					{
						fileName = SelectedFile.FileName;
					}

					var wasAbleToSave = DependencyService.Get<IPicture>().SavePictureToDisk(fileName, data);

					if (wasAbleToSave)
					{
						DisplayAlert(this, new MessageData("Gespeichert", "Die Datei wurde erfolgreich heruntergeladen!", "Ok"));
					}
					else
					{
						throw new Exception("Datei konnte nicht gespeichert werden!");
					}
				}
			}
			catch (Exception e)
			{
				DisplayAlert(this, new MessageData("Fehler", e.Message, "Ok"));
			}
			finally
			{
				Workers--;
			}
		}
	}
}
