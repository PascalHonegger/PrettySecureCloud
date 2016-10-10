using NUnit.Framework;
using PrettySecureCloud.Login;
using PrettySecureCloud.Test.Infrastructure;

namespace PrettySecureCloud.Test.Login
{
	public class RegistrationViewModelTest : IntegrationTestBase<RegistrationViewModel>
	{
		protected override void DoSetup()
		{
			base.DoSetup();

			UnitUnderTest = new RegistrationViewModel();
		}

		private const string ValidUsername = "VeryCoolUser";
		private const string InvalidUsername = "u";

		private const string ValidPassword = "VerySecureP@$$vv0rd";
		private const string InvalidPassword = "p";

		private const string ValidEmail = "test@mail.com";
		private const string InvalidEmail = "p";

		[Test]
		public void CanRegisterIsTrueIfValuesAreEntered()
		{
			//Arrange

			//Act
			UnitUnderTest.Username = ValidUsername;
			UnitUnderTest.Password1 = ValidPassword;
			UnitUnderTest.Password2 = ValidPassword;
			UnitUnderTest.Email = ValidEmail;

			//Assert
			Assert.That(UnitUnderTest.CanRegister(), Is.True);
		}

		[Test]
		public void CanRegisterIsFalseIfPasswordInvalid()
		{
			//Arrange
			UnitUnderTest.Username = ValidUsername;
			UnitUnderTest.Email = ValidEmail;

			//Act & Assert
			Assert.That(UnitUnderTest.CanRegister, Is.False);

			//Both passwords too short
			UnitUnderTest.Password1 = InvalidPassword;
			UnitUnderTest.Password2 = InvalidPassword;
			Assert.That(UnitUnderTest.CanRegister, Is.False);

			//One password correct
			UnitUnderTest.Password1 = ValidPassword;
			Assert.That(UnitUnderTest.CanRegister, Is.False);

			//Both correct
			UnitUnderTest.Password2 = ValidPassword;
			Assert.That(UnitUnderTest.CanRegister, Is.True);
		}
	}
}
