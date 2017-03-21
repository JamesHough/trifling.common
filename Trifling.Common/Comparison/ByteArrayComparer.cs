// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE file
// </copyright>
namespace Trifling.Comparison
{
    using System.Collections.Generic;

    /// <summary>
    /// A comparer for comparing the content of two byte arrays.
    /// </summary>
    public class ByteArrayComparer : IComparer<byte[]>, IEqualityComparer<byte[]>
    {
        /// <summary>
        /// Gets the default instance of the <see cref="ByteArrayComparer"/>. 
        /// </summary>
        public static ByteArrayComparer Default
        {
            get { return new ByteArrayComparer(); }
        }

        /// <summary>
        /// Compares two objects as byte arrays and returns their relative position.
        /// </summary>
        /// <param name="a">The first byte array to compare.</param>
        /// <param name="b">The second byte array to compare.</param>
        /// <returns>Returns -1 if <paramref name="a"/> is before <paramref name="b"/>. Returns 0 if they are equal. Otherwise returns 1.</returns>
        public int Compare(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return 0;
            }

            if (a == null)
            {
                return -1;
            }

            if (b == null)
            {
                return 1;
            }

            for (var i = 0; i < a.Length; i++)
            {
                if (b.Length <= i)
                {
                    return -1;
                }

                var comp = b[i].CompareTo(a[i]);

                if (comp != 0)
                {
                    return comp;
                }
            }

            if (b.Length > a.Length)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Determines if the given byte arrays are equal.
        /// </summary>
        /// <param name="x">The first byte array to compare.</param>
        /// <param name="y">The second byte array to compare.</param>
        /// <returns>Returns true if both arrays are the same length and contain exactly the same values at each position.</returns>
        public bool Equals(byte[] x, byte[] y)
        {
            return this.Compare(x, y) == 0;
        }

        /// <summary>
        /// Generates a hash code for the given byte array value.
        /// </summary>
        /// <param name="obj">The byte array for which to generate a hash code.</param>
        /// <returns>Returns an integer hash code.</returns>
        public int GetHashCode(byte[] obj)
        {
            if (obj == null || obj.Length < 1)
            {
                return 0;
            }

            const int Prime = 37;
            unchecked
            {
                var hash = 0x900;
                for (var i = 0; i < obj.Length; i++)
                {
                    hash = (hash * Prime) + obj[i].GetHashCode();
                }

                return hash;
            }
        }
    }
}
