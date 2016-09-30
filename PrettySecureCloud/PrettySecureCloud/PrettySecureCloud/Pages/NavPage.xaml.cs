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

			NavListView.ItemsSource = new List<Startscreen>
			{
				new Startscreen
				{
					Title = "Home",
					Image = "home.png"
				}
			};
		}

		public ListView ListView => NavListView;
	}
}