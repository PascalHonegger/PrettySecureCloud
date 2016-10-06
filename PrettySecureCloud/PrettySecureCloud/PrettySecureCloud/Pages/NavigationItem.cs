using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public class NavigationItem
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }

		public Page Page { get; set; }
	}
}