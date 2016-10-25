using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PrettySecureCloud.Pages
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
