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