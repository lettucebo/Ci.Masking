using System;
using Ci.Extension.Core;

namespace Ci.Masking
{
    /// <summary>
    /// An opinionated masking impl.
    /// <para></para>
    /// Masks string, preserving length, by masking X+ characters in middle and
    /// showing Y- characters split between start and end preferring start if Y is odd.
    /// </summary>
    /// <remarks>
    /// X = 6,
    /// Y = 5,
    /// mask character = *
    /// <para></para>
    /// example:
    /// 123456789abc => 123*******bc,
    /// 123456 => ******
    /// </remarks>
    public class OpinionatedMask : IMaskValue
    {
        private readonly char _maskCharacter;
        private readonly int _minCharactersToMask;
        private readonly int _maxCharactersToShow;

        internal const char DefaultMaskCharacter = '*';
        internal const int DefaultMinCharactersToMask = 6;
        internal const int DefaultMaxCharactersToShow = 5;

        // reduce GC pressure for first N strings
        internal const int MaskStringPoolSize = 100;
        private readonly IMaskStringPool _maskStringPool;

        public OpinionatedMask(
            char maskCharacter = DefaultMaskCharacter,
            int minCharactersToMask = DefaultMinCharactersToMask,
            int maxCharactersToShow = DefaultMaxCharactersToShow)
        {
            if (minCharactersToMask < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minCharactersToMask), minCharactersToMask, null);
            }
            if (maxCharactersToShow < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCharactersToShow), maxCharactersToShow, null);
            }

            this._maskCharacter = maskCharacter;
            this._minCharactersToMask = minCharactersToMask;
            this._maxCharactersToShow = maxCharactersToShow;

            _maskStringPool = new PreserveLengthMaskStringPool(MaskStringPoolSize, this._maskCharacter);
        }

        /// <summary>
        /// Masks <paramref name="s"/>, preserving length, by masking X+ characters in middle and
        /// showing Y- characters split between start and end preferring start if Y is odd.
        /// </summary>
        /// <remarks>
        /// X = 6,
        /// Y = 5,
        /// mask character = *
        /// </remarks>
        public string Mask(string s)
        {
            if (s == null)
            {
                return s;
            }

            // note: preserve length
            var showCharacters = Math.Min(_maxCharactersToShow, Math.Max(0, s.Length - _minCharactersToMask));
            var showEndCharacters = showCharacters / 2;
            // show 1 extra start character on odd showCharacters
            var showStartCharacters = showCharacters - showEndCharacters;

            var maskedLength = s.Length - showCharacters;

            // see https://www.chinhdo.com/20070929/stringbuilder-part-2/
            var masked =
                string.Concat(
                    s.Substring(0, showStartCharacters),
                    _maskStringPool.GetString(maskedLength) ?? new string(_maskCharacter, maskedLength),
                    s.SubStringToEnd(showEndCharacters));
            return masked;
        }
    }
}
