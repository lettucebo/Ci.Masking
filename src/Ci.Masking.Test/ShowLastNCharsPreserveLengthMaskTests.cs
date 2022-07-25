using NUnit.Framework;

namespace Ci.Masking.Test
{
	[TestFixture]
	public class ShowLastNCharsPreserveLengthMaskTests
	{
		[TestFixture]
		public class Mask
		{
			internal const char DifferentMaskCharacter = 'x';

			[Test]
			[TestCase("12345678901234567890", "****************7890")]
			[TestCase("123456", "**3456")]
			[TestCase("12345", "*2345")]
			[TestCase("1234", "1234")]
			[TestCase("123", "123")]
			[TestCase("12", "12")]
			[TestCase("1", "1")]
			[TestCase("", "")]
			[TestCase(null, null)]
			public void Masks_Correctly(string s, string maskedExpected)
			{
				var mask = new ShowLastNCharsPreserveLengthMask();
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("123456", "***456")]
			[TestCase("12345", "**345")]
			[TestCase("1234", "*234")]
			[TestCase("123", "123")]
			[TestCase("12", "12")]
			[TestCase("1", "1")]
			[TestCase("", "")]
			[TestCase(null, null)]
			public void Masks_Correctly_When_CharactersToShow_Specified(string s, string maskedExpected)
			{
				var mask = new ShowLastNCharsPreserveLengthMask(nCharactersToShow: 3);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("12345", $"x2345")]
			public void Masks_Correctly_When_Mask_Specified(string s, string maskedExpected)
			{
				var mask = new ShowLastNCharsPreserveLengthMask(DifferentMaskCharacter);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}
		}
	}
}
