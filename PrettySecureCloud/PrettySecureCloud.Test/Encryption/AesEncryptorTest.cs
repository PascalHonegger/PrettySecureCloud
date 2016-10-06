using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PrettySecureCloud.Encryption;
using PrettySecureCloud.Test.Infrastructure;
using PrettySecureCloud.Test.Properties;

namespace PrettySecureCloud.Test.Encryption
{
	public class AesEncryptorTest : IntegrationTestBase<AesEncryptor>
	{
		protected override void DoSetup()
		{
			base.DoSetup();

			UnitUnderTest = new AesEncryptor();
		}

		public AesEncryptorTest()
		{
			_unencryptetBytes = Resources.Test;
		}

		private readonly byte[] _unencryptetBytes;

		[Test]
		public void CanEncryptData()
		{
			//Act
			var encrypted = UnitUnderTest.Encrypt(_unencryptetBytes, new byte[16] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16});
			
			//Assert
			Assert.That(_unencryptetBytes, Is.Not.EqualTo(encrypted));
		}

		[Test]
		public void CanDecryptEncryptData()
		{
			//Arrange
			var key = new byte[16] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
			var encrypted = UnitUnderTest.Encrypt(_unencryptetBytes, key);

			//Act
			var decrypted = UnitUnderTest.Decrypt(encrypted, key);

			//Assert
			Assert.That(decrypted, Is.EqualTo(_unencryptetBytes));
		}
	}
}
