﻿using System;

namespace Ci.Masking
{
    /// <inheritdoc cref="IMaskStringPool"/>
    public class CompressLengthMaskStringPool : IMaskStringPool
    {
        private readonly string[] _pool;

        public CompressLengthMaskStringPool(
            byte size,
            char maskCharacter)
        {
            _pool = new string[size + 1];
            BuildPool(maskCharacter);
        }

        private void BuildPool(char maskCharacter)
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = i >= 3 ? $"{maskCharacter}{i}{maskCharacter}" : new string(maskCharacter, i);
            }
        }

        /// <inheritdoc/>
        public string GetString(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }
            return length < _pool.Length ? _pool[length] : null;
        }
    }
}