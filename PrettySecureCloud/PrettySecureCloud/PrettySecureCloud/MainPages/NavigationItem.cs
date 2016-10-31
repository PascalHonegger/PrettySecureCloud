using Xamarin.Forms;

namespace PrettySecureCloud.MainPages
{
	public class NavigationItem
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }

		public Page Page { get; set; }
	}
}