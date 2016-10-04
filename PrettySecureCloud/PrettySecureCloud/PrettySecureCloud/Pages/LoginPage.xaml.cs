using System;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Login;

namespace PrettySecureCloud.Pages
{
	public partial class LoginPage
	{
		private readonly LoginViewModel _viewModel;

		public LoginPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new LoginViewModel();

			_viewModel.LoginCommand.ChangeCanExecute();
			_viewModel.RegisterCommand.ChangeCanExecute();

			this.Subscribe<LoginViewModel, LoginPage>();
		}

		~LoginPage()
		{
			this.Unsubscribe<LoginViewModel, LoginPage>();
		}

		private void OnComplete(object sender, EventArgs e)
		{
			if (_viewModel.LoginCommand.CanExecute(null))
				_viewModel.LoginCommand.Execute(null);
		}
	}
}