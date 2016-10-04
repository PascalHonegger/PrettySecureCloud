using System;
using PrettySecureCloud.Login;

namespace PrettySecureCloud.Pages
{
	public partial class RegistrationPage
	{
		private readonly RegisterViewModel _viewModel;

		public RegistrationPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new RegisterViewModel();

			Subscribe<RegisterViewModel>();
		}

		~RegistrationPage()
		{
			Unsubscribe<RegisterViewModel>();
		}

		private void Entry_OnCompleted(object sender, EventArgs e)
		{
			if (_viewModel.RegisterCommand.CanExecute(null))
			{
				_viewModel.RegisterCommand.Execute(null);
			}
		}
	}
}