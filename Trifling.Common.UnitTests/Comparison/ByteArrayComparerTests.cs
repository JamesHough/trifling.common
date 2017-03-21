namespace Trifling.Common.UnitTests.Comparison
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Trifling.Comparison;

    /// <summary>
    /// Unit Tests for the <see cref="ByteArrayComparer"/> class. 
    /// </summary>
    [TestClass]
    public class ByteArrayComparerTests
    {
        [TestMethod]
        public void ByteArrayComparerTest_WhenDefaultPropertyUsed_ThenReturnsByteArrayComparer()
        {
            // ----- Arrange -----
            // ----- Act -----
            var comparer = ByteArrayComparer.Default;

            // ----- Assert -----
            Assert.IsNotNull(comparer);
            Assert.IsInstanceOfType(comparer, typeof(ByteArrayComparer));
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenBothAreNullArrays_ThenReturnsZero()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = null;
            byte[] b = null;

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenFirstIsNullArray_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = null;
            byte[] b = new byte[0];

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenSecondIsNullArray_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[0];
            byte[] b = null;

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenBothAreEmptyArrays_ThenReturnsZero()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[0];
            byte[] b = new byte[0];

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenFirstHasMoreEntries_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 45 };
            byte[] b = new byte[0];

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenSecondHasMoreEntries_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[0];
            byte[] b = new byte[] { 120 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenFirstOfFirstHasHigherValue_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 100, 4, 5 };
            byte[] b = new byte[] { 49 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenFirstOfSecondHasHigherValue_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 100, 4, 5 };
            byte[] b = new byte[] { 149 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenFifthOfFirstHasHigherValue_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 80, 46, 119, 70, 100 };
            byte[] b = new byte[] { 80, 46, 119, 70, 49, 121, 3 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenFifthOfSecondHasHigherValue_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 80, 46, 119, 70, 100 };
            byte[] b = new byte[] { 80, 46, 119, 70, 249, 121, 3 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenAllSixMatched_ThenReturnsZero()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 80, 46, 119, 70, 100, 1 };
            byte[] b = new byte[] { 80, 46, 119, 70, 100, 1 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenFirstHasExtra_ThenReturnsNegative()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 80, 1, 9, 44, 100 };
            byte[] b = new byte[] { 80, 1, 9, 44 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenSecondHasExtra_ThenReturnsPositive()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 80, 1, 9, 44, 9 };
            byte[] b = new byte[] { 80, 1, 9, 44, 9, 3, 1 };

            // ----- Act -----
            var result = comparer.Compare(a, b);

            // ----- Assert -----
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenGetHashCode_AndByteArrayEmpty_ThenZero()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[0];

            // ----- Act -----
            var result = comparer.GetHashCode(a);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenGetHashCode_AndByteArrayNull_ThenZero()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = null;

            // ----- Act -----
            var result = comparer.GetHashCode(a);

            // ----- Assert -----
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenDifferentByteArrays_ThenHashCodesDoNotMatch()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] arrayA = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] arrayB = new byte[] { 0, 0, 0 };
            byte[] arrayC = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            byte[] arrayD = new byte[] { 255 };
            byte[] arrayE = new byte[] { 255, 255 };
            byte[] arrayF = new byte[] { 101, 201, 102, 202 };
            byte[] arrayG = new byte[] { 10, 10, 10, 10, 10 };
            byte[] arrayH = new byte[] { 9, 9, 0, 0, 0, 0, 0, 0, 0 };
            byte[] arrayI = new byte[] { 1, 1, 1, 1 };
            byte[] arrayJ = new byte[] { 67, 190, 244, 131, 4 };

            var results = new int[10];

            // ----- Act -----
            results[0] = comparer.GetHashCode(arrayA);
            results[1] = comparer.GetHashCode(arrayB);
            results[2] = comparer.GetHashCode(arrayC);
            results[3] = comparer.GetHashCode(arrayD);
            results[4] = comparer.GetHashCode(arrayE);
            results[5] = comparer.GetHashCode(arrayF);
            results[6] = comparer.GetHashCode(arrayG);
            results[7] = comparer.GetHashCode(arrayH);
            results[8] = comparer.GetHashCode(arrayI);
            results[9] = comparer.GetHashCode(arrayJ);

            // ----- Assert -----
            for (var i = 0; i < 10; i++)
            {
                for (var j = i + 1; j < 10; j++)
                {
                    Assert.AreNotEqual(results[i], results[j]);
                }
            }
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenSameByteArrays_ThenHashCodesMatch()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] arrayA1 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] arrayA2 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] arrayB1 = new byte[] { 0, 0, 0 };
            byte[] arrayB2 = new byte[] { 0, 0, 0 };
            byte[] arrayC1 = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            byte[] arrayC2 = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            byte[] arrayD1 = new byte[] { 255 };
            byte[] arrayD2 = new byte[] { 255 };
            byte[] arrayE1 = new byte[] { 67, 190, 244, 131, 4 };
            byte[] arrayE2 = new byte[] { 67, 190, 244, 131, 4 };
            byte[] arrayF1 = new byte[] { 83, 53, 33, 4, 4, 4, 4, 5, 5, 5, 156, 216, 196 };
            byte[] arrayF2 = new byte[] { 83, 53, 33, 4, 4, 4, 4, 5, 5, 5, 156, 216, 196 };

            var results = new int[12];

            // ----- Act -----
            results[0] = comparer.GetHashCode(arrayA1);
            results[1] = comparer.GetHashCode(arrayA2);
            results[2] = comparer.GetHashCode(arrayB1);
            results[3] = comparer.GetHashCode(arrayB2);
            results[4] = comparer.GetHashCode(arrayC1);
            results[5] = comparer.GetHashCode(arrayC2);
            results[6] = comparer.GetHashCode(arrayD1);
            results[7] = comparer.GetHashCode(arrayD2);
            results[8] = comparer.GetHashCode(arrayE1);
            results[9] = comparer.GetHashCode(arrayE2);
            results[10] = comparer.GetHashCode(arrayF1);
            results[11] = comparer.GetHashCode(arrayF2);

            // ----- Assert -----
            for (var i = 0; i < 12; i += 2)
            {
                Assert.AreEqual(results[i], results[i + 1]);
            }
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndBothByteArraysEmpty_ThenTrue()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[0];
            byte[] b = new byte[0];

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndBothByteArraysNull_ThenTrue()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = null;
            byte[] b = null;

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndFirstByteArrayEmpty_ThenFalse()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[0];
            byte[] b = new byte[] { 23, 24, 25 };

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndFirstByteArrayNull_ThenFalse()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = null;
            byte[] b = new byte[] { 23, 24, 25 };

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndSecondByteArrayEmpty_ThenFalse()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 199, 100, 0, 0 };
            byte[] b = new byte[0];

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndSecondByteArrayNull_ThenFalse()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 199, 100, 0, 0 };
            byte[] b = null;

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndByteArraysDifferInValues_ThenFalse()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 199, 100, 0, 0 };
            byte[] b = new byte[] { 0, 0, 100, 199 };

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndByteArraysDifferInLength_ThenFalse()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 199, 100, 0, 0 };
            byte[] b = new byte[] { 199, 100, 0 };

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ByteArrayComparerTest_WhenEquals_AndByteArraysDifferInLength2_ThenFalse()
        {
            // ----- Arrange -----
            var comparer = new ByteArrayComparer();
            byte[] a = new byte[] { 199, 100, 0 };
            byte[] b = new byte[] { 199, 100, 0, 1 };

            // ----- Act -----
            var result = comparer.Equals(a, b);

            // ----- Assert -----
            Assert.IsFalse(result);
        }
    }
}
