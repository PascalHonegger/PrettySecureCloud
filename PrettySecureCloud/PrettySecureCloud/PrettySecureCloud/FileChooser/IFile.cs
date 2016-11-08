namespace PrettySecureCloud.FileChooser
{
	/// <summary>
	/// The inteface for a File
	/// </summary>
	public interface IFile
	{
		/// <summary>
		/// The file name
		/// </summary>
		string FileName { get; }

		/// <summary>
		/// The file type
		/// </summary>
		string FileType { get; }

		/// <summary>
		/// The path of the file
		/// </summary>
		string Path { get; }

		/// <summary>
		/// The path to the preview Image
		/// </summary>
		string Image { get; }
	}
}