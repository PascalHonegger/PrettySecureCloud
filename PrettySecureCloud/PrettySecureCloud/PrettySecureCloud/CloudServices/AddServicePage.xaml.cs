using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices
{
	public partial class AddServicePage
	{
		public AddServicePage()
		{
			InitializeComponent();

			AddServiceViewModel viewModel;

			BindingContext = viewModel = new AddServiceViewModel();

			viewModel.AddServiceCommand.ChangeCanExecute();

			this.Subscribe<AddServiceViewModel, AddServicePage>();
		}
	}
}
