using NUnit.Framework;
using static Ci.Masking.FixedMask;

namespace Ci.Masking.Test
{
	[TestFixture]
	public class FixedMaskTests
	{
		[TestFixture]
		public class Mask
		{
			internal const string DifferentFixedMask = "xxx";

			[Test]
			[TestCase("123456", DefaultFixedMask)]
			[TestCase("12345", DefaultFixedMask)]
			[TestCase("1234", DefaultFixedMask)]
			[TestCase("123", DefaultFixedMask)]
			[TestCase("12", DefaultFixedMask)]
			[TestCase("1", DefaultFixedMask)]
			[TestCase("", DefaultFixedMask)]
			[TestCase(null, null)]
			public void Masks_Correctly(string s, string maskedExpected)
			{
				var mask = new FixedMask();
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}

			[Test]
			[TestCase("12345", DifferentFixedMask)]
			public void Masks_Correctly_When_Mask_Specified(string s, string maskedExpected)
			{
				var mask = new FixedMask(DifferentFixedMask);
				var masked = mask.Mask(s);
				Assert.AreEqual(maskedExpected, masked);
			}
		}
	}
}
