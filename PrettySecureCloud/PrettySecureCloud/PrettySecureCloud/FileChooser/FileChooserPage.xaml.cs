using System;
using PrettySecureCloud.CloudServices.ServiceChooser;
using PrettySecureCloud.Infrastructure;
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


		/// <inheritdoc />
		protected override void OnAppearing()
		{
			this.Subscribe<FileChooserViewModel, FileChooserPage>();
			base.OnAppearing();
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			this.Unsubscribe<FileChooserViewModel, FileChooserPage>();
			base.OnDisappearing();
		}
	}
}
