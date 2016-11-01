using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.FileChooser
{
	class FileDetailsViewModel : ViewModelBase
	{
		private IFile _selectedFile;

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

		public FileDetailsViewModel(IFile selectedFile)
		{
			SelectedFile = selectedFile;
		}
	}
}
