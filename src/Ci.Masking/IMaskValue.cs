namespace Ci.Masking
{
	/// <summary>
	/// Defines impl for masking.
	/// </summary>
	public interface IMaskValue
	{
		/// <summary>
		/// Mask <paramref name="value"/>.
		/// <para></para>
		/// <see href="https://en.wikipedia.org/wiki/Data_masking#Masking_out">source</see>
		/// </summary>
		string Mask(string value);
	}
}
