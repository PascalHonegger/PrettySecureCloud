using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	public static class EventsHelper
	{
		public static void Subscribe<TViewModel, TView>(this TView instance) where TView : Page where TViewModel : class
		{
			MessagingCenter.Subscribe<TViewModel, MessageData>(instance, MessageData.DisplayAlert,
				async (sender, message) =>
				{
					if (string.IsNullOrEmpty(message.Accept))
					{
						await instance.DisplayAlert(message.Title, message.Content, message.Cancel);
					}
					else
					{
						await instance.DisplayAlert(message.Title, message.Content, message.Accept, message.Cancel);
					}
				});

			MessagingCenter.Subscribe<TViewModel, Page>(instance, ViewModelBase.NavigationPushView,
				async (sender, page) => { await instance.Navigation.PushAsync(page); });

			MessagingCenter.Subscribe<TViewModel, Page>(instance, ViewModelBase.NavigationPushViewModal,
				(sender, page) =>
				{
					Application.Current.MainPage = page;
				});
		}

		public static void Unsubscribe<TViewModel, TView>(this TView instance) where TView : Page where TViewModel : class
		{
			MessagingCenter.Unsubscribe<TViewModel, MessageData>(instance, MessageData.DisplayAlert);
			MessagingCenter.Unsubscribe<TViewModel, Page>(instance, ViewModelBase.NavigationPushView);
		}
	}
}