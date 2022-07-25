using System;
using System.Text;
using Ci.Extension.Core;

namespace Ci.Masking
{
    /// <summary>
    /// A masking impl substituting with given string.
    /// </summary>
    public class SimpleSubstitutionMask : IMaskValue
    {
        private readonly string _substitution;

        public SimpleSubstitutionMask(
            string substitution)
        {
            _substitution = substitution.NullIfWhiteSpace()
                ?? throw new ArgumentNullException(nameof(substitution));
        }

        /// <inheritdoc/>
        public string Mask(string s)
        {
            if (s == null)
                return null;

            var quotient = s.Length / _substitution.Length;
            var remainder = s.Length - (quotient * _substitution.Length);
            var masked =
                new StringBuilder(s.Length)
                    .Insert(0, _substitution, quotient)
                    .Append(_substitution.SubstringFromStart(remainder))
                    .ToString();
            return masked;
        }
    }
}
