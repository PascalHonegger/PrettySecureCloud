using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using PrettySecureCloud.LoginService;
using Xamarin.Forms;

namespace PrettySecureCloud.Infrastructure
{
	/// <summary>
	///     Base-class for all main viewmodels
	/// </summary>
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		/// <summary>
		///     Constant for the PushView request
		/// </summary>
		public const string NavigationPushView = "Naviagion.PushView";

		/// <summary>
		///     Handles the exception if possible, else throws it
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="args"></param>
		/// <returns>True if there was no exception, false if there was one</returns>
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

		/// <summary>
		///     The LoginService-Instance. Used for all Server-Calls
		/// </summary>
		public static LoginServiceClient Service { get; set; }

		/// <summary>
		///     The current session. Basically <see cref="Session.Instance"/>
		/// </summary>
		protected static Session CurrentSession => Session.Instance;

		private int _workers;

		/// <summary>
		///     Tells the current view to display an alert (pop-up)
		/// </summary>
		/// <typeparam name="TViewModel">The type of the viewmodel</typeparam>
		/// <param name="instance">The instance of the viewmodel</param>
		/// <param name="message">The message to display</param>
		protected static void DisplayAlert<TViewModel>(TViewModel instance, MessageData message) where TViewModel : class
		{
			MessagingCenter.Send(instance, MessageData.DisplayAlert, message);
		}

		/// <summary>
		///     Tells the current view to navigate to the provided page
		/// </summary>
		/// <typeparam name="TViewModel">The type of the viewmodel</typeparam>
		/// <param name="instance">The instance of the viewmodel</param>
		/// <param name="page">The page to navigate to</param>
		protected static void PushView<TViewModel>(TViewModel instance, Page page) where TViewModel : class
		{
			MessagingCenter.Send(instance, NavigationPushView, page);
		}

		/// <summary>
		///     Changes the main page of the application
		/// </summary>
		/// <typeparam name="TViewModel">The type of the viewmodel</typeparam>
		/// <param name="instance">The instance of the viewmodel</param>
		/// <param name="page">The page to display</param>
		protected static void PushViewModal<TViewModel>(TViewModel instance, Page page) where TViewModel : class
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				Application.Current.MainPage = page;
			});
		}

		/// <summary>
		///     Determines wheter this ViewModel is free or not
		/// </summary>
		public bool IsFree => !IsBusy;

		/// <summary>
		///     Determines wheter this ViewModel is working or not
		/// </summary>
		public bool IsBusy => Workers > 0;

		protected int Workers
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

		/// <summary>
		///     Notifies the GUI that a property has changed
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     Event invocator for <see cref="PropertyChanged"/>
		/// </summary>
		/// <param name="propertyName"></param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}