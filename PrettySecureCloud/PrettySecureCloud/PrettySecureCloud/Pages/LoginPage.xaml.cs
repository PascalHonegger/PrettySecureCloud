using System;
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

			Subscribe<LoginViewModel>();
		}

		~LoginPage()
		{
			Unsubscribe<LoginViewModel>();
		}

		private void OnComplete(object sender, EventArgs e)
		{
			if (_viewModel.LoginCommand.CanExecute(null))
				_viewModel.LoginCommand.Execute(null);
		}
	}
}