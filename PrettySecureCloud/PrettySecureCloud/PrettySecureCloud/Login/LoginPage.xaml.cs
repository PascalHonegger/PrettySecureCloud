using System;
using System.ServiceModel;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.Login
{
	public partial class LoginPage
	{
		private readonly LoginViewModel _viewModel;

		private void TryConnectToServer()
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

			this.Subscribe<LoginViewModel, LoginPage>();
		}

		private void OnComplete(object sender, EventArgs e)
		{
			if (_viewModel.LoginCommand.CanExecute(null))
				_viewModel.LoginCommand.Execute(null);
		}
	}
}