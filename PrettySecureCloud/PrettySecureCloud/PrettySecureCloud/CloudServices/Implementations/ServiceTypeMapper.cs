using System;
using System.Collections.Generic;
using PrettySecureCloud.LoginService;

namespace PrettySecureCloud.CloudServices.Implementations
{
	public static class ServiceTypeMapper
	{
		private static readonly Dictionary<string, ICloudService> Mapping = new Dictionary<string, ICloudService>
		{
			//TODO Add Class-Mapping
			{ "Dropbox", null }
		};

		public static ICloudService ToICloudService(this ServiceType type)
		{
			if(!type.IsSupported()) throw new ArgumentOutOfRangeException(nameof(type), "Only supported / implemented Types are valid!");

			return Mapping[type.Name];
		}

		public static bool IsSupported(this ServiceType type)
		{
			return Mapping.ContainsKey(type.Name);
		}
	}
}
