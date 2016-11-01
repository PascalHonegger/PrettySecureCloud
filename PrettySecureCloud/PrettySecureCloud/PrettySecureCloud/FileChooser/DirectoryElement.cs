using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices.Files;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	class DirectoryElement : IFile
	{
		public string FileName { get; set; }
		public string FileType { get; set; }
		public string Path { get; set; }
		public string Image { get; } = "Bla";
		//public ImageSource Image { get; set; }
	}
}
