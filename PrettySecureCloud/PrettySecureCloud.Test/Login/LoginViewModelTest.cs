using NUnit.Framework;
using PrettySecureCloud.Login;
using PrettySecureCloud.Test.Infrastructure;

namespace PrettySecureCloud.Test.Login
{
	public class LoginViewModelTest : IntegrationTestBase<LoginViewModel>
	{
		protected override void DoSetup()
		{
			base.DoSetup();

			UnitUnderTest = new LoginViewModel();
		}

		[Test]
		public void CanLoginIsTrueIfValuesAreEntered()
		{
			//Arrange
			const string username = "TestUser";
			const string password = "VerySecurePassword";

			//Act
			UnitUnderTest.Username = username;
			UnitUnderTest.Password = password;

			//Assert
			Assert.That(UnitUnderTest.CanLogin(), Is.True);
		}
	}
}
