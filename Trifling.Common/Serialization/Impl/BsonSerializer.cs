// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE file
// </copyright>
namespace Trifling.Serialization.Impl
{
    using System;
    using System.IO;
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    using Trifling.Serialization.Interfaces;

    /// <summary>
    /// An implementation of a binary serialiser which uses Newtonsoft to create BSON data.
    /// </summary>
    public class BsonSerializer : IBinarySerializer
    {
        /// <summary>
        /// Converts the given bytes to a value of type <typeparamref name="T"/> (assuming that the data is BSON compatible).
        /// </summary>
        /// <typeparam name="T">The expected type that should be returned from the given byte data.</typeparam>
        /// <param name="value">The byte data that should be decoded as BSON into an instance of type <typeparamref name="T"/>.</param>
        /// <returns>Returns the de-serialised value.</returns>
        public T Deserialize<T>(byte[] value)
        {
            using (var memoryStream = new MemoryStream(value))
            {
                return this.DeserializeFromStream<T>(memoryStream);
            }
        }

        /// <summary>
        /// Converts the given object instance into a BSON compatible byte array.
        /// </summary>
        /// <typeparam name="T">The object type that has been given.</typeparam>
        /// <param name="value">The object instance that should be encoded as BSON data.</param>
        /// <returns>Returns the serialised value as a byte array.</returns>
        public byte[] Serialize<T>(T value)
        {
            using (var memoryStream = new MemoryStream())
            {
                this.SerializeToStream<T>(value, memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Converts the given object instance into a byte array and writes the output to the given stream.
        /// </summary>
        /// <typeparam name="T">The type of object that has been given.</typeparam>
        /// <param name="value">The object instance that should be encoded and written to the <paramref name="outputStream"/>.</param>
        /// <param name="outputStream">The stream into which the output should be written.</param>
        public void SerializeToStream<T>(T value, Stream outputStream)
        {
            using (var bsonWriter = new BsonWriter(outputStream))
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(bsonWriter, MakeBsonCompatibleObject(value), typeof(T));
            }
        }

        /// <summary>
        /// Reads bytes from the <paramref name="inputStream"/> and converts the data to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected type that should be read from the input stream.</typeparam>
        /// <param name="inputStream">The stream from where the source of bytes should be read.</param>
        /// <returns>Returns the de-serialised value.</returns>
        public T DeserializeFromStream<T>(Stream inputStream)
        {
            using (var bsonReader = new BsonReader(inputStream))
            {
                var serializer = new JsonSerializer();
                return BsonSerializer.TypeIsWrapped(typeof(T))
                    ? serializer.Deserialize<BsonWrappedType<T>>(bsonReader).Value
                    : serializer.Deserialize<T>(bsonReader);
            }
        }

        #region private methods

        /// <summary>
        /// Determines whether or not the type provided is simple and thus cannot be handled by the 
        /// normal BSON serialisation rules which require the root to define an array or an object.
        /// </summary>
        /// <param name="type">The type to check for compatibility with BSON.</param>
        /// <returns>Returns true if the type must be wrapped before it can be serialised to BSON.</returns>
        private static bool TypeIsWrapped(Type type)
        {
            // all instances of the nullable type must be wrapped. 
            if (type.FullName.StartsWith("System.Nullable`", StringComparison.Ordinal))
            {
                return true;
            }

            // determine if it is a type which requires wrapping.
            if (string.Equals(type.FullName, "System.String", StringComparison.Ordinal))
            {
                return true;
            }

            // we will have to assume that this type will only need wrapping if it's a struct.
            var ti = type.GetTypeInfo();
            return ti.IsValueType || ti.IsEnum;
        }

        /// <summary>
        /// Converts the given value to a value that can be serialised to BSON.
        /// </summary>
        /// <typeparam name="T">The type of the value that is being serialised.</typeparam>
        /// <param name="value">The value which will be serialised.</param>
        /// <returns>Returns an object that can be serialised to BSON.</returns>
        private static object MakeBsonCompatibleObject<T>(T value)
        {
            return BsonSerializer.TypeIsWrapped(typeof(T))
                ? new BsonWrappedType<T>(value)
                : (object)value;
        }

        #endregion private methods
    }
}
