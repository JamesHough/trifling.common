// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE.md
// </copyright>
namespace Trifling.Compression.Impl
{
    using System.IO;
    using System.IO.Compression;

    using Microsoft.Extensions.Options;

    using Trifling.Compression.Interfaces;

    /// <summary>
    /// A compressor which can compress and decompress data in Deflate format.
    /// </summary>
    public class DeflateCompressor : IDeflateCompressor
    {
        /// <summary>
        /// The configuration options for this implementation of a Deflate Compressor.
        /// </summary>
        private readonly CompressorConfiguration _configuration;

        /// <summary>
        /// Initialises a new instance of the <see cref="DeflateCompressor"/> class with the default configuration.
        /// </summary>
        public DeflateCompressor()
        {
            this._configuration = new CompressorConfiguration();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DeflateCompressor"/> class with the specified configuration. 
        /// </summary>
        /// <param name="configuration">The configuration options for the compression engine.</param>
        public DeflateCompressor(IOptions<CompressorConfiguration> configuration)
        {
            this._configuration = configuration.Value ?? new CompressorConfiguration();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DeflateCompressor"/> class with the specified configuration. 
        /// </summary>
        /// <param name="configuration">The configuration options for the compression engine.</param>
        public DeflateCompressor(CompressorConfiguration configuration)
        {
            this._configuration = configuration ?? new CompressorConfiguration();
        }

        /// <summary>
        /// Compresses a source byte array and returns a Deflate compressed byte array.
        /// </summary>
        /// <param name="source">The source of data to compress.</param>
        /// <returns>Returns a byte array containing the compressed data.</returns>
        /// <remarks>If the input byte array is smaller than the configured minimum size then the same data is returned without modification.</remarks>
        public byte[] Compress(byte[] source)
        {
            using (var inStream = new MemoryStream(source, false))
            {
                using (var outStream = new MemoryStream())
                {
                    this.CompressStream(inStream, outStream);

                    outStream.Seek(0L, SeekOrigin.Begin);
                    return outStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Compresses the input stream and writes the Deflate compressed stream to the output stream.
        /// </summary>
        /// <param name="inputStream">The stream from which to read the data to be compressed.</param>
        /// <param name="outputStream">The stream into which the compressed data must be written.</param>
        /// <remarks>If the input stream is shorter than the configured minimum size then the same stream data is copied to the output without modification.</remarks>
        public void CompressStream(Stream inputStream, Stream outputStream)
        {
            var initialBuffer = new byte[this._configuration.MinimumSizeToCompress];
            var readLength = inputStream.Read(initialBuffer, 0, initialBuffer.Length);
            if (readLength < initialBuffer.Length)
            {
                // the data length is shorter than the configured minimum. output the uncompressed data.
                outputStream.Write(initialBuffer, 0, readLength);
                outputStream.Flush();
                return;
            }

            // prepare the Deflate stream by outputting the configured compression level as the header.
            outputStream.WriteByte(0x78);

            switch (this._configuration.CompressionLevel)
            {
                case CompressionLevel.NoCompression:
                    outputStream.WriteByte(0x01);
                    break;
                case CompressionLevel.Optimal:
                    outputStream.WriteByte(0xda);
                    break;
                default:
                    outputStream.WriteByte(0x9c);
                    break;
            }

            // now compress the actual data.
            using (var engine = new DeflateStream(outputStream, this._configuration.CompressionLevel, true))
            {
                // first write the first chunk that we already read earlier.
                engine.Write(initialBuffer, 0, readLength);

                // now copy the remaining input stream to the compression engine and out to the output stream.
                inputStream.CopyTo(engine);

                engine.Flush();
            }
        }

        /// <summary>
        /// Decompresses the given source Deflate byte array and returns a decompressed byte array.
        /// </summary>
        /// <param name="source">The source of data to decompress.</param>
        /// <returns>Returns a byte array containing the decompressed data.</returns>
        /// <remarks>If the input byte array does not contain a valid Deflate header then the same data is returned without modification.</remarks>
        public byte[] Decompress(byte[] source)
        {
            using (var inStream = new MemoryStream(source, false))
            {
                using (var outStream = new MemoryStream())
                {
                    this.DecompressStream(inStream, outStream);

                    outStream.Seek(0L, SeekOrigin.Begin);
                    return outStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Decompresses the input stream and writes the decompressed stream to the output stream.
        /// </summary>
        /// <param name="inputStream">The stream from which to read the data to be decompressed.</param>
        /// <param name="outputStream">The stream into which the decompressed data must be written.</param>
        /// <remarks>If the input stream does not contain a valid Deflate header then the same data is returned without modification.</remarks>
        public void DecompressStream(Stream inputStream, Stream outputStream)
        {
            // test if the Deflate header is present in the first bytes.
            var header = new byte[2];
            var readLength = inputStream.Read(header, 0, 2);
            if (readLength < 2 || (header[0] != 0x78) || (header[1] != 0x01 && header[1] != 0x9c && header[1] != 0xda))
            {
                // this header is too short or the first two bytes aren't Deflate header.
                if (readLength > 0)
                {
                    outputStream.Write(header, 0, readLength);
                }

                if (readLength > 1)
                {
                    inputStream.CopyTo(outputStream);
                }

                return;
            }

            // this is a Deflate header. Decompress.
            // we will ignore the header bytes, so ther is no need to read back to the beginning.
            using (var engine = new DeflateStream(inputStream, CompressionMode.Decompress, true))
            {
                // now copy the compression engine stream to the output stream.
                engine.CopyTo(outputStream);

                engine.Flush();
            }
        }
    }
}
