using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public partial class MasterPage
	{
		public MasterPage()
		{
			InitializeComponent();

			Subscribe();
			Master = new NavPage();
			Unsubscribe();
		}

		/// <inheritdoc />
		protected override void OnAppearing()
		{
			Subscribe();
			base.OnAppearing();
		}

		private void Subscribe()
		{
			MessagingCenter.Subscribe<NavPageViewModel, Page>(this, ViewModelBase.NavigationPushView,
				(sender, page) =>
				{
					Detail = page;
					IsPresented = false;
				});
		}

		private void Unsubscribe()
		{
			MessagingCenter.Unsubscribe<NavPageViewModel, Page>(this, ViewModelBase.NavigationPushView);
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			Unsubscribe();
			base.OnDisappearing();
		}
	}
}
 