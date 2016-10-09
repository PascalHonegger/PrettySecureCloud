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

			BindingContext = _viewModel = new RegistrationViewModel();

			_viewModel.RegisterCommand.ChangeCanExecute();

			this.Subscribe<RegistrationViewModel, RegistrationPage>();
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