using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api;
using PrettySecureCloud.LoginService;
using Xamarin.Auth;
using Xamarin.Forms.OAuth;
using Xamarin.Forms.OAuth.Providers;

namespace PrettySecureCloud.CloudServices.Implementations
{
	public class DropboxImplementation : ICloudService
	{
		public DropboxImplementation(ServiceType type)
		{
			CloudServiceType = type;
		}

		public ServiceType CloudServiceType { get; }

		public string CustomName { get; set; }

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
			var authorizeUrl = DropboxOAuth2Helper.GetAuthorizeUri(CloudServiceType.Key);
			var redirectUrl = new Uri("https://www.dropbox.com/1/oauth2/authorize");

			var auth = new OAuth2Authenticator(
				clientId: CloudServiceType.Key,
				scope: CloudServiceType.Secret,
				authorizeUrl: authorizeUrl,
				redirectUrl: redirectUrl);

			auth.Completed += (sender, eventArgs) =>
			{
				if (eventArgs.IsAuthenticated)
				{
				}
			};


			var authenticationResult =
				await OAuthAuthenticator.Authenticate(OAuthProviders.Dropbox(CloudServiceType.Key, CloudServiceType.Secret,
					"https://www.dropbox.com/", new string[0]));

			if (authenticationResult.Success)
			{
				return authenticationResult.Account.AccessToken.Token;
			}
			else
			{
				//TODO Login failed
				throw new NotImplementedException(authenticationResult.ErrorDescription);
			}
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