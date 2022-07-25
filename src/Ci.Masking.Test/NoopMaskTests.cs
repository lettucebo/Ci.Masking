using NUnit.Framework;

namespace Ci.Masking.Test
{
	[TestFixture]
	public class NoopMaskTests
	{
		[TestFixture]
		public class Mask
		{
			[Test]
			[TestCase("12345", "12345")]
			[TestCase("1234", "1234")]
			[TestCase("123", "123")]
			[TestCase("12", "12")]
			[TestCase("1", "1")]
			[TestCase("", "")]
			[TestCase(null, null)]
			public void Masks_Correctly(string s, string maskedExpected)
			{
				var mask = new NoopMask();
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}
		}
	}
}
