using System;
using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.Login
{
	public partial class RegistrationPage
	{
		private readonly RegisterViewModel _viewModel;

		public RegistrationPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new RegisterViewModel();

			_viewModel.RegisterCommand.ChangeCanExecute();

			this.Subscribe<RegisterViewModel, RegistrationPage>();
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