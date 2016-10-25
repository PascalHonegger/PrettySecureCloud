using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettySecureCloud.CloudServices.Implementations;
using Xamarin.Forms;

namespace PrettySecureCloud.CloudServices
{
	public class AddServiceViewModel
	{
		public AddServiceViewModel(ServiceTypeViewModel serviceType)
		{
			ServiceTypeViewModel = serviceType;

			//TODO Map serviceType to CloudService

			AuthenticateCommand = new Command(Authenticate);
		}

		private void Authenticate()
		{
			//TODO var loginToken = await CloudService.AuthenticateLoginToken()
		}

		public ServiceTypeViewModel ServiceTypeViewModel { get; set; }

		public ICloudService CloudService { get; }

		public Command AuthenticateCommand { get; }
	}
}
