using System.Globalization;
using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.CloudServices.AddService
{
	/// <summary>
	/// ViewModel to encapsulate the <see cref="ServiceType"/>
	/// </summary>
	public class ServiceTypeViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceTypeViewModel" /> class.
		/// </summary>
		/// <param name="type"></param>
		public ServiceTypeViewModel(ServiceType type)
		{
			Type = type;
			ImageName = $"{Type.Name.ToLowerInvariant()}.png";
		}

		/// <summary>
		/// The ServiceType (Model)
		/// </summary>
		public ServiceType Type { get; }
		/// <summary>
		/// Path to Picture
		/// </summary>
		public string ImageName { get; }
	}
}
