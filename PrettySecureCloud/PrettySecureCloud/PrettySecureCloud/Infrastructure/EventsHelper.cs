using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	/// <summary>
	///     A helper class for ViewModel-events
	/// </summary>
	public static class EventsHelper
	{
		/// <summary>
		///     Subscribes all events
		/// </summary>
		/// <typeparam name="TViewModel">The type of the viewmodel</typeparam>
		/// <typeparam name="TView">The type of the view</typeparam>
		/// <param name="instance">The instance of the viewmodel</param>
		public static void Subscribe<TViewModel, TView>(this TView instance) where TView : Page where TViewModel : class
		{
			MessagingCenter.Subscribe<TViewModel, MessageData>(instance, MessageData.DisplayAlert,
				(sender, message) =>
				{
					Device.BeginInvokeOnMainThread(async () =>
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
				});

			MessagingCenter.Subscribe<TViewModel, Page>(instance, ViewModelBase.NavigationPushView,
				(sender, page) =>
				{
					Device.BeginInvokeOnMainThread(async () =>
					{
						await instance.Navigation.PushAsync(page);
					});
				});
		}

		/// <summary>
		///     Unsubscribes all events
		/// </summary>
		/// <typeparam name="TViewModel">The type of the viewmodel</typeparam>
		/// <typeparam name="TView">The type of the view</typeparam>
		/// <param name="instance">The instance of the viewmodel</param>
		public static void Unsubscribe<TViewModel, TView>(this TView instance) where TView : Page where TViewModel : class
		{
			MessagingCenter.Unsubscribe<TViewModel, MessageData>(instance, MessageData.DisplayAlert);
			MessagingCenter.Unsubscribe<TViewModel, Page>(instance, ViewModelBase.NavigationPushView);
		}
	}
}