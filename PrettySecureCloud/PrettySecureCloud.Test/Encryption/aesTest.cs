using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PrettySecureCloud.Encryption;
using PrettySecureCloud.Test.Infrastructure;

namespace PrettySecureCloud.Test.Encryption
{
	class aesTest : IntegrationTestBase<AES>
	{
		protected override void DoSetup()
		{
			base.DoSetup();

			UnitUnderTest = new AES();
		}

		[Test]
		public void CanEncryptData()
		{
			//Arrange
			var file = File.ReadAllBytes(@"C:\Users\Serafin\Source\Repos\PrettySecureCloud\PrettySecureCloud\PrettySecureCloud.Test\TestData\Test.txt");
			//Act
			var encrypted = UnitUnderTest.EncryptAes(file, new byte[16] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16});
			//Assert
			Assert.That(file, Is.Not.EqualTo(encrypted));
		}
	}
}
