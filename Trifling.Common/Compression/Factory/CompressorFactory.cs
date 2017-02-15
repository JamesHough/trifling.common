// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE file
// </copyright>
namespace Trifling.Compression.Factory
{
    using System;
    using System.Collections.Generic;

    using Trifling.Compression.Impl;
    using Trifling.Compression.Interfaces;

    /// <summary>
    /// A factory for creating instances of compressors.
    /// </summary>
    public class CompressorFactory : ICompressorFactory
    {
        /// <summary>
        /// A map which describes which concrete implementations are used for each <see cref="ICompressor"/> interface. 
        /// </summary>
        private static Dictionary<Type, Type> map = new Dictionary<Type, Type>
        {
            { typeof(IDeflateCompressor), typeof(DeflateCompressor) },
            { typeof(IGzipCompressor), typeof(GzipCompressor) }
        };

        /// <summary>
        /// A map which describes which concrete implementations are used for each <see cref="ICompressor"/> interface. 
        /// </summary>
        protected static Dictionary<Type, Type> Map => map;

        /// <summary>
        /// Creates a new instance of the compressor which matches the requested type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of compressor to create.</typeparam>
        /// <param name="configuration">The configuration to use when generating the instance of the compressor.</param>
        /// <returns>Returns an implementation of the compressor requested.</returns>
        public virtual T Create<T>(CompressorConfiguration configuration) where T : ICompressor
        {
            if (!map.ContainsKey(typeof(T)))
            {
                // the requested type is not in the map - ICompressor for example is not specific enough.
                throw new InvalidOperationException("The compressor specified does not have a concrete implementation or has multiple implementations.");
            }

            // wrap the configuration parameter to pass to the constructor of the class.
            var parameter = new object[]
            {
                configuration
            };

            return (T)Activator.CreateInstance(map[typeof(T)], parameter);
        }
    }
}
