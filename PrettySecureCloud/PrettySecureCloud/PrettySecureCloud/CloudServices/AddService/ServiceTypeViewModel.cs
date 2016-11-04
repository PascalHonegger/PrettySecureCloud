using System.Globalization;
using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.CloudServices.AddService
{
	public class ServiceTypeViewModel
	{
		public ServiceTypeViewModel(ServiceType type)
		{
			Type = type;
			ImageName = $"{Type.Name.ToLowerInvariant()}.png";
		}

		public ServiceType Type { get; }
		public string ImageName { get; }
	}
}
