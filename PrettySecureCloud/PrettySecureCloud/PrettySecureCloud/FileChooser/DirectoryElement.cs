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
		public DirectoryElement(string fileName, string path)
		{
			FileName = fileName;
			FileType = System.IO.Path.GetExtension(fileName);
			Path = path;

			//Beispiel:
			//	.aes => _aes.png
			//	.png => _png.png
			//TODO Image = System.IO.Path.GetExtension(path).Replace('.', '_') + ".png";

			//Temp
			Image = "fileblue.png";
		}

		public string FileName { get; }
		public string FileType { get; }
		public string Path { get; }
		public string Image { get; }
	}
}
