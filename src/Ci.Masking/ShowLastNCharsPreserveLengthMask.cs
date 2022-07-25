using System;
using Ci.Extension.Core;

namespace Ci.Masking
{
    /// <summary>
    /// A masking impl showing last N chars, and preserving length.
    /// </summary>
    public class ShowLastNCharsPreserveLengthMask : IMaskValue
    {
        private readonly char _maskCharacter;
        private readonly int _nCharactersToShow;

        internal const char DefaultMaskCharacter = '*';
        internal const int DefaultNCharactersToShow = 4;

        // reduce GC pressure for first N strings
        internal const int MaskStringPoolSize = 100;
        private readonly IMaskStringPool _maskStringPool;

        public ShowLastNCharsPreserveLengthMask(
            char maskCharacter = DefaultMaskCharacter,
            int nCharactersToShow = DefaultNCharactersToShow)
        {
            if (nCharactersToShow < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(nCharactersToShow), nCharactersToShow, null);
            }

            _maskCharacter = maskCharacter;
            _nCharactersToShow = nCharactersToShow;

            _maskStringPool = new PreserveLengthMaskStringPool(MaskStringPoolSize, _maskCharacter);
        }

        /// <inheritdoc/>
        public string Mask(string input)
        {
            if (input == null)
                return null;

            // note: preserve length
            var maskedLength = Math.Max(0, input.Length - _nCharactersToShow);

            // see https://www.chinhdo.com/20070929/stringbuilder-part-2/
            var masked =
                string.Concat(
                    _maskStringPool.GetString(maskedLength) ?? new string(_maskCharacter, maskedLength),
                    input.SubStringToEnd(_nCharactersToShow));
            return masked;
        }
    }
}
