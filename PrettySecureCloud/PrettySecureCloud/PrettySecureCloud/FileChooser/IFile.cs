namespace PrettySecureCloud.CloudServices.Files
{
	public interface IFile
	{
		string FileName { get; }

		string FileType { get; }

		string Path { get; }

		string Image { get; }
	}
}