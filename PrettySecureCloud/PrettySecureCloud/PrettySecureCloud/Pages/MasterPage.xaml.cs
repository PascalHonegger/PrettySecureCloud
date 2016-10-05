using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public partial class MasterPage
	{
		public MasterPage()
		{
			InitializeComponent();

			MessagingCenter.Subscribe<NavPageViewModel, Page>(this, ViewModelBase.NavigationPushView,
				(sender, page) =>
				{
					Detail = page;
					IsPresented = false;
				});

			Master = new NavPage();
		}
	}
}