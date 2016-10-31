using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices.AddService;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.CloudServices
{
	public interface ICloudService
	{
		/// <summary>
		/// The user customisable name
		/// </summary>
		string CustomName { get; set; }

		/// <summary>
		/// The ServiceType
		/// </summary>
		ServiceTypeViewModel CloudServiceType { get; }

		/// <summary>
		/// The original data / model behind the service
		/// </summary>
		CloudService Model { get; set; }

		/// <summary>
		/// Loads the root file structure to be presented by the <see cref="FileChooserPage"/>
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<IFile>> FileStructure();

		/// <summary>
		/// Tries to authenticate the user to this service. Returns the Access / Login token
		/// </summary>
		/// <returns></returns>
		Task<string> AuthenticateLoginTokenAsync();

		/// <summary>
		/// Upload a file to the remote cloud service
		/// </summary>
		/// <param name="source">The source to be uploaded</param>
		/// <param name="target">Where the uploaded file should end up</param>
		/// <returns></returns>
		Task UploadFile(Stream source, IFile target);

		/// <summary>
		/// Download a file from the remote cloud service
		/// </summary>
		/// <param name="target">The file to download</param>
		/// <returns><see cref="Stream"/> with the file content</returns>
		Task<Stream> DownloadFile(IFile target);
	}
}
