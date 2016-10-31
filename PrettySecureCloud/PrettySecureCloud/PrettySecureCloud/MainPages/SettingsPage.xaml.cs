using System;
using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.MainPages
{
	public partial class SettingsPage
	{
		private readonly SettingsViewModel _viewModel;

		public SettingsPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new SettingsViewModel();

			_viewModel.ChangePasswordCommand.ChangeCanExecute();
		}

		private void OnComplete(object sender, EventArgs e)
		{
			if (_viewModel.ChangePasswordCommand.CanExecute(null))
			{
				_viewModel.ChangePasswordCommand.Execute(null);
			}
		}

		protected override void OnAppearing()
		{
			this.Subscribe<SettingsViewModel, SettingsPage>();
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			this.Unsubscribe<SettingsViewModel, SettingsPage>();
			base.OnDisappearing();
		}
	}
}
