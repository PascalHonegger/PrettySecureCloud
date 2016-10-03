using System;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Login;
using Xamarin.Forms;

namespace PrettySecureCloud.Pages
{
	public partial class LoginPage
	{
		private readonly LoginViewModel _viewModel;

		public LoginPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new LoginViewModel();

			MessagingCenter.Subscribe<LoginViewModel, MessageData>(this, MessageData.DisplayAlert, (sender, message) =>
			{
				if (string.IsNullOrEmpty(message.Accept))
					DisplayAlert(message.Title, message.Content, message.Cancel);
				else
					DisplayAlert(message.Title, message.Content, message.Accept, message.Cancel);
			}, _viewModel);

			MessagingCenter.Subscribe<LoginViewModel, Page>(this, ViewModelBase.NavigationPushView,
				(sender, page) => { Navigation.PushModalAsync(page); }, _viewModel);
		}

		~LoginPage()
		{
			MessagingCenter.Unsubscribe<LoginViewModel, MessageData>(this, MessageData.DisplayAlert);
			MessagingCenter.Unsubscribe<LoginViewModel, Page>(this, ViewModelBase.NavigationPushView);
		}

		private void OnComplete(object sender, EventArgs e)
		{
			if (_viewModel.LoginCommand.CanExecute(null))
				_viewModel.LoginCommand.Execute(null);
		}
	}
}