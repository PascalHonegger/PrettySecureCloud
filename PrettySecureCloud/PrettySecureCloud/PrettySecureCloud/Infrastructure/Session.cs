using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.Infrastructure
{
	public class Session
	{
		private Session()
		{

		}

		public static Session Instance { get; } = new Session();

		public User CurrentUser { get; set; }
	}
}
