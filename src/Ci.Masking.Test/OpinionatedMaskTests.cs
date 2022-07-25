using NUnit.Framework;

namespace Ci.Masking.Test
{
	[TestFixture]
	public class OpinionatedMaskTests
	{
		[TestFixture]
		public class Mask
		{
			internal const char DifferentMaskCharacter = 'x';

			[Test]
			[TestCase("123456789abc", "123*******bc")]
			[TestCase("123456789ab", "123******ab")]
			[TestCase("123456789a", "12******9a")]
			[TestCase("123456789", "12******9")]
			[TestCase("12345678", "1******8")]
			[TestCase("1234567", "1******")]
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
				var mask = new OpinionatedMask();
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("1234567890", "12xxxxxx90")]
			public void Masks_Correctly_When_Mask_Specified(string s, string maskedExpected)
			{
				var mask = new OpinionatedMask(maskCharacter: DifferentMaskCharacter);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("1234567890", "1********0")]
			public void Masks_Correctly_When_MinCharactersToMask_Specified(string s, string maskedExpected)
			{
				var mask = new OpinionatedMask(minCharactersToMask: 8);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("1234567890", "1********0")]
			public void Masks_Correctly_When_MaxCharactersToShow_Specified(string s, string maskedExpected)
			{
				var mask = new OpinionatedMask(maxCharactersToShow: 2);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}
		}
	}
}
