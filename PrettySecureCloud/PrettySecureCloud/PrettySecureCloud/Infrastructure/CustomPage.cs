using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	public class CustomPage : ContentPage
	{
		protected void Subscribe<T>() where T : class
		{
			MessagingCenter.Subscribe<T, MessageData>(this, MessageData.DisplayAlert, (sender, message) =>
			{
				if (string.IsNullOrEmpty(message.Accept))
					DisplayAlert(message.Title, message.Content, message.Cancel);
				else
					DisplayAlert(message.Title, message.Content, message.Accept, message.Cancel);
			});

			MessagingCenter.Subscribe<T, Page>(this, ViewModelBase.NavigationPushView,
				(sender, page) => { Navigation.PushModalAsync(page); });
		}

		protected void Unsubscribe<T>() where T : class
		{
			MessagingCenter.Unsubscribe<T, MessageData>(this, MessageData.DisplayAlert);
			MessagingCenter.Unsubscribe<T, Page>(this, ViewModelBase.NavigationPushView);
		}
	}
}
