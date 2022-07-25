namespace Ci.Masking
{
	/// <summary>
	/// A masking impl with a noop mask.
	/// </summary>
	public class NoopMask : IMaskValue
	{
		public NoopMask()
		{ }

		/// <inheritdoc/>
		public string Mask(string s)
		{
			return s;
		}
	}
}
