// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE.md
// </copyright>
namespace Trifling.Common.UnitTests.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A custom object for testing serialize/deserialize functions on custom types.
    /// </summary>
    internal class CustomObjectForSerializeTests : IEquatable<CustomObjectForSerializeTests>
    {
        /// <summary>
        /// Gets or sets some string.
        /// </summary>
        public string SomeString { get; set; }

        /// <summary>
        /// Gets or sets an identifier.
        /// </summary>
        public int Identifier { get; set; }

        /// <summary>
        /// Gets or sets a duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets a list of related objects.
        /// </summary>
        public List<AdditionalCustomObject> RelatedObjects { get; set; }

        /// <summary>
        /// Gets or sets a float ratio.
        /// </summary>
        public float Ratio1 { get; set; }

        /// <summary>
        /// Gets or sets a double ratio.
        /// </summary>
        public double Ratio2 { get; set; }

        /// <summary>
        /// Gets or sers a (possibly undefined) decimal.
        /// </summary>
        public decimal? UndefinedDecimal { get; set; }

        /// <summary>
        /// Performs equality comparison of this object and the given object.
        /// </summary>
        /// <param name="other">The object instance to which this is being compared.</param>
        /// <returns>Returns true if the objects contain the same values.</returns>
        public bool Equals(CustomObjectForSerializeTests other)
        {
            return this.Duration.Equals(other.Duration)
                && this.Identifier.Equals(other.Identifier)
                && this.Ratio1.Equals(other.Ratio1)
                && this.Ratio2.Equals(other.Ratio2)
                && string.Equals(this.SomeString, other.SomeString)
                && (
                    (this.RelatedObjects != null && other.RelatedObjects != null && this.RelatedObjects.All(t => other.RelatedObjects.Any(o => t.Equals(o))))
                    ||
                    (this.RelatedObjects == null && other.RelatedObjects == null))
                && (
                    (this.UndefinedDecimal.HasValue && other.UndefinedDecimal.HasValue && this.UndefinedDecimal.Value.Equals(other.UndefinedDecimal.Value))
                    ||
                    (!this.UndefinedDecimal.HasValue && !other.UndefinedDecimal.HasValue));
        }

        /// <summary>
        /// Performs equality comparison of this object and the given object.
        /// </summary>
        /// <param name="obj">The object instance to which this is being compared.</param>
        /// <returns>Returns true if the objects contain the same values.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as CustomObjectForSerializeTests;

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
            const int Prime = 23;
            unchecked
            {
                var hash = string.IsNullOrEmpty(this.SomeString) ? 0x0114 : this.SomeString.GetHashCode();
                hash = (hash * Prime) + this.Identifier.GetHashCode();
                hash = (hash * Prime) + this.Ratio1.GetHashCode();
                hash = (hash * Prime) + this.Ratio2.GetHashCode();
                hash = (hash * Prime) + this.Duration.GetHashCode();

                if (this.RelatedObjects != null)
                {
                    hash = (hash * Prime) + this.RelatedObjects.Count;
                    for (var i = 0; i < this.RelatedObjects.Count; i++)
                    {
                        hash = (hash * Prime) + this.RelatedObjects[i].GetHashCode();
                    }
                }

                return hash;
            }
        }
    }
}
