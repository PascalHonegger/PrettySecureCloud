using System;
using System.Collections.Generic;
using PrettySecureCloud.LoginService;

namespace PrettySecureCloud.CloudServices.Implementations
{
	public static class ServiceTypeMapper
	{
		private static readonly Dictionary<string, Func<ServiceType, ICloudService>> Mapping = new Dictionary<string, Func<ServiceType, ICloudService>>
		{
			{ "Dropbox", t => new DropboxImplementation(t) }
		};

		public static ICloudService ToICloudService(this ServiceType type)
		{
			if(!type.IsSupported()) throw new ArgumentOutOfRangeException(nameof(type), "Only supported / implemented Types are valid!");

			return Mapping[type.Name](type);
		}

		public static ICloudService ToICloudService(this CloudService service)
		{
			var type = service.Type.ToICloudService();

			type.Model = service;

			return type;
		}

		public static bool IsSupported(this ServiceType type)
		{
			return Mapping.ContainsKey(type.Name);
		}
	}
}
