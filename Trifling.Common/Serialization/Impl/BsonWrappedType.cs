// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE file
// </copyright>
namespace Trifling.Serialization.Impl
{
    /// <summary>
    /// A class used to wrap simple struct values as BSON-compatible objects.
    /// </summary>
    /// <typeparam name="T">The simple type that is being wrapped.</typeparam>
    internal class BsonWrappedType<T>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="BsonWrappedType{T}"/> class with the given value. 
        /// </summary>
        /// <param name="value">The value being wrapped.</param>
        public BsonWrappedType(T value)
        {
            this.Value = value;
        }
        
        /// <summary>
        /// Gets or sets the value being wrapped.
        /// </summary>
        public T Value { get; set; }
    }
}
