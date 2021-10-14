
namespace HolzShots
{
    public static class MathEx
    {
        #region Pow

        /// <summary>Returns a specified number raised to the specified power.</summary>
        /// <returns>The number <paramref name="x" /> raised to the power <paramref name="y" />.</returns>
        /// <param name="x">An integer to be raised to a power.</param>
        /// <param name="y">An integer that specifies a power.</param>
        /// <filterpriority>1</filterpriority>
        public static int Pow(int x, int y)
        {
            if (y < 0)
                throw new ArgumentException("Invalid " + nameof(y));
            if (y == 0)
                return 1;
            if (y == 1)
                return x;

            if (x == 2 && y < sizeof(int) * 8)
                return 1 << y;

            var res = 1;
            while (y != 0)
            {
                if ((y & 1) == 1)
                    res *= x;
                x *= x;
                y >>= 1;
            }
            return res;
        }

        /// <summary>Returns a specified number raised to the specified power.</summary>
        /// <returns>The number <paramref name="x" /> raised to the power <paramref name="y" />.</returns>
        /// <param name="x">A long integer to be raised to a power.</param>
        /// <param name="y">A long integer that specifies a power.</param>
        /// <filterpriority>1</filterpriority>
        public static long Pow(long x, long y)
        {
            if (y < 0)
                throw new ArgumentException("Invalid " + nameof(y));
            if (y == 0)
                return 1L;
            if (y == 1L)
                return x;

            if (x == 2 && y < sizeof(long) * 8)
                return 1L << (int)y;

            var res = 1L;
            while (y != 0)
            {
                if ((y & 1) == 1)
                    res = res * x;
                x *= x;
                y >>= 1;
            }
            return res;
        }

        #endregion
    }
}
