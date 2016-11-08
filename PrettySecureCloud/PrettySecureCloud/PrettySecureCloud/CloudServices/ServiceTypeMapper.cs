using System;
using System.Collections.Generic;
using PrettySecureCloud.CloudServices.Implementations;
using PrettySecureCloud.Service_References.LoginService;

namespace PrettySecureCloud.CloudServices
{
	/// <summary>
	/// This Class Mapps a ServiceType
	/// </summary>
	public static class ServiceTypeMapper
	{
		private static readonly Dictionary<string, Func<ServiceType, ICloudService>> Mapping = new Dictionary<string, Func<ServiceType, ICloudService>>
		{
			{ "Dropbox", t => new DropboxImplementation(t) }
		};

		/// <summary>
		/// Extentionmethod to convert a <see cref="ServiceType" /> to an instance of <see cref="ICloudService"/>
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static ICloudService ToICloudService(this ServiceType type)
		{
			if(!type.IsSupported()) throw new ArgumentOutOfRangeException(nameof(type), "Only supported / implemented Types are valid!");

			return Mapping[type.Name](type);
		}

		/// <summary>
		/// Extentionmethod to convert a <see cref="CloudService" /> to an instance of <see cref="ICloudService"/>
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		public static ICloudService ToICloudService(this CloudService service)
		{
			var type = service.Type.ToICloudService();

			type.Model = service;

			return type;
		}

		/// <summary>
		/// Extentionmethod to check if a Servicetype is supportet by the mapper
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsSupported(this ServiceType type)
		{
			return Mapping.ContainsKey(type.Name);
		}
	}
}
