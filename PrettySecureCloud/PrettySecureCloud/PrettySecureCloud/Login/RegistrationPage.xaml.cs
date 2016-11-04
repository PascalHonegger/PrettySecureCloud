using System;
using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.Login
{
	public partial class RegistrationPage
	{
		private readonly RegistrationViewModel _viewModel;

		public RegistrationPage()
		{
			InitializeComponent();

			_viewModel = new RegistrationViewModel();
			BindingContext = _viewModel;

			_viewModel.RegisterCommand.ChangeCanExecute();
		}

		private void Entry_OnCompleted(object sender, EventArgs e)
		{
			if (_viewModel.RegisterCommand.CanExecute(null))
			{
				_viewModel.RegisterCommand.Execute(null);
			}
		}

		protected override void OnAppearing()
		{
			this.Subscribe<RegistrationViewModel, RegistrationPage>();
			this.Subscribe<LoginViewModel, RegistrationPage>();
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			this.Unsubscribe<RegistrationViewModel, RegistrationPage>();
			this.Unsubscribe<LoginViewModel, RegistrationPage>();
			base.OnDisappearing();
		}
	}
}