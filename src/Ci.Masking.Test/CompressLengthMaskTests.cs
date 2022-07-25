using NUnit.Framework;

namespace Ci.Masking.Test
{
	[TestFixture]
	public class CompressLengthMaskTests
	{
		[TestFixture]
		public class Mask
		{
			internal const char DifferentMaskCharacter = 'x';

			[Test]
			[TestCase("12345678901234567890", "*20*")]
			[TestCase("123456", "*6*")]
			[TestCase("12345", "*5*")]
			[TestCase("1234", "*4*")]
			[TestCase("123", "*3*")]
			[TestCase("12", "**")]
			[TestCase("1", "*")]
			[TestCase("", "")]
			[TestCase(null, null)]
			public void Masks_Correctly(string s, string maskedExpected)
			{
				var mask = new CompressLengthMask();
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("12345", $"x5x")]
			public void Masks_Correctly_When_Mask_Specified(string s, string maskedExpected)
			{
				var mask = new CompressLengthMask(DifferentMaskCharacter);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}
		}
	}
}
