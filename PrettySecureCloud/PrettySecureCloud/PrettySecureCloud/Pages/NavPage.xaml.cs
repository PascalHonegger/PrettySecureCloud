using System.Collections.Generic;
using PrettySecureCloud.Model;
using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public partial class NavPage
	{
		public NavPage()
		{
			InitializeComponent();

			BindingContext = new NavPageViewModel();
		}
	}
}