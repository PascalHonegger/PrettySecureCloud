using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using PrettySecureCloud.CloudServices.AddService;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.FileChooser;
using PrettySecureCloud.Service_References.LoginService;
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

		///<inheritdoc cref="ICloudService.CloudServiceType"/>
		public ServiceTypeViewModel CloudServiceType => new ServiceTypeViewModel(Model.Type);


		///<inheritdoc cref="ICloudService.Model"/>
		public CloudService Model { get; set; } = new CloudService();


		///<inheritdoc cref="ICloudService.CustomName"/>
		public string CustomName
		{
			get { return Model.Name; }
			set { Model.Name = value; }
		}

		private DropboxClient DropboxClient
		{
			get
			{
				if (_dropboxClient == null)
				{
					if(Model?.LoginToken  == null) throw new InvalidOperationException();

					_dropboxClient = new DropboxClient(Model.LoginToken);
				}

				return _dropboxClient;
			}
		}

		private DropboxClient _dropboxClient;

		///<inheritdoc cref="ICloudService.FileStructure"/>
		public async Task<IEnumerable<IFile>> FileStructure()
		{
			var folderContent = await DropboxClient.Files.ListFolderAsync(string.Empty);

			var files = folderContent.Entries.Where(e => e.IsFile).Select(f => f.AsFile);

			var Directory = new List<IFile>();
			foreach (var file in files)
			{
				var element = new DirectoryElement()
				{
					FileName = file.Name,
					FileType = file.ParentSharedFolderId,
					Path = file.PathDisplay,
				};
				Directory.Add(element);
			}
			return Directory;
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

		///<inheritdoc cref="ICloudService.UploadFile"/>
		public async Task UploadFile(Stream source, IFile target)
		{
			await DropboxClient.Files.UploadAsync(new CommitInfo(target.Path, WriteMode.Add.Instance), source);
		}

		///<inheritdoc cref="ICloudService.DownloadFile"/>
		public async Task<Stream> DownloadFile(IFile target)
		{
			var result = await DropboxClient.Files.DownloadAsync(target.Path);

			return await result.GetContentAsStreamAsync();
		}
	}
}