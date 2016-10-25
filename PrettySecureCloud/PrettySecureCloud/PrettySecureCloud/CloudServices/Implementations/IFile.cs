namespace PrettySecureCloud.CloudServices.Implementations
{
	public interface IFile
	{
		string FileName { get; }

		string FileType { get; }

		string Path { get; }
	}
}