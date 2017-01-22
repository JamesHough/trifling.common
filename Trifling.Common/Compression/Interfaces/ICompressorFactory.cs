// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE.md
// </copyright>
namespace Trifling.Compression.Interfaces
{
    /// <summary>
    /// A factory for generating implementations of compressor interfaces.
    /// </summary>
    public interface ICompressorFactory 
    {
        /// <summary>
        /// Creates a new instance of the compressor which matches the requested type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of compressor to create.</typeparam>
        /// <param name="configuration">The configuration to use when generating the instance of the compressor.</param>
        /// <returns>Returns an implementation of the compressor requested.</returns>
        T Create<T>(CompressorConfiguration configuration) where T : ICompressor;
    }
}
