using System;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.Files
{
	public partial class FileChooserPage : ContentPage
	{
		public FileChooserPage()
		{
			InitializeComponent();

			BindingContext = new FileChooserViewModel();
		}

		private void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
