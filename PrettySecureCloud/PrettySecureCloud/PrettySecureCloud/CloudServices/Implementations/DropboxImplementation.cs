using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using PrettySecureCloud.LoginService;
using Xamarin.Forms;
using Xamarin.Forms.OAuth;

namespace PrettySecureCloud.CloudServices.Implementations
{
	public class DropboxImplementation : ICloudService
	{
		public DropboxImplementation(ServiceType type)
		{
			Model.Type = type;
		}

		public ServiceTypeViewModel CloudServiceType => new ServiceTypeViewModel(Model.Type);
		public CloudService Model { get; set; } = new CloudService();

		public string CustomName
		{
			get { return Model.Name; }
			set { Model.Name = value; }
		}

		public IEnumerable<IFile> FileStructure
		{
			get { throw new NotImplementedException(); }
		}

		static async Task Run()
		{
			using (var dbx = new DropboxClient("YOUR ACCESS TOKEN"))
			{
				var full = await dbx.Users.GetCurrentAccountAsync();
				System.Diagnostics.Debug.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
			}
		}

		public async Task<string> AuthenticateLoginTokenAsync()
		{
			var previousMainWindow = Application.Current.MainPage;

			var authenticationResult =
				await OAuthAuthenticator.Authenticate(OAuthProviders.Dropbox(CloudServiceType.Type.Key, CloudServiceType.Type.Secret,
					// ReSharper disable once RedundantExplicitParamsArrayCreation
					"https://www.dropbox.com/", new string[0]));

			if (authenticationResult.Success)
			{
				return authenticationResult.Account.AccessToken.Token;
			}


			Application.Current.MainPage = previousMainWindow;
			throw new AuthException();
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