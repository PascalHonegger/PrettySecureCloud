using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using PrettySecureCloud.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public const string NavigationPushView = "Naviagion.PushView";
		public const string NavigationPushViewModal = "Naviagion.PushViewModal";

		/// <summary>
		///     Handles the exception if possible, else throws it
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="args"></param>
		/// <returns>True if there was no exception, true if there was one</returns>
		protected static bool HandleException<TViewModel>(TViewModel instance, AsyncCompletedEventArgs args) where TViewModel : class
		{
			if (args.Error == null) return true;

			if (args.Error is FaultException)
			{
				DisplayAlert(instance, new MessageData("Fehler", args.Error.Message, "Ok"));
			}
			else if (args.Error is CommunicationException)
			{
				DisplayAlert(instance, new MessageData("Keine Verbindung", "Konnte keine Verbindung zum Server herstellen", "Ok"));
			}
			else
			{
				throw args.Error;
			}

			return false;
		}

		public static LoginServiceClient Service { get; set; }

		protected static Session CurrentSession => Session.Instance;

		private int _workers;

		protected static void DisplayAlert<TViewModel>(TViewModel instance, MessageData message) where TViewModel : class
		{
			MessagingCenter.Send(instance, MessageData.DisplayAlert, message);
		}

		protected static void PushView<TViewModel>(TViewModel instance, Page page) where TViewModel : class
		{
			MessagingCenter.Send(instance, NavigationPushView, page);
		}

		protected static void PushViewModal<TViewModel>(TViewModel instance, Page page) where TViewModel : class
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