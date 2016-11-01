using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PrettySecureCloud.CloudServices.EditService;
using PrettySecureCloud.FileChooser;
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
		public ObservableCollection<ICloudService> CloudServices { get; } = new ObservableCollection<ICloudService>();

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
			Service.RemoveServiceCompleted -= RemoveServiceCompleted;

			if(HandleException(this, asyncCompletedEventArgs))
			{
				var clickedService = (ICloudService) asyncCompletedEventArgs.UserState;

				CurrentSession.CurrentUser.Services.Remove(clickedService.Model);
				CloudServices.Remove(clickedService);
			}

			Workers--;
		}

		public void EditService(ICloudService clickedService)
		{
			PushView(this, new EditServicePage(clickedService));
		}
	}
}