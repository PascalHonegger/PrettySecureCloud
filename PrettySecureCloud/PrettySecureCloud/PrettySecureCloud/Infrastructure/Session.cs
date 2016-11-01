using PrettySecureCloud.Encryption;
using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.Infrastructure
{
	public class Session
	{
		private Session()
		{
			Encryptor = new AesEncryptor();
		}

		public static Session Instance { get; } = new Session();

		public User CurrentUser { get; set; }
		public IByteEncryptor Encryptor { get; set; }
	}
}
