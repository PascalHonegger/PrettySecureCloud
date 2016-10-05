using System.ComponentModel;
using System.Runtime.CompilerServices;
using PrettySecureCloud.Service_References.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public const string NavigationPushView = "Naviagion.PushView";
		public const string NavigationPushViewModal = "Naviagion.PushViewModal";

		protected static LoginServiceClient Service
			=> _service ??
			   (_service = new LoginServiceClient(LoginServiceClient.EndpointConfiguration.BasicHttpsBinding_ILoginService));

		protected static Session CurrentSession => _session ?? (_session = new Session());

		private int _workers;
		private static LoginServiceClient _service;
		private static Session _session;

		protected static void DisplayAlert<T>(T instance, MessageData message) where T : class
		{
			MessagingCenter.Send(instance, MessageData.DisplayAlert, message);
		}

		protected static void PushView<T>(T instance, Page page) where T : class
		{
			MessagingCenter.Send(instance, NavigationPushView, page);
		}

		protected static void PushViewModal<T>(T instance, Page page) where T : class
		{
			MessagingCenter.Send(instance, NavigationPushViewModal, page);
		}

		public bool IsFree => !IsBusy;

		public bool IsBusy => Workers > 0;

		public int Workers
		{
			get { return _workers; }
			set
			{
				if (Equals(_workers, value)) return;
				_workers = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsBusy));
				OnPropertyChanged(nameof(IsFree));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}