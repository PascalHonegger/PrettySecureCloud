using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Dropbox.Api.Files;
using PrettySecureCloud.CloudServices.Files;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.ServiceChooser
{
	public class ServiceChooserViewModel : ViewModelBase
	{
		public ServiceChooserViewModel()
		{
			ApplyFilter();

			AddCommand = new Command(Add);
		}

		private void Add()
		{
			PushView(this, new AddService.SelectServiceTypePage());
		}

		public Command AddCommand { get; }

		private string _searchText = string.Empty;
		private ICloudService _selectedCloudService;
		public ObservableCollection<ICloudService> CloudServices { get; } = new ObservableCollection<ICloudService>();

		public ICloudService SelectedCloudService
		{
			get { return _selectedCloudService; }
			set
			{
				if (Equals(_selectedCloudService, value)) return;
				_selectedCloudService = value;
				PushView(this, new FileChooserPage(_selectedCloudService));
			}
		}

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
					.Where(s => s.Name.ToLower().Contains(_searchText.ToLower()))
					.Select(s => s.ToICloudService()))
			{
				CloudServices.Add(service);
			}
		}

		public void DeleteService(ICloudService clickedService)
		{
			Workers++;

			Service.RemoveServiceCompleted += RemoveServiceCompleted;
			Service.RemoveServiceAsync(clickedService.Model.Id, clickedService);
		}

		private void RemoveServiceCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
		{
			if(HandleException(this, asyncCompletedEventArgs))
			{
				var clickedService = (ICloudService) asyncCompletedEventArgs.UserState;

				CurrentSession.CurrentUser.Services.Remove(clickedService.Model);
				CloudServices.Remove(clickedService);
			}
		}

		public void EditService(ICloudService clickedService)
		{
			//TODO Re-Use AddServicePage
			throw new System.NotImplementedException();
		}
	}
}