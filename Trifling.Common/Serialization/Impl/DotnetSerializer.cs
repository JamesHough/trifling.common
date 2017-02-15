// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE file
// </copyright>
namespace Trifling.Serialization.Impl
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters;

    using Trifling.Serialization.Interfaces;

    /// <summary>
    /// This class has not been implemented because NetStandard 1.6 does not support this operation.
    /// </summary>
    public class DotnetSerializer : IBinarySerializer
    {
        /// <summary>
        /// Converts the given bytes to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected type that should be returned from the given byte array.</typeparam>
        /// <param name="value">The byte data that should be decoded as an instance of an object of type <typeparamref name="T"/>.</param>
        /// <returns>Returns the de-serialised value.</returns>
        /// <exception cref="NotImplementedException">This exception is always returned.</exception>
        public T Deserialize<T>(byte[] value)
        {
            throw new NotImplementedException("This feature requires dotnet library components that are not yet available.");
        }

        /// <summary>
        /// Reads bytes from the <paramref name="inputStream"/> and converts the data to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected type that should be read from the input stream.</typeparam>
        /// <param name="inputStream">The stream from where the source of bytes should be read.</param>
        /// <returns>Returns the de-serialised value.</returns>
        /// <exception cref="NotImplementedException">This exception is always returned.</exception>
        public T DeserializeFromStream<T>(Stream inputStream)
        {
            throw new NotImplementedException("This feature requires dotnet library components that are not yet available.");
        }

        /// <summary>
        /// Converts the given object instance into a byte array.
        /// </summary>
        /// <typeparam name="T">The type of object that has been given.</typeparam>
        /// <param name="value">The object instance that should be encoded in a byte array.</param>
        /// <returns>Returns the serialised value as a byte array.</returns>
        /// <exception cref="NotImplementedException">This exception is always returned.</exception>
        public byte[] Serialize<T>(T value)
        {
            throw new NotImplementedException("This feature requires dotnet library components that are not yet available.");
        }

        /// <summary>
        /// Converts the given object instance into a byte array and writes the output to the given stream.
        /// </summary>
        /// <typeparam name="T">The type of object that has been given.</typeparam>
        /// <param name="value">The object instance that should be encoded and written to the <paramref name="outputStream"/>.</param>
        /// <param name="outputStream">The stream into which the output should be written.</param>
        /// <exception cref="NotImplementedException">This exception is always returned.</exception>
        public void SerializeToStream<T>(T value, Stream outputStream)
        {
            throw new NotImplementedException("This feature requires dotnet library components that are not yet available.");
        }
    }
}
