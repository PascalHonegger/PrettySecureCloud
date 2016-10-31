using System;
using PrettySecureCloud.Infrastructure;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices.ServiceChooser
{
	public partial class ServiceChooserPage
	{
		private readonly ServiceChooserViewModel _viewModel;

		public ServiceChooserPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new ServiceChooserViewModel();
		}

		/// <inheritdoc />
		protected override void OnAppearing()
		{
			this.Subscribe<ServiceChooserViewModel, ServiceChooserPage>();
			base.OnAppearing();
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			this.Unsubscribe<ServiceChooserViewModel, ServiceChooserPage>();
			base.OnDisappearing();
		}

		private async void MenuItem_OnClickedDelete(object sender, EventArgs e)
		{
			var menuItem = (MenuItem)sender;
			var clickedService = (ICloudService)menuItem.CommandParameter;

			var confirm = await DisplayAlert("Wirklich löschen?", $"\"{clickedService.CustomName}\" wirklich löschen?", "Ja", "Nein");

			if (confirm)
			{
				_viewModel.DeleteService(clickedService);
			}
		}

		private void MenuItem_OnClickedEdit(object sender, EventArgs e)
		{
			var menuItem = (MenuItem)sender;
			var clickedService = (ICloudService)menuItem.CommandParameter;

			_viewModel.EditService(clickedService);
		}
	}
}