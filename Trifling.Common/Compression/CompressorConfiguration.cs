// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE.md
// </copyright>
namespace Trifling.Compression
{
    using System;
    using System.IO.Compression;

    /// <summary>
    /// The configuration options for the compressor implementation.
    /// </summary>
    public class CompressorConfiguration : IEquatable<CompressorConfiguration>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CompressorConfiguration"/> class with the default options. 
        /// </summary>
        /// <remarks>Default <see cref="CompressorConfiguration.MinimumSizeToCompress"/> is 200 and 
        /// default <see cref="CompressorConfiguration.CompressionLevel"/> is Fastest.</remarks>
        public CompressorConfiguration()
            : this(200, CompressionLevel.Fastest)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="CompressorConfiguration"/> class with the given property values. 
        /// </summary>
        /// <param name="minimumSizeToCompress">The minimum input size which will be considered valid for compression. Any 
        /// value less than this value will not be compressed but will be returned unchanged.</param>
        /// <param name="compressionLevel">The level of compression that the implementation will use when performing the 
        /// compression.</param>
        public CompressorConfiguration(int minimumSizeToCompress, CompressionLevel compressionLevel)
        {
            this.MinimumSizeToCompress = minimumSizeToCompress;
            this.CompressionLevel = compressionLevel;
        }

        /// <summary>
        /// Gets or sets the minimum input size which will be considered valid for compression. Any value less than this value
        /// will not be compressed but will be returned unchanged.
        /// </summary>
        public int MinimumSizeToCompress { get; set; }

        /// <summary>
        /// Gets or sets the level of compression that the implementation will use when performing the compression.
        /// </summary>
        public CompressionLevel CompressionLevel { get; set; }

        #region IEquatable interface

        /// <summary>
        /// Calculates the hash code for this instance based on the values of the properties.
        /// </summary>
        /// <returns>Returns an integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.CompressionLevel.GetHashCode() * 37) + this.MinimumSizeToCompress.GetHashCode();
            }
        }

        /// <summary>
        /// Determines, based on property values, whether or not this instance equals the given instance.
        /// </summary>
        /// <param name="other">The instance to which the current instance is being compared.</param>
        /// <returns>Returns true if both have the same values, otherwise false.</returns>
        public bool Equals(CompressorConfiguration other)
        {
            return (other == null)
                ? false
                : this.CompressionLevel.Equals(other.CompressionLevel)
                    && this.MinimumSizeToCompress.Equals(other.MinimumSizeToCompress);
        }

        /// <summary>
        /// Determines, based on property values, whether or not this instance equals the given instance.
        /// </summary>
        /// <param name="obj">The instance to which the current instance is being compared.</param>
        /// <returns>Returns true if both have the same values, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as CompressorConfiguration;
            return (other == null)
                ? false
                : this.Equals(other);
        }

        #endregion IEquatable interface
    }
}
