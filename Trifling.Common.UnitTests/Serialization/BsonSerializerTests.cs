namespace Trifling.Common.UnitTests.Serialization
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Trifling.Common.UnitTests.Internal;
    using Trifling.Serialization.Impl;

    /// <summary>
    /// Unit Tests for the <see cref="BsonSerializer"/> class. 
    /// </summary>
    [TestClass]
    public class BsonSerializerTests
    {
        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenString_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();

            // ----- Act -----
            var result = serializer.Serialize("how now brown cow?");

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenTimespan_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();

            // ----- Act -----
            var result = serializer.Serialize(TimeSpan.FromHours(33.3333d));

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenDateTimeOffset_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();

            // ----- Act -----
            var result = serializer.Serialize(new DateTimeOffset(new DateTime(2004, 10, 29, 14, 19, 36), TimeSpan.FromHours(-1d)));

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenNullableLong_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            long? nullValue = null;

            // ----- Act -----
            var result = serializer.Serialize(nullValue);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenNullableLongWithValue_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            long? value = 52329430122L;

            // ----- Act -----
            var result = serializer.Serialize(value);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenListOfIntegers_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var list = new List<int> { 21, 145, 78900, 1910 };

            // ----- Act -----
            var result = serializer.Serialize(list);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenDictioary_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var value = new Dictionary<int, string>
            {
                { 21, "a" },
                { 145, "bb" },
                { 78900, "C" },
                { 1910, "ddddddddddd" }
            };

            // ----- Act -----
            var result = serializer.Serialize(value);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenTuple_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var value = new Tuple<int, string, int, string, TimeSpan>(21, "a", 145, "ddddddddddd", TimeSpan.FromMinutes(-12.75d));

            // ----- Act -----
            var result = serializer.Serialize(value);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Serialize_WhenGivenCustomObject_ThenProducesByteArray()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var value = new CustomObjectForSerializeTests
            {
                SomeString = "ddd eee fff",
                Identifier = 998
            };

            // ----- Act -----
            var result = serializer.Serialize(value);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }

        [TestMethod]
        public void BsonSerializer_Deserialize_WhenByteArray_ThenReturnsTimeSpan()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var input = new byte[] { 35, 0, 0, 0, 2, 86, 97, 108, 117, 101, 0, 19, 0, 0, 0, 49, 46, 48, 57, 58, 49, 57, 58, 53, 57, 46, 56, 56, 48, 48, 48, 48, 48, 0, 0 };

            // ----- Act -----
            var result = serializer.Deserialize<TimeSpan>(input);

            // ----- Assert -----
            Assert.AreEqual(TimeSpan.FromHours(33.3333d), result);
        }

        [TestMethod]
        public void BsonSerializer_Deserialize_WhenByteArray_ThenReturnsString()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var input = new byte[] { 35, 0, 0, 0, 2, 86, 97, 108, 117, 101, 0, 19, 0, 0, 0, 49, 46, 48, 57, 58, 49, 57, 58, 53, 57, 46, 56, 56, 48, 48, 48, 48, 48, 0, 0 };

            // ----- Act -----
            var result = serializer.Deserialize<string>(input);

            // ----- Assert -----
            Assert.AreEqual("1.09:19:59.8800000", result);
        }

        [TestMethod]
        public void BsonSerializer_Deserialize_WhenByteArray_ThenReturnsNullableLongAsNull()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var input = new byte[] { 12, 0, 0, 0, 10, 86, 97, 108, 117, 101, 0, 0 };

            // ----- Act -----
            var result = serializer.Deserialize<long?>(input);

            // ----- Assert -----
            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void BsonSerializer_Deserialize_WhenByteArray_ThenReturnsNullableLongValue()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var input = new byte[] { 20, 0, 0, 0, 18, 86, 97, 108, 117, 101, 0, 106, 188, 19, 47, 12, 0, 0, 0, 0 };

            // ----- Act -----
            var result = serializer.Deserialize<long?>(input);

            // ----- Assert -----
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(52329430122L, result.Value);
        }

        [TestMethod]
        public void BsonSerializer_SerializeAndDeserialize_WhenGivenCustomObject_ThenRoundTripMatches()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var sourceValue = new CustomObjectForSerializeTests
            {
                SomeString = "ddd eee fff",
                Identifier = 998
            };

            // ----- Act -----
            var serializedValue = serializer.Serialize(sourceValue);
            var resultantValue = serializer.Deserialize<CustomObjectForSerializeTests>(serializedValue);

            // ----- Assert -----
            Assert.IsNotNull(resultantValue, "Deserializarion failed.");
            Assert.IsTrue(resultantValue.Equals(sourceValue), "Round-trip deserialization failed.");
        }

        [TestMethod]
        public void BsonSerializer_SerializeAndDeserialize_WhenGivenFullyPopulatedCustomObject_ThenRoundTripMatches()
        {
            // ----- Arrange -----
            var serializer = new BsonSerializer();
            var sourceValue = new CustomObjectForSerializeTests
            {
                Duration = TimeSpan.FromSeconds(944.13123),
                SomeString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec in pellentesque leo. Sed maximus vehicula odio ac tempor. Praesent quam ex, hendrerit a massa sed, tincidunt eleifend nisi. Quisque velit augue, mattis ut sodales vel, cursus sed augue. Cras vulputate ligula suscipit nibh viverra euismod. Ut in congue ante. Phasellus at rhoncus ante. Suspendisse risus metus, placerat ullamcorper porttitor ut, fringilla at elit. Fusce vitae leo sit amet purus tincidunt sodales. Quisque felis lacus, semper eu arcu in, convallis luctus lacus. Sed laoreet ligula vel quam porta, ullamcorper sagittis tortor pretium.",
                Identifier = 41817001,
                Ratio1 = 455901f / 333f,
                Ratio2 = 195d / 81821d,
                UndefinedDecimal = 53232m,
                RelatedObjects = new List<AdditionalCustomObject>
                {
                    new AdditionalCustomObject { Id = 212, MoreData = "Aliquam suscipit id erat vestibulum viverra.", RateOfReturn = 34.765349d },
                    new AdditionalCustomObject { Id = 213, MoreData = "Morbi at tristique arcu.", RateOfReturn = 0d },
                    new AdditionalCustomObject { Id = 214, MoreData = "Phasellus at rhoncus ante.", RateOfReturn = 819711.00004d },
                    new AdditionalCustomObject { Id = 215, MoreData = "In quis mi tellus.", RateOfReturn = 3d },
                }
            };

            // ----- Act -----
            var serializedValue = serializer.Serialize(sourceValue);
            var resultantValue = serializer.Deserialize<CustomObjectForSerializeTests>(serializedValue);

            // ----- Assert -----
            Assert.IsNotNull(resultantValue, "Deserializarion failed.");
            Assert.IsTrue(resultantValue.Equals(sourceValue), "Round-trip deserialization failed.");
        }
    }
}
