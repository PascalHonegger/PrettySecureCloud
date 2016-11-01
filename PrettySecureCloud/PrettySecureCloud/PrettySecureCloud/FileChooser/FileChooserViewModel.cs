using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.Files
{
	internal class FileChooserViewModel
	{
		private readonly ICloudService CloudService;
		public ObservableCollection<IFile> FilledListView { get; } = new ObservableCollection<IFile>();

		public FileChooserViewModel(ICloudService cloudService)
		{
			CloudService = cloudService;


			ShowDirectory();
		}

		private async void ShowDirectory()
		{
			var files = await CloudService.FileStructure();

			Device.BeginInvokeOnMainThread(() =>
			{
				foreach (var file in files)
					FilledListView.Add(file);
			});
		}
	}
}