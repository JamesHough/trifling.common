// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE.md
// </copyright>
namespace Trifling.Compression.Interfaces
{
    using System.IO;

    /// <summary>
    /// A compressor which can compress and decompress data.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        /// Compresses a source byte array and returns a compressed byte array.
        /// </summary>
        /// <param name="source">The source of data to compress.</param>
        /// <returns>Returns a byte array containing the compressed data.</returns>
        byte[] Compress(byte[] source);

        /// <summary>
        /// Decompresses the given source byte array and returns a decompressed byte array.
        /// </summary>
        /// <param name="source">The source of data to decompress.</param>
        /// <returns>Returns a byte array containing the decompressed data.</returns>
        byte[] Decompress(byte[] source);

        /// <summary>
        /// Compresses the input stream and writes the compressed stream to the output stream.
        /// </summary>
        /// <param name="inputStream">The stream from which to read the data to be compressed.</param>
        /// <param name="outputStream">The stream into which the compressed data must be written.</param>
        void CompressStream(Stream inputStream, Stream outputStream);

        /// <summary>
        /// Decompresses the input stream and writes the decompressed stream to the output stream.
        /// </summary>
        /// <param name="inputStream">The stream from which to read the data to be decompressed.</param>
        /// <param name="outputStream">The stream into which the decompressed data must be written.</param>
        void DecompressStream(Stream inputStream, Stream outputStream);
    }
}
