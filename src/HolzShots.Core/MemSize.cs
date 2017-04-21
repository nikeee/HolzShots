using System;

namespace HolzShots
{
    /// <summary>
    /// Represents a size of memory, e.g. 1GiB. Can be used to display some file size on the screen, using the ToString method.
    /// Supports creating instances from specific sizes and notations (decimal prefix vs binary prefix).
    /// Also has some operators (+/-/==/!=).
    /// </summary>
    public struct MemSize : IEquatable<MemSize>, IComparable<MemSize>
    {
        /// <summary>Represents the size 0 bytes.</summary>
        public static MemSize Zero => default(MemSize);

        /// <summary>Gets the number of bytes associated with this instance.</summary>
        public long ByteCount { get; }

        #region Ctor

        /// <summary>Creates an instance using a plain byte count.</summary>
        /// <param name="bytes">The number of bytes</param>
        public MemSize(long bytes)
            : this(bytes, MemSizeUnit.Bytes)
        { }
        /// <summary>Creates an intance converting it from a specific notation</summary>
        /// <param name="prefixedBytes">Number of bytes in the specified magnitude.</param>
        /// <param name="type">The magniture: GiB, GB, MiB, KB, KiB etc.</param>
        public MemSize(long prefixedBytes, MemSizeUnit type)
        {
            if (prefixedBytes == 0)
            {
                ByteCount = 0;
                return;
            }

            if (type == MemSizeUnit.Bytes)
            {
                ByteCount = prefixedBytes;
                return;
            }
            int kind = (int)type >> 8;
            int factorIdentifier = (int)type & 0xff;
            int baseInt = (PrefixType)kind == PrefixType.Binary ? 1024 : 1000;

            ByteCount = prefixedBytes * MathEx.Pow(baseInt, factorIdentifier);
        }

        #endregion

        public static MemSize Parse(string value)
        {
            if (!TryParse(value, out var res)) // Cheap implementation
                throw new FormatException();
            return res;
        }

        public static bool TryParse(string value, out MemSize result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = Zero;
                return true;
            }

            value = value.Trim();

            // Handle simple case: value ist just an integer number
            // If that's the case, just use it as a plain byte count
            if (long.TryParse(value, out var res))
            {
                result = new MemSize(res);
                return true;
            }
            throw new NotImplementedException(); // TODO: Implement
        }

        #region +/- operators

        public static MemSize operator +(MemSize bs1, MemSize bs2) => new MemSize(bs1.ByteCount + bs2.ByteCount);
        public static MemSize Add(MemSize bs1, MemSize bs2) => bs1 + bs2;
        public static MemSize operator ++(MemSize b) => new MemSize(b.ByteCount + 1);
        public static MemSize Increment(MemSize b) => new MemSize(b.ByteCount + 1);

        public static MemSize operator -(MemSize bs1, MemSize bs2) => new MemSize(bs1.ByteCount - bs2.ByteCount);
        public static MemSize Subtract(MemSize bs1, MemSize bs2) => bs1 - bs2;
        public static MemSize operator --(MemSize b) => new MemSize(b.ByteCount - 1);
        public static MemSize Decrement(MemSize b) => new MemSize(b.ByteCount - 1);

        #endregion
        #region gt/lt operators

        public static bool operator <(MemSize b1, MemSize b2) => b1.ByteCount < b2.ByteCount;
        public static bool operator <=(MemSize b1, MemSize b2) => b1.ByteCount <= b2.ByteCount;
        public static bool operator >(MemSize b1, MemSize b2) => b1.ByteCount > b2.ByteCount;
        public static bool operator >=(MemSize b1, MemSize b2) => b1.ByteCount >= b2.ByteCount;

        public int CompareTo(MemSize other) => this.ByteCount.CompareTo(other.ByteCount);

        #endregion
        #region casts

        public static explicit operator long(MemSize value) => value.ByteCount;
        public long ToInt64() => ByteCount;

        #endregion
        #region Equals

        public static bool operator ==(MemSize a, MemSize b) => a.ByteCount == b.ByteCount;
        public static bool operator !=(MemSize a, MemSize b) => a.ByteCount != b.ByteCount;

        public override bool Equals(object obj) => obj is MemSize mem && mem == this;
        public bool Equals(MemSize other) => other.ByteCount == ByteCount;

        public override int GetHashCode() => ByteCount.GetHashCode();

        #endregion
        #region ToString

        public string ToString(PrefixType prefixType) => Format(ByteCount, prefixType);

        public override string ToString() => ToString(PrefixType.Binary);

        private static string Format(long size, PrefixType prefixType)
        {
            int unit = prefixType == PrefixType.Decimal ? 1000 : 1024;
            string i = prefixType == PrefixType.Decimal ? string.Empty : "i";

            if (size < unit)
                return $"{size:F0} bytes";

            if (size < MathEx.Pow(unit, 2))
                return $"{(size / unit):F0} K{i}B";

            if (size < MathEx.Pow(unit, 3))
                return $"{(size / MathEx.Pow(unit, 2)):F0} M{i}B";

            if (size < MathEx.Pow(unit, 4))
                return $"{(size / MathEx.Pow(unit, 3)):F0} G{i}B";

            if (size < MathEx.Pow(unit, 5))
                return $"{(size / MathEx.Pow(unit, 4)):F0} T{i}B";

            if (size < MathEx.Pow(unit, 6))
                return $"{(size / MathEx.Pow(unit, 5)):F0} P{i}B";

            return $"{(size / MathEx.Pow(unit, 6)):F0} E{i}B";
        }

        #endregion
    }

    public enum MemSizeUnit
    {
        Bytes = 0x000,

        KiloByte = 0x101,
        MegaByte,
        GigaByte,
        TeraByte,
        PetaByte,
        ExaByte,

        KibiByte = 0x201,
        MibiByte,
        GibiByte,
        TebiByte,
        PebiByte,
        ExbiByte
    }

#pragma warning disable CA1720 // Identifier contains type name
    public enum PrefixType
    {
        /// <summary>???</summary>
        Unknown = 0x0,
        /// <summary>Decimal prefix. See <see href="https://en.wikipedia.org/wiki/Binary_prefix">Wikipedia</see>.</summary>
        Decimal = 0x1,
        /// <summary>Decimal prefix. See <see href="https://en.wikipedia.org/wiki/Binary_prefix">Wikipedia</see>.</summary>
        Binary = 0x2
    }
#pragma warning restore CA1720 // Identifier contains type name
}
