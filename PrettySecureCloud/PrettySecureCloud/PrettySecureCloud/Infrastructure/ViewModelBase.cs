using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public const string NavigationPushView = "Naviagion.PushView";

		protected static void DisplayAlert<T>(T instance, MessageData message) where T : class
		{
			MessagingCenter.Send(instance, MessageData.DisplayAlert, message);
		}

		protected static void PushView<T>(T instance, Page page) where T : class
		{
			MessagingCenter.Send(instance, NavigationPushView, page);
		}


		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
