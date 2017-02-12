namespace Trifling.Common.UnitTests.Comparison
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Trifling.Comparison;

    /// <summary>
    /// Unit Tests for the <see cref="BoxedByteArrayComparer"/> class. 
    /// </summary>
    [TestClass]
    public class BoxedByteArrayComparerTests
    {
        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenDefaultPropertyUsed_ThenReturnsByteArrayComparer()
        {
            // ----- Arrange -----
            // ----- Act -----
            var comparer = BoxedByteArrayComparer.Default;

            // ----- Assert -----
            Assert.IsNotNull(comparer);
            Assert.IsInstanceOfType(comparer, typeof(BoxedByteArrayComparer));
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenComparingNonArrays_ThenReturnsZero()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();

            // ----- Act -----
            var result = comparer.Compare("78", 123);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenBothAreNullArrays_ThenReturnsZero()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = null;
            byte[] b = null;

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenFirstIsNullArray_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = null;
            byte[] b = new byte[0];

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenSecondIsNullArray_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[0];
            byte[] b = null;

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenBothAreEmptyArrays_ThenReturnsZero()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[0];
            byte[] b = new byte[0];

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenFirstHasMoreEntries_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[] { 45 };
            byte[] b = new byte[0];

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenSecondHasMoreEntries_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[0];
            byte[] b = new byte[] { 120 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenFirstOfFirstHasHigherValue_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[] { 100, 4, 5 };
            byte[] b = new byte[] { 49 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenFirstOfSecondHasHigherValue_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[] { 100, 4, 5 };
            byte[] b = new byte[] { 149 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenFifthOfFirstHasHigherValue_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[] { 80, 46, 119, 70, 100 };
            byte[] b = new byte[] { 80, 46, 119, 70, 49, 121, 3 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenFifthOfSecondHasHigherValue_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[] { 80, 46, 119, 70, 100 };
            byte[] b = new byte[] { 80, 46, 119, 70, 249, 121, 3 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenAllSixMatched_ThenReturnsZero()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[] { 80, 46, 119, 70, 100, 1 };
            byte[] b = new byte[] { 80, 46, 119, 70, 100, 1 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenFirstHasExtra_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[] { 80, 1, 9, 44, 100 };
            byte[] b = new byte[] { 80, 1, 9, 44 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void BoxedByteArrayComparerTest_WhenSecondHasExtra_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new BoxedByteArrayComparer();
            byte[] a = new byte[] { 80, 1, 9, 44, 9 };
            byte[] b = new byte[] { 80, 1, 9, 44, 9, 3, 1 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }
    }
}
