using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	public class DirectoryElement : IFile
	{
		public DirectoryElement(string fileName, string fileType, string path)
		{
			FileName = fileName;
			FileType = fileType;
			Path = path;

			//Beispiel:
			//	.aes => _aes.png
			//	.png => _png.png
			Image = System.IO.Path.GetExtension(path).Replace('.', '_') + ".png";
		}

		public string FileName { get; }
		public string FileType { get; }
		public string Path { get; }
		public string Image { get; }
		//public ImageSource Image { get; set; }
	}
}
