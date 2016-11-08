using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PrettySecureCloud.CloudServices.EditService;
using PrettySecureCloud.FileChooser;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.ServiceChooser
{
	/// <summary>
	/// ViewModel to choose a Service
	/// </summary>
	public class ServiceChooserViewModel : ViewModelBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceChooserViewModel" /> class.
		/// </summary>
		public ServiceChooserViewModel()
		{
			ApplyFilter();

			AddCommand = new Command(Add);
		}

		private void Add()
		{
			PushView(this, new AddService.SelectServiceTypePage());
		}

		/// <summary>
		/// Command for adding a CloudService
		/// </summary>
		public Command AddCommand { get; }

		private string _searchText = string.Empty;
		/// <summary>
		/// List of CloudServices
		/// </summary>
		public ObservableCollection<ICloudService> CloudServices { get; } = new ObservableCollection<ICloudService>();

		/// <summary>
		/// The selected CloudService
		/// </summary>
		public ICloudService SelectedCloudService
		{
			get { return null; }
			set
			{
				if (value == null) return;
				OnPropertyChanged();
				PushView(this, new FileChooserPage(value));
			}
		}

		/// <summary>
		/// The text to search after
		/// </summary>
		public string SearchText
		{
			get { return _searchText; }
			set
			{
				if (Equals(_searchText, value)) return;
				_searchText = value;
				OnPropertyChanged();

				ApplyFilter();
			}
		}

		private void ApplyFilter()
		{
			CloudServices.Clear();

			foreach (
				var service in
				CurrentSession.CurrentUser.Services.Where(s => s.Type.IsSupported())
					.Where(s => s.Name.ToLowerInvariant().Contains(_searchText.ToLowerInvariant()))
					.Select(s => s.ToICloudService()))
			{
				CloudServices.Add(service);
			}
		}

		/// <summary>
		/// Deletes a Service
		/// </summary>
		/// <param name="clickedService"></param>
		public void DeleteService(ICloudService clickedService)
		{
			Workers++;

			Service.RemoveServiceCompleted += RemoveServiceCompleted;
			Service.RemoveServiceAsync(clickedService.Model.Id, clickedService);
		}

		private void RemoveServiceCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
		{
			Service.RemoveServiceCompleted -= RemoveServiceCompleted;

			if(HandleException(this, asyncCompletedEventArgs))
			{
				var clickedService = (ICloudService) asyncCompletedEventArgs.UserState;

				CurrentSession.CurrentUser.Services.Remove(clickedService.Model);
				CloudServices.Remove(clickedService);
			}

			Workers--;
		}

		/// <summary>
		/// Edits the Service
		/// </summary>
		/// <param name="clickedService"></param>
		public void EditService(ICloudService clickedService)
		{
			PushView(this, new EditServicePage(clickedService));
		}
	}
}