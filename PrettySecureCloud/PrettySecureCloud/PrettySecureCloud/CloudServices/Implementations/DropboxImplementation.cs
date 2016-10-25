using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using PrettySecureCloud.LoginService;
using Xamarin.Auth;

namespace PrettySecureCloud.CloudServices.Implementations
{
	public class DropboxImplementation : ICloudService
	{
		ServiceType _type;
		public DropboxImplementation(ServiceType type)
		{
			_type = type;
		}

		public ServiceType CloudServiceType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string CustomName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IEnumerable<IFile> FileStructure
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		static async Task Run()
		{
			using (var dbx = new DropboxClient("YOUR ACCESS TOKEN"))
			{
				var full = await dbx.Users.GetCurrentAccountAsync();
				System.Diagnostics.Debug.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
			}
		}

		public string AuthenticateLoginToken()
		{
			var auth = new OAuth2Authenticator(
				clientId: _type.Key,
				scope: _type.Secret,
		  		authorizeUrl: new Uri("https://www.dropbox.com/1/oauth2/authorize"),
				redirectUrl: new Uri(""));

			auth.Completed += (sender, eventArgs) =>
			{
				if (eventArgs.IsAuthenticated)
				{

				}
			};

			//PushView(auth.GetUI(), true, null);
		}

		public StreamReader DownloadFile(IFile target)
		{
			throw new NotImplementedException();
		}

		public void UploadFile(StreamReader source, IFile target)
		{
			throw new NotImplementedException();
		}
	}
}
