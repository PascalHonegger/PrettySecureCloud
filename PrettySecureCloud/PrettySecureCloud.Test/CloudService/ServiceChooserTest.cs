using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PrettySecureCloud.CloudServices;
using PrettySecureCloud.CloudServices.ServiceChooser;
using PrettySecureCloud.Infrastructure;
using PrettySecureCloud.Service_References.LoginService;
using PrettySecureCloud.Test.Infrastructure;
using PrettySecureCloud.Test.Properties;

namespace PrettySecureCloud.Test.CloudService
{
	public class ServiceChooserTest : IntegrationTestBase<ServiceChooserViewModel>
	{
		[Test]
		public void CanSearch()
		{
			//Arrange
			Session.Instance.CurrentUser = new User
			{
				Services = new List<Service_References.LoginService.CloudService>()
			};

			var servicemock = new Mock<ICloudService>();


			UnitUnderTest = new ServiceChooserViewModel();
			Assert.That(UnitUnderTest.CloudServices, Is.Empty);

			UnitUnderTest.CloudServices.Add(servicemock.Object);
			Assert.That(UnitUnderTest.CloudServices, Is.Not.Empty);

			//Act
			UnitUnderTest.SearchText = "I hope this string is not in the service list";

			//Assert
			Assert.That(UnitUnderTest.CloudServices, Is.Empty);
		}
	}
}
