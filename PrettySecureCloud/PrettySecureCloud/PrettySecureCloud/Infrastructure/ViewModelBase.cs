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
		/// <param name="args"></param>
		/// <returns>True if there was no exception, true if there was one</returns>
		protected bool HandleException<T>(T instance, AsyncCompletedEventArgs args) where T : class
		{
			if (args.Error == null) return true;

			try
			{
				throw args.Error;
			}
			catch (FaultException fault)
			{
				DisplayAlert(instance, new MessageData("Fehler", fault.Message, "Ok"));
			}
			catch (CommunicationException)
			{
				DisplayAlert(instance, new MessageData("Keine Verbindung", "Konnte keine Verbindung zum Server herstellen", "Ok"));
			}

			return false;
		}

		public static LoginServiceClient Service { get; set; }

		protected static Session CurrentSession => _session ?? (_session = new Session());

		private int _workers;
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