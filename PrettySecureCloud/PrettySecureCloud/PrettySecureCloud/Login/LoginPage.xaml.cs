using System;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.LoginService;

namespace PrettySecureCloud.Login
{
	public partial class LoginPage
	{
		private readonly LoginViewModel _viewModel;

		private static void TryConnectToServer()
		{
			ViewModelBase.Service = new LoginServiceClient(LoginServiceClient.EndpointConfiguration.BasicHttpsBinding_ILoginService);

			ViewModelBase.Service.OpenAsync();
		}

		public LoginPage()
		{
			InitializeComponent();

			_viewModel = new LoginViewModel();

			TryConnectToServer();

			BindingContext = _viewModel;

			_viewModel.LoginCommand.ChangeCanExecute();
			_viewModel.RegisterCommand.ChangeCanExecute();
		}

		protected override void OnAppearing()
		{
			this.Subscribe<LoginViewModel, LoginPage>();
			base.OnAppearing();
		}

		private void OnComplete(object sender, EventArgs e)
		{
			if (_viewModel.LoginCommand.CanExecute(null))
				_viewModel.LoginCommand.Execute(null);
		}

		protected override void OnDisappearing()
		{
			this.Unsubscribe<LoginViewModel, LoginPage>();
			base.OnDisappearing();
		}
	}
}