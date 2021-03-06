﻿namespace Trifling.Common.UnitTests.Compression
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Trifling.Compression;
    using Trifling.Compression.Factory;
    using Trifling.Compression.Impl;
    using Trifling.Compression.Interfaces;

    /// <summary>
    /// Unit Tests for the <see cref="CompressorFactory"/> class. 
    /// </summary>
    [TestClass]
    public class CompressorFactoryTests
    {
        [TestMethod]
        public void CompressorFactory_Create_WhenIGzipCompressor_ThenReturnsGzipCompressor()
        {
            // ----- Arrange -----
            var factory = new CompressorFactory();
            var configuration = new CompressorConfiguration();

            // ----- Act -----
            var instance = factory.Create<IGzipCompressor>(configuration);

            // ----- Assert -----
            Assert.IsNotNull(instance, "no implementation was generated.");
            Assert.IsInstanceOfType(instance, typeof(GzipCompressor), "the wrong type was generated by the factory.");
        }

        [TestMethod]
        public void CompressorFactory_Create_WhenIDeflateCompressor_ThenReturnsDeflateCompressor()
        {
            // ----- Arrange -----
            var factory = new CompressorFactory();
            var configuration = new CompressorConfiguration();

            // ----- Act -----
            var instance = factory.Create<IDeflateCompressor>(configuration);

            // ----- Assert -----
            Assert.IsNotNull(instance, "no implementation was generated.");
            Assert.IsInstanceOfType(instance, typeof(DeflateCompressor), "the wrong type was generated by the factory.");
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void CompressorFactory_Create_WhenICompressor_ThenThrowsException()
        {
            // ----- Arrange -----
            var factory = new CompressorFactory();
            var configuration = new CompressorConfiguration();

            // ----- Act -----
            var instance = factory.Create<ICompressor>(configuration);

            // ----- Assert -----
            Assert.Fail("The expected exception was not thrown.");
        }
    }
}
