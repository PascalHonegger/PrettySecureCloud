using System.Collections.ObjectModel;
using PrettySecureCloud.FileChooser;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.Files
{
	internal class FileChooserViewModel : ViewModelBase
	{
		private readonly ICloudService CloudService;
		private IFile _selectedFile;
		public ObservableCollection<IFile> FilledListView { get; } = new ObservableCollection<IFile>();

		public IFile SelectedFile
		{
			get { return _selectedFile; }
			set
			{
				_selectedFile = value;
				OnPropertyChanged();

				PushView(this, new FileDetailsView(_selectedFile));
			}
		}

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