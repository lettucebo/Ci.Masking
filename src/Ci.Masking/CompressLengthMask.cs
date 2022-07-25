namespace Ci.Masking
{
    /// <summary>
    /// A masking impl compressing length.
    /// </summary>
    public class CompressLengthMask : IMaskValue
    {
        private readonly char _maskCharacter;

        internal const char DefaultMaskCharacter = '*';

        // reduce GC pressure for first N strings
        internal const int MaskStringPoolSize = 100;
        private readonly IMaskStringPool _maskStringPool;

        public CompressLengthMask(
            char maskCharacter = DefaultMaskCharacter)
        {
            this._maskCharacter = maskCharacter;

            _maskStringPool = new CompressLengthMaskStringPool(MaskStringPoolSize, this._maskCharacter);
        }

        /// <inheritdoc/>
        public string Mask(string s)
        {
            if (s == null)
            {
                return s;
            }
            var maskedLength = s.Length;
            return _maskStringPool.GetString(maskedLength) ??
                (maskedLength >= 3 ? $"{_maskCharacter}{maskedLength}{_maskCharacter}" : new string(_maskCharacter, maskedLength));
        }
    }
}
