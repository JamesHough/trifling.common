// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE file
// </copyright>
namespace Trifling.Compression.Factory
{
    using System;
    using System.Collections.Generic;

    using Trifling.Compression.Interfaces;

    /// <summary>
    /// A factory for creating instances of compressors.
    /// </summary>
    public class CachedCompressorFactory : CompressorFactory
    {
        /// <summary>
        /// A dictionary containing previously-instantiated compressors with the configuration that
        /// they are using. This dictionary will be checked for an existing instance before creating another.
        /// </summary>
        private Dictionary<Tuple<Type, CompressorConfiguration>, ICompressor> cachedCompressors =
            new Dictionary<Tuple<Type, CompressorConfiguration>, ICompressor>();

        /// <summary>
        /// Creates a new instance of the compressor which matches the requested type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of compressor to create.</typeparam>
        /// <param name="configuration">The configuration to use when generating the instance of the compressor.</param>
        /// <returns>Returns an implementation of the compressor requested.</returns>
        public override T Create<T>(CompressorConfiguration configuration)
        {
            var cacheKey = new Tuple<Type, CompressorConfiguration>(typeof(T), configuration);
            if (this.cachedCompressors.ContainsKey(cacheKey))
            {
                // a matching compressor was found.
                return (T)this.cachedCompressors[cacheKey];
            }

            // not found, use the CompressorFactory to create one.
            var newCompressor = base.Create<T>(configuration);

            this.cachedCompressors.Add(cacheKey, newCompressor); 
            return newCompressor;
        }
    }
}
