namespace Ci.Masking
{
	/// <summary>
	/// Defines methods for a string pool containing masked strings.
	/// </summary>
	public interface IMaskStringPool
	{
		/// <summary>
		/// Returns masked string for the given <paramref name="maskLength"/>.
		/// </summary>
		string GetString(int maskLength);
	}
}
