using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	/// <summary>
	/// The Implementation of IFile
	/// </summary>
	public class DirectoryElement : IFile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DirectoryElement" /> class.
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="path"></param>
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

		/// <inheritdoc cref="IFile.FileName"/>
		public string FileName { get; }

		/// <inheritdoc cref="IFile.FileType"/>
		public string FileType { get; }

		/// <inheritdoc cref="IFile.Path"/>
		public string Path { get; }

		/// <inheritdoc cref="IFile.Image"/>
		public string Image { get; }
	}
}
