// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE.md
// </copyright>
namespace Trifling.Compression
{
    using System.IO.Compression;

    /// <summary>
    /// The configuration options for the compressor implementation.
    /// </summary>
    public class CompressorConfiguration
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
    }
}
