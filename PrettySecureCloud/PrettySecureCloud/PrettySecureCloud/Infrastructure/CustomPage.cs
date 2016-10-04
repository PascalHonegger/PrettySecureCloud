using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	public static class EventsHelper
	{
		public static void Subscribe<TViewModel, TView>(this TView instance) where TView : Page where TViewModel : class
		{
			MessagingCenter.Subscribe<TViewModel, MessageData>(instance, MessageData.DisplayAlert, (sender, message) =>
			{
				if (string.IsNullOrEmpty(message.Accept))
					instance.DisplayAlert(message.Title, message.Content, message.Cancel);
				else
					instance.DisplayAlert(message.Title, message.Content, message.Accept, message.Cancel);
			});

			MessagingCenter.Subscribe<TViewModel, Page>(instance, ViewModelBase.NavigationPushView,
				(sender, page) => { instance.Navigation.PushModalAsync(page); });
		}

		public static void Unsubscribe<TViewModel, TView>(this TView instance) where TView : Page where TViewModel : class
		{
			MessagingCenter.Unsubscribe<TViewModel, MessageData>(instance, MessageData.DisplayAlert);
			MessagingCenter.Unsubscribe<TViewModel, Page>(instance, ViewModelBase.NavigationPushView);
		}
	}
}
