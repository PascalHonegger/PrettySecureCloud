using PrettySecureCloud.Infrastructure;

namespace PrettySecureCloud.CloudServices
{
	public partial class AddServicePage
	{
		public AddServicePage()
		{
			InitializeComponent();

			BindingContext = new AddServiceViewModel();

			this.Subscribe<AddServiceViewModel, AddServicePage>();
		}
	}
}
