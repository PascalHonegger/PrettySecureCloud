using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.FileChooser
{
	public partial class FileDetailsView : ContentPage
	{
		public FileDetailsView(IFile selectedFile)
		{
			InitializeComponent();

			BindingContext = new FileDetailsViewModel(selectedFile);
		}

		/// <inheritdoc />
		protected override void OnAppearing()
		{
			this.Subscribe<FileDetailsViewModel, FileDetailsView>();
			base.OnAppearing();
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			this.Unsubscribe<FileDetailsViewModel, FileDetailsView>();
			base.OnDisappearing();
		}
	}
}
