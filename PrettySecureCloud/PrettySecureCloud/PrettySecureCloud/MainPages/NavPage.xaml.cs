namespace PrettySecureCloud.MainPages
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