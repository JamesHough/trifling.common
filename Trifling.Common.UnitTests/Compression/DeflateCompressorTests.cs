namespace Trifling.Common.UnitTests.Compression
{
    using System.IO.Compression;
    using System.Text;

    using Microsoft.Extensions.Options;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Trifling.Compression;
    using Trifling.Compression.Impl;

    /// <summary>
    /// Unit Tests for the <see cref="DeflateCompressor"/> class. 
    /// </summary>
    [TestClass]
    public class DeflateCompressorTests
    {
        [TestMethod]
        public void DeflateCompressor_Compress_WhenInputShorterThanMinimum_ThenNoCompressionPerformed()
        {
            // ----- Arrange -----
            var configuration = new CompressorConfiguration(50, CompressionLevel.Fastest);
            var compressor = new DeflateCompressor(new OptionsWrapper<CompressorConfiguration>(configuration));

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
        public void DeflateCompressor_Compress_WhenInputEqualsMinimum_ThenCompressionPerformed()
        {
            // ----- Arrange -----
            var configuration = new CompressorConfiguration(200, CompressionLevel.Optimal);
            var compressor = new DeflateCompressor(new OptionsWrapper<CompressorConfiguration>(configuration));

            // ----- Act -----
            // uniode length of 100 characters is 200 bytes.
            var result = compressor.Compress(
                Encoding.Unicode.GetBytes(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas maximus id tellus sed turpis duis."));

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(200, result.Length);

            // confirm that the Deflate header is present in the first bytes.
            Assert.AreEqual(0x78, result[0]);
            Assert.AreEqual(0xda, result[1]);
        }

        [TestMethod]
        public void DeflateCompressor_Compress_WhenInputLongerThanMinimum_ThenCompressionPerformed()
        {
            // ----- Arrange -----
            // use default configuration which is 200 byte minimum.
            var compressor = new DeflateCompressor();

            // ----- Act -----
            // utf-8 length of 210 characters is 210 bytes.
            var result = compressor.Compress(
                Encoding.UTF8.GetBytes(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla porta ante vitae pretium cursus. Integer non velit vel nulla dictum mattis. Praesent aliquam bibendum ligula, id ultrices ante lobortis at posuere."));

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreNotEqual(200, result.Length);

            // confirm that the Deflate header is present in the first bytes.
            Assert.AreEqual(0x78, result[0]);
            Assert.AreEqual(0x9c, result[1]);
        }

        [TestMethod]
        public void DeflateCompressor_Decompress_WhenInputIsntGzipped_ThenReturnInputUnchanged()
        {
            // ----- Arrange -----
            var compressor = new DeflateCompressor();

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
        public void DeflateCompressor_Decompress_WhenInputIsGzipped_ThenReturnDecompressedData()
        {
            // ----- Arrange -----
            var compressor = new DeflateCompressor();

            // the following base-64 encoded value was generated at http://www.txtwizard.net/compression 
            var inputBytes = System.Convert.FromBase64String(
                "eJy1VkuO5DYM3fcpeADDdwgyCTBAEiQYJHuWrapmYFlufYwcP4+kVK6erLOo/tgS9fg+VL39knKIJEdpkda0pUxFKnEMdaIl7SUsNdSWiVc5pCyyPyhsUmf6utfwCJk+mhSKXArTKo9dSpFIm1Xd2lJboZvcwr62ONOflU6pHGjDo5zoeOccamaqYduwMmxB7lhLjJ2AIjvdZZcbXv3dSk0zfQsrDvtHYisTrU1oDwttDFzClFvNLU7jdHm0jbWJk7cNIIFNJqsZ2kO46j5UzlLw03EdLbcy009VONLSctFjTm2Y0Cx42eSj4feRcq0CiJMD69RdBNj/k2+6Z7AmG7BEbjiNtpDokfmUlYEF0Gb6krQR/TiVQwTCRzFaZ1TSylvA8t8zhxL26uyfnAWLrO2JCig609bqgRY7bX9gGYBTygsoa/uCZQmkLFLbCkZkpSr7ImtDTQaDjFoKHcwC31AY/LdyQCA0GYBWS6KZ6YXkV89AzgiQAL6nfaIGCuKS8gHX4IHqsc1vb1+0hSOVFnLw7R8NyJ1RZcTbc+1dJuc8rYLW/pKT4YZL1GCmGwVdY16WFgsDRMo7PIfG2mK+MsUhHDZF5TWBjasbf4tHKx+2PCr/jVfucDrPUDon+LdRWdB1drb/45JQ6hOK7Zh6QyMibgdbB0ng/PeZfsxcoDMMGcWYfqC0GQuVNSVsapozLnzEyOdMv2Eb/GtQmMIu0fz8wnMJ6PsH9TWWWlnVxRZZT/3Ay9pmOXMrjAH+ARbUbADvXm1gWHHGmNZkbsQRqnkDTQf0CFmP5YeAHEX82Ll3CaKHbpyXNvS4rOkn7OqkyTw0AqmdAfO7LJp5WWf6NeWbXDnqCcLImAx8x/QpftZZf2EnveDts0wpwfL2ndN9RChnaspP2YLvK6xfMx4ELzC86vJEVh66g/hlWlzm8vLujad/3OjDQa/h8mneIXVKPL9vb19hvGoTSOlzW55tO1rlCsdiHIsG4ecGI3fYivp+t36yE81jBj9p7vZT1aywuemp2wTULpV6zPq0AQ+BMfgxIoLl5Z1v6EVltqI4G9bSFxh1nmp1Z+h/XFa/Q2ElhgDw0Pg84EkeOrlnjMJnfJyzCqmRH73qxsBDERuj3ZDqih5dTYaJew+5T7VxNz1N5neEBW9MXK1grOAqWXCF6lnphjQ+p4thwb2ktlvlGethfh9NL6+tmvLZjQBR/18WX2f+yy2B83S12SwmH5ce3mdunV8nULWfLqr6nWUX0mhx/84Pp5wh49vBoM4Gog6JMTzGQR3q1LU2GQBJJ9OnKew5UvNf868H3W/qfod40vzq6K35N5b5X9EtYus=");

            // ----- Act -----
            var result = compressor.Decompress(inputBytes);

            // ----- Assert -----
            // the input was a known "Lorem ipsum" string which we will test here.
            var stringForm = Encoding.UTF8.GetString(result);
            Assert.AreEqual(
                "\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Integer quis massa dignissim lorem luctus bibendum. Ut vitae libero pharetra tellus eleifend auctor in finibus justo. Sed maximus, dui nec lacinia rutrum, lorem ligula convallis nisi, in feugiat lacus risus vitae purus. Etiam cursus, velit eget aliquet porttitor, justo dolor dignissim dolor, eget fringilla mauris leo gravida felis. Donec nec massa sit amet metus rutrum sodales. Praesent quis varius lorem, sed volutpat justo. Quisque orci nunc, sollicitudin id tincidunt a, laoreet facilisis massa. Suspendisse neque leo, convallis consectetur elementum non, ullamcorper non nisl.\n"
                + "\nDuis posuere consequat dolor nec varius. Sed vitae justo odio. Vivamus feugiat lectus posuere lacus accumsan, ornare faucibus purus dictum. Proin convallis purus in dapibus malesuada. Sed volutpat, eros eu scelerisque porttitor, justo est accumsan eros, vitae bibendum felis est id nibh. Cras quam mi, congue eget tortor ac, sodales malesuada ante. Nam vel porta enim, vel consequat sem. Aliquam eget nisl vel eros congue dignissim quis nec nisi. Vestibulum metus urna, commodo sed semper ut, placerat sagittis magna. Cras in posuere arcu. Proin tincidunt metus nulla, non aliquet enim vehicula id. Morbi sit amet rutrum dui, nec semper felis. Donec quis semper metus, placerat luctus eros. Duis consectetur velit vel odio sollicitudin, ut ultricies eros feugiat. Nam mattis, eros a fringilla volutpat, velit felis accumsan lectus, vitae ullamcorper ipsum elit vehicula massa.\n"
                + "\nIn et leo non justo vulputate pretium. Fusce ultrices efficitur enim a maximus. Morbi sodales arcu non quam tincidunt, ac aliquam eros auctor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Duis metus velit, congue vitae tempor et, laoreet ac lorem. Cras nec justo eget odio fermentum finibus sit amet eget ante. Quisque nec arcu suscipit, lobortis purus vitae, imperdiet tortor. Proin dictum imperdiet arcu ac accumsan.\n"
                + "\nPellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Suspendisse tincidunt ante ac elit mollis posuere. Proin tempor justo quam, sit amet varius orci dictum in. Pellentesque viverra suscipit nibh in sagittis. Proin egestas, metus eget molestie porttitor, velit leo consequat metus, eget feugiat felis lacus mollis libero.",
                stringForm);
        }

        [TestMethod]
        public void DeflateCompressor_CompressAndDecompress_WhenRoundTrip_ThenSameValueReturned()
        {
            // ----- Arrange -----
            var configuration = new CompressorConfiguration(0, CompressionLevel.Fastest);
            var compressor = new DeflateCompressor(new OptionsWrapper<CompressorConfiguration>(configuration));
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
        public void DeflateCompressor_Compress_WhenInputEmpty_ThenReturnsEmptyResult()
        {
            // ----- Arrange -----
            var configuration = new CompressorConfiguration(200, CompressionLevel.Fastest);
            var compressor = new DeflateCompressor(new OptionsWrapper<CompressorConfiguration>(configuration));
            var inputBytes = new byte[0];

            // ----- Act -----
            var compressed = compressor.Compress(inputBytes);

            // ----- Assert -----
            Assert.IsNotNull(compressed);
            Assert.AreEqual(0, compressed.Length);
        }

        [TestMethod]
        public void DeflateCompressor_Decompress_WhenInputEmpty_ThenReturnsEmptyResult()
        {
            // ----- Arrange -----
            var compressor = new DeflateCompressor();
            var inputBytes = new byte[0];

            // ----- Act -----
            var result = compressor.Decompress(inputBytes);

            // ----- Assert -----
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void DeflateCompressor_Decompress_WhenOneByteInput_ThenReturnsOneByte()
        {
            // ----- Arrange -----
            var compressor = new DeflateCompressor();
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
