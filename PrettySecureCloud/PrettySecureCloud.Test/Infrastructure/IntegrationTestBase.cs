using NUnit.Framework;

namespace PrettySecureCloud.Test.Infrastructure
{
	[TestFixture]
	[Category("IntegrationTest")]
	public abstract class IntegrationTestBase<T>
	{
		[SetUp]
		public void SetUp()
		{
			DoSetup();
		}

		[TearDown]
		public void TearDown()
		{
			DoTearDown();
		}

		protected T UnitUnderTest;

		protected virtual void DoSetup()
		{
			//Optional
		}

		protected virtual void DoTearDown()
		{
			//Optional
		}
	}
}