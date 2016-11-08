using System;
using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices.AddService
{
	/// <summary>
	/// This class adds a new ServicePage
	/// </summary>
	public partial class AddServicePage
	{
		private readonly AddServiceViewModel _viewModel;

		/// <summary>
		/// Initializes a new instance of the <see cref="AddServicePage" /> class.
		/// </summary>
		/// <param name="viewModel"></param>
		public AddServicePage(AddServiceViewModel viewModel)
		{
			InitializeComponent();

			_viewModel = viewModel;
			BindingContext = _viewModel;
		}

		/// <inheritdoc />
		protected override void OnAppearing()
		{
			this.Subscribe<AddServiceViewModel, AddServicePage>();
			base.OnAppearing();
		}

		/// <inheritdoc />
		protected override void OnDisappearing()
		{
			this.Unsubscribe<AddServiceViewModel, AddServicePage>();
			base.OnDisappearing();
		}

		private void Entry_OnCompleted(object sender, EventArgs e)
		{
			_viewModel.AuthenticateCommand.Execute(null);
		}
	}
}
