using System;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.Files
{
	public partial class FileChooserPage : ContentPage
	{
		public FileChooserPage(ICloudService selectedCloudService)
		{
			InitializeComponent();

			BindingContext = new FileChooserViewModel(selectedCloudService);
		}

		private void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
