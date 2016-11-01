using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.Infrastructure;

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
			var File = await  CloudService.DownloadFile(SelectedFile);
			
		}
	}
}
