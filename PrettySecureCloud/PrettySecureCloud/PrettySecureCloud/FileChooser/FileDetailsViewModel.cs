using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	class FileDetailsViewModel : ViewModelBase
	{
		private IFile _selectedFile;
		private ICloudService CloudService;

		public IFile SelectedFile
		{
			get
			{
				return _selectedFile;
			}
			set
			{
				_selectedFile = value;
				OnPropertyChanged();
			}
		}

		public FileDetailsViewModel(IFile selectedFile, ICloudService cloudService)
		{
			SelectedFile = selectedFile;
			CloudService = cloudService;
		}

		public async Task DownloadFile()
		{
			Workers++;
			var File = await CloudService.DownloadFile(SelectedFile);
			//Image image = new Image();
			//image.Source = ImageSource.FromStream(() => File);
			using (var ms = new MemoryStream())
			{
				File.CopyTo(ms);
				DependencyService.Get<IPicture>().SavePictureToDisk(SelectedFile.FileName, ms.ToArray());
			}

			Workers--;
		}
	}
}
