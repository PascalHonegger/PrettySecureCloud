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
	/// <summary>
	/// Viewmodel for the Fileoverview of the Cloud
	/// </summary>
	public class FileChooserViewModel : ViewModelBase
	{
		/// <summary>
		/// Fileextension for the encrypted file
		/// </summary>
		public const string FileExtension = ".aes";

		private readonly ICloudService _cloudService;
		/// <summary>
		/// List woth all Files of the Cloud
		/// </summary>
		public ObservableCollection<IFile> FilledListView { get; } = new ObservableCollection<IFile>();

		/// <summary>
		/// By user Selected File
		/// </summary>
		public IFile SelectedFile
		{
			get { return null; }
			set
			{
				OnPropertyChanged();

				PushView(this, new FileDetailsPage(value, _cloudService));
			}
		}

		/// <summary>
		/// Constructor for the Viewmodel
		/// </summary>
		/// <param name="cloudService">selected Cloud service</param>
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

		/// <summary>
		/// Upload a File to the Cloud
		/// </summary>
		/// <param name="getFileAction"></param>
		/// <returns></returns>
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

					//Refresh after upload
					await ShowDirectory();
				}
			}
			catch (Exception e)
			{
				DisplayAlert(this, new MessageData("Fehler", e.Message, "OK"));
			}
			finally
			{
				Workers--;
			}
		}

		/// <summary>
		/// Command for refreshing the listed Files
		/// </summary>
		public Command RefreshFilesCommand { get; }
	}
}