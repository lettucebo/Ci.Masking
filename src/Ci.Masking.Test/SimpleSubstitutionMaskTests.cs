using NUnit.Framework;

namespace Ci.Masking.Test
{
	[TestFixture]
	public class SimpleSubstitutionMaskTests
	{
		[TestFixture]
		public class Mask
		{
			internal const string Substitution = "banana";

			[Test]
			[TestCase("12345678901234567890", "bananabananabananaba")]
			[TestCase("123456", "banana")]
			[TestCase("12345", "banan")]
			[TestCase("1234", "bana")]
			[TestCase("123", "ban")]
			[TestCase("12", "ba")]
			[TestCase("1", "b")]
			[TestCase("", "")]
			[TestCase(null, null)]
			public void Masks_Correctly(string s, string maskedExpected)
			{
				var mask = new SimpleSubstitutionMask(Substitution);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}
		}
	}
}
