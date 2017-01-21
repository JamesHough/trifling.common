namespace Trifling.Common.UnitTests.Compression
{
    using System.IO.Compression;
    using System.Text;

    using Microsoft.Extensions.Options;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Trifling.Compression;
    using Trifling.Compression.Impl;

    /// <summary>
    /// Unit Tests for the <see cref="GzipCompressor"/> class. 
    /// </summary>
    [TestClass]
    public class GzipCompressorTests
    {
        [TestMethod]
        public void GzipCompressor_Compress_WhenInputShorterThanMinimum_ThenNoCompressionPerformed()
        {
            // ----- Arrange -----
            var configuration = new CompressorConfiguration(50, CompressionLevel.Fastest);
            var compressor = new GzipCompressor(new OptionsWrapper<CompressorConfiguration>(configuration));

            // ----- Act -----
            var result = compressor.Compress(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 });

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreEqual(26, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(6, result[25]);
        }

        [TestMethod]
        public void GzipCompressor_Compress_WhenInputEqualsMinimum_ThenCompressionPerformed()
        {
            // ----- Arrange -----
            var configuration = new CompressorConfiguration(200, CompressionLevel.Fastest);
            var compressor = new GzipCompressor(new OptionsWrapper<CompressorConfiguration>(configuration));

            // ----- Act -----
            // uniode length of 100 characters is 200 bytes.
            var result = compressor.Compress(
                Encoding.Unicode.GetBytes(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas maximus id tellus sed turpis duis."));

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(200, result.Length);

            // confirm that the G-Zip header is present in the first bytes.
            Assert.AreEqual(0x1f, result[0]);
            Assert.AreEqual(0x8b, result[1]);
        }

        [TestMethod]
        public void GzipCompressor_Compress_WhenInputLongerThanMinimum_ThenCompressionPerformed()
        {
            // ----- Arrange -----
            // use default configuration which is 200 byte minimum.
            var compressor = new GzipCompressor();

            // ----- Act -----
            // utf-8 length of 210 characters is 210 bytes.
            var result = compressor.Compress(
                Encoding.UTF8.GetBytes(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla porta ante vitae pretium cursus. Integer non velit vel nulla dictum mattis. Praesent aliquam bibendum ligula, id ultrices ante lobortis at posuere."));

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(200, result.Length);

            // confirm that the G-Zip header is present in the first bytes.
            Assert.AreEqual(0x1f, result[0]);
            Assert.AreEqual(0x8b, result[1]);
        }

        [TestMethod]
        public void GzipCompressor_Decompress_WhenInputIsntGzipped_ThenReturnInputUnchanged()
        {
            // ----- Arrange -----
            var compressor = new GzipCompressor();

            // ----- Act -----
            var result = compressor.Decompress(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 });

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreEqual(26, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(6, result[25]);
        }

        [TestMethod]
        public void GzipCompressor_Decompress_WhenInputIsGzipped_ThenReturnDecompressedData()
        {
            // ----- Arrange -----
            var compressor = new GzipCompressor();

            // the following base-64 encoded value was generated in another program.
            var inputBytes = System.Convert.FromBase64String(
                "H4sIAAAAAAAAC7SWXW7kNhCE3w34Dn0AQXdY7CbAAsligyB575E4416QoswfIcdPdZOUxpvnPNiGRxRZXfV1c15ffovJBZI910Br9DFRlkIcXJloiVt2S3GlJuJVdsmLbA9yXspMX7fiHi7Re5VMgXNmWuWxSc4SyNuuvi6lZrrJzW1rDTP9VeiQwo48PkqR9jdOriSm4rzHSued3LGWGG9Cimx0l01uePSj5hJn+tOtOOwfCTVPtFahzS3kGbqEKdWSapjG6fKonrWIg72HSGiTyfZ09SFc9D3snCTjd9O111TzTL8U4UBLTXgy0aEFE4qFL17eK/7uMZUikDg1Yd26ywD7f2ov3RNcEw8tgStOI+8iPRIfsjK0QNpMX6IWoj/NyhEC4Uc1WmWU48reYfn3xC67rTT3D06CRVb2RBkWHdHXsqPEbtsfWAbhFNMCy+q2YFmEKYuUusIRWanItshasSfDQcZeKh3OQt9IGP7XvCMgFOmgVrdEMdOTyc/MIM4AkRC+xW2iCgvCEtMOavCB5uHn15fXly9axB5zdcm1Dd4rtDdP1ZNWYEu/BdVcj6uguL/lYPBwxYrz67VhS5mXpYbMkBHTBupQWl2MLMsc0eElIPo9Rfhx1dOe4qOVd1seNIHKq5rx5DSyThEEV8oL6kbMas5/OHG5nFLsjakXNJqkAWHrEArYf5vpc+KMpIFkAMDQ9sDWhhZ21j5hy9PYuPQRo0Nn+obXQLBJYXKbABH9//I5O9T9ScnGUttWk7FFVlM/8ILboDNegQb8h1hY4yG+0VrhsOoMIa7ReMQRmnqFTTvycEmP5YfAHFX82LhXCaNHbpyWOvK44GwnbMrSZBSNltTKoPlNFu16WWf6PaabXJ3UewhDYzLxXdOHBrTK+gM76Ulvn2ZqCZbXn1hvQ0I9Uyg/dBfIL4C/JHyAgMzTzmqLJ7D60Anip3lxwdW2b2yc/DTQB0HP7dXmeZfULWkdrP32Fehpm0QzsIF5VL/XwgXMYiSLtsKvFSh34ar7freKUrOaxxw+je4Aam62sfF0JjdBdwtLKbNKbcgjYgx/jAlnHfPGN1SjQdumOBtw6QOMu9bXyqdBoNPxhP2OjNUagkBcVMpxLjySatSYiWcDNdcKwkYH6XU3hh42sVHakVQuevNqb1i8d5f6ZBv304lZuyes9cbU1R3MFVwnC65RPSve0I/nfDEtuJsUvFXOxh74t+H09Nh2Uz87Chbr/+vj8+Qv112B83S1oRYU+nPwnr3bHG4WavqYVcOsfnPZtTSK3H4i4pDDJXxHGObZUNRBMQbIOKhLnXraFgQk6XT6MIlbLyn+1wzszd7u636PtG5r10cvrX1vmf8FAAD//wMAYARMOTcJAAA=");

            // ----- Act -----
            var result = compressor.Decompress(inputBytes);

            // ----- Assert -----
            // the input was a known "Lorem ipsum" string which we will test here.
            var stringForm = Encoding.UTF8.GetString(result);
            Assert.AreEqual(
                "\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Integer quis massa dignissim lorem luctus bibendum. Ut vitae libero pharetra tellus eleifend auctor in finibus justo. Sed maximus, dui nec lacinia rutrum, lorem ligula convallis nisi, in feugiat lacus risus vitae purus. Etiam cursus, velit eget aliquet porttitor, justo dolor dignissim dolor, eget fringilla mauris leo gravida felis. Donec nec massa sit amet metus rutrum sodales. Praesent quis varius lorem, sed volutpat justo. Quisque orci nunc, sollicitudin id tincidunt a, laoreet facilisis massa. Suspendisse neque leo, convallis consectetur elementum non, ullamcorper non nisl.\r\n"
                + "\r\nDuis posuere consequat dolor nec varius. Sed vitae justo odio. Vivamus feugiat lectus posuere lacus accumsan, ornare faucibus purus dictum. Proin convallis purus in dapibus malesuada. Sed volutpat, eros eu scelerisque porttitor, justo est accumsan eros, vitae bibendum felis est id nibh. Cras quam mi, congue eget tortor ac, sodales malesuada ante. Nam vel porta enim, vel consequat sem. Aliquam eget nisl vel eros congue dignissim quis nec nisi. Vestibulum metus urna, commodo sed semper ut, placerat sagittis magna. Cras in posuere arcu. Proin tincidunt metus nulla, non aliquet enim vehicula id. Morbi sit amet rutrum dui, nec semper felis. Donec quis semper metus, placerat luctus eros. Duis consectetur velit vel odio sollicitudin, ut ultricies eros feugiat. Nam mattis, eros a fringilla volutpat, velit felis accumsan lectus, vitae ullamcorper ipsum elit vehicula massa.\r\n"
                + "\r\nIn et leo non justo vulputate pretium. Fusce ultrices efficitur enim a maximus. Morbi sodales arcu non quam tincidunt, ac aliquam eros auctor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Duis metus velit, congue vitae tempor et, laoreet ac lorem. Cras nec justo eget odio fermentum finibus sit amet eget ante. Quisque nec arcu suscipit, lobortis purus vitae, imperdiet tortor. Proin dictum imperdiet arcu ac accumsan.\r\n"
                + "\r\nPellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Suspendisse tincidunt ante ac elit mollis posuere. Proin tempor justo quam, sit amet varius orci dictum in. Pellentesque viverra suscipit nibh in sagittis. Proin egestas, metus eget molestie porttitor, velit leo consequat metus, eget feugiat felis lacus mollis libero.",
                stringForm);
        }

        [TestMethod]
        public void GzipCompressor_CompressAndDecompress_WhenRoundTrip_ThenSameValueReturned()
        {
            // ----- Arrange -----
            var configuration = new CompressorConfiguration(0, CompressionLevel.Fastest);
            var compressor = new GzipCompressor(new OptionsWrapper<CompressorConfiguration>(configuration));
            var expectedBytes = new byte[]
            {
                10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6,
                10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6,
                10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6,
                10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6,
                10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6,
                10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6
            };

            // ----- Act -----
            var compressed = compressor.Compress(expectedBytes);
            var result = compressor.Decompress(compressed);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedBytes.Length, result.Length);
            for (var i = 0; i < expectedBytes.Length; i++)
            {
                Assert.AreEqual(expectedBytes[i], result[i]);
            }
        }

        [TestMethod]
        public void GzipCompressor_Compress_WhenInputEmpty_ThenReturnsEmptyResult()
        {
            // ----- Arrange -----
            var configuration = new CompressorConfiguration(200, CompressionLevel.Fastest);
            var compressor = new GzipCompressor(new OptionsWrapper<CompressorConfiguration>(configuration));
            var inputBytes = new byte[0];

            // ----- Act -----
            var compressed = compressor.Compress(inputBytes);

            // ----- Assert -----
            Assert.IsNotNull(compressed);
            Assert.AreEqual(0, compressed.Length);
        }

        [TestMethod]
        public void GzipCompressor_Decompress_WhenInputEmpty_ThenReturnsEmptyResult()
        {
            // ----- Arrange -----
            var compressor = new GzipCompressor();
            var inputBytes = new byte[0];

            // ----- Act -----
            var result = compressor.Decompress(inputBytes);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GzipCompressor_Decompress_WhenOneByteInput_ThenReturnsOneByte()
        {
            // ----- Arrange -----
            var compressor = new GzipCompressor();
            var inputBytes = new byte[] { 99 };

            // ----- Act -----
            var result = compressor.Decompress(inputBytes);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(99, result[0]);
        }
    }
}
