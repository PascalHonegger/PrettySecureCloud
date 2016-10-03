using System;
using PrettySecureCloud.Login;

namespace PrettySecureCloud.Pages
{
	public partial class RegistrationPage
	{
		private RegisterViewModel _viewModel;

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
	}
}