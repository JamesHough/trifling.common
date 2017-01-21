namespace Trifling.Common.UnitTests.Internal
{
    using System;

    /// <summary>
    /// A custom object for testing serialize/deserialize functions on custom types.
    /// </summary>
    internal class AdditionalCustomObject : IEquatable<AdditionalCustomObject>
    {
        /// <summary>
        /// Gets or sets a string containing more data.
        /// </summary>
        public string MoreData { get; set; }

        /// <summary>
        /// Gets or sets the rate of return.
        /// </summary>
        public double RateOfReturn { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Performs equality comparison of this object and the given object.
        /// </summary>
        /// <param name="other">The object instance to which this is being compared.</param>
        /// <returns>Returns true if the objects contain the same values.</returns>
        public bool Equals(AdditionalCustomObject other)
        {
            return this.Id.Equals(other.Id)
                && this.RateOfReturn.Equals(other.RateOfReturn)
                && string.Equals(this.MoreData, other.MoreData, StringComparison.Ordinal)
                && (
                    (this.Id.HasValue && other.Id.HasValue && this.Id.Value.Equals(other.Id.Value))
                    ||
                    (!this.Id.HasValue && !other.Id.HasValue));
        }

        /// <summary>
        /// Performs equality comparison of this object and the given object.
        /// </summary>
        /// <param name="obj">The object instance to which this is being compared.</param>
        /// <returns>Returns true if the objects contain the same values.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as AdditionalCustomObject;
            return (other == null)
                ? false
                : this.Equals(other);
        }

        /// <summary>
        /// Gets a unique hash code for this object based on the values.
        /// </summary>
        /// <returns>Returns a hash code derived from the instance values.</returns>
        public override int GetHashCode()
        {
            const int Prime = 37;
            unchecked
            {
                var hash = string.IsNullOrEmpty(this.MoreData) ? 0x0114 : this.MoreData.GetHashCode();
                hash = (hash * Prime) + this.Id.GetHashCode();
                hash = (hash * Prime) + this.RateOfReturn.GetHashCode();

                return hash;
            }
        }
    }
}
