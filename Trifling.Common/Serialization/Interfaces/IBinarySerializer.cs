// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE file
// </copyright>
namespace Trifling.Serialization.Interfaces
{
    using System.IO;

    /// <summary>
    /// A binary serialiser to convert values to byte arrays.
    /// </summary>
    public interface IBinarySerializer
    {
        /// <summary>
        /// Converts the given object instance into a byte array.
        /// </summary>
        /// <typeparam name="T">The type of object that has been given.</typeparam>
        /// <param name="value">The object instance that should be encoded in a byte array.</param>
        /// <returns>Returns the serialised value as a byte array.</returns>
        byte[] Serialize<T>(T value);

        /// <summary>
        /// Converts the given bytes to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected type that should be returned from the given byte array.</typeparam>
        /// <param name="value">The byte data that should be decoded as an instance of an object of type <typeparamref name="T"/>.</param>
        /// <returns>Returns the de-serialised value.</returns>
        T Deserialize<T>(byte[] value);

        /// <summary>
        /// Converts the given object instance into a byte array and writes the output to the given stream.
        /// </summary>
        /// <typeparam name="T">The type of object that has been given.</typeparam>
        /// <param name="value">The object instance that should be encoded and written to the <paramref name="outputStream"/>.</param>
        /// <param name="outputStream">The stream into which the output should be written.</param>
        void SerializeToStream<T>(T value, Stream outputStream);

        /// <summary>
        /// Reads bytes from the <paramref name="inputStream"/> and converts the data to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected type that should be read from the input stream.</typeparam>
        /// <param name="inputStream">The stream from where the source of bytes should be read.</param>
        /// <returns>Returns the de-serialised value.</returns>
        T DeserializeFromStream<T>(Stream inputStream);
    }
}
