using System;

namespace Ci.Masking
{
    /// <summary>
    /// A masking impl preserving length.
    /// </summary>
    public class PreserveLengthMask : IMaskValue
    {
        private readonly char _maskCharacter;
        private readonly int _maxMaskLength;

        internal const char DefaultMaskCharacter = '*';
        internal const int DefaultMaxMaskLength = int.MaxValue;

        // reduce GC pressure for first N strings
        internal const int MaskStringPoolSize = 100;
        private readonly IMaskStringPool _maskStringPool;

        public PreserveLengthMask(
            char maskCharacter = DefaultMaskCharacter,
            int maxMaskLength = DefaultMaxMaskLength)
        {
            _maskCharacter = maskCharacter;
            _maxMaskLength = maxMaskLength;

            _maskStringPool = new PreserveLengthMaskStringPool(MaskStringPoolSize, _maskCharacter);
        }

        /// <inheritdoc/>
        public string Mask(string s)
        {
            if (s == null)
            {
                return s;
            }
            var maskedLength = Math.Min(s.Length, _maxMaskLength);
            return _maskStringPool.GetString(maskedLength) ?? new string(_maskCharacter, maskedLength);
        }
    }
}
