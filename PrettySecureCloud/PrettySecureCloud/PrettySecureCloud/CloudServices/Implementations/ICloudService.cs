using System.Collections.Generic;
using System.IO;
using PrettySecureCloud.LoginService;

namespace PrettySecureCloud.CloudServices.Implementations
{
	public interface ICloudService
	{
		string CustomName { get; }

		ServiceType CloudServiceType { get; }

		IEnumerable<IFile> FileStructure { get; }

		string AuthenticateLoginToken();

		void UploadFile(StreamReader source, IFile target);

		StreamReader DownloadFile(IFile target);
	}
}
