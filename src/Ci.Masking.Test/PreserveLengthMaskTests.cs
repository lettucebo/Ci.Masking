using NUnit.Framework;

namespace Ci.Masking.Test
{
	[TestFixture]
	public class PreserveLengthMaskTests
	{
		[TestFixture]
		public class Mask
		{
			internal const char DifferentMaskCharacter = 'x';

			[Test]
			[TestCase("12345678901234567890", "********************")]
			[TestCase("123456", "******")]
			[TestCase("12345", "*****")]
			[TestCase("1234", "****")]
			[TestCase("123", "***")]
			[TestCase("12", "**")]
			[TestCase("1", "*")]
			[TestCase("", "")]
			[TestCase(null, null)]
			public void Masks_Correctly(string s, string maskedExpected)
			{
				var mask = new PreserveLengthMask();
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("123456", "***")]
			[TestCase("12345", "***")]
			[TestCase("1234", "***")]
			[TestCase("123", "***")]
			[TestCase("12", "**")]
			[TestCase("1", "*")]
			[TestCase("", "")]
			[TestCase(null, null)]
			public void Masks_Correctly_When_MaxMaskLength_Specified(string s, string maskedExpected)
			{
				var mask = new PreserveLengthMask(maxMaskLength: 3);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("12345", $"xxxxx")]
			public void Masks_Correctly_When_Mask_Specified(string s, string maskedExpected)
			{
				var mask = new PreserveLengthMask(DifferentMaskCharacter);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}
		}
	}
}
