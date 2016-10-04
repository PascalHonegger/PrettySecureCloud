using System;
using PrettySecureCloud.Infrastructure;
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

			this.Subscribe<RegisterViewModel, RegistrationPage>();
		}

		~RegistrationPage()
		{
			this.Unsubscribe<RegisterViewModel, RegistrationPage>();
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