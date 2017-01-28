namespace Trifling.Common.UnitTests.Logging
{
    using System.Globalization;
    using System.Text;

    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Trifling.Common.UnitTests.Internal;
    using Trifling.Logging;

    [TestClass]
    public class LoggingTextWriterTests
    {
        [TestMethod]
        public void LoggingTextWriter_FormatProvider_IsInvariantCulture()
        {
            // ----- Arrange -----
            var loggerMoq = new Mock<ILogger>();
            var loggingTextWriter = new LoggingTextWriter(loggerMoq.Object);

            // ----- Act -----
            // ----- Assert -----
            Assert.AreEqual(
                CultureInfo.InvariantCulture, 
                loggingTextWriter.FormatProvider, 
                "Log entries will have inconsistent formatting depending on the running environment.");
        }

        [TestMethod]
        public void LoggingTextWriter_Encoding_IsUnicode()
        {
            // ----- Arrange -----
            var loggerMoq = new Mock<ILogger>();
            var loggingTextWriter = new LoggingTextWriter(loggerMoq.Object);

            // ----- Act -----
            // ----- Assert -----
            Assert.AreEqual(
                Encoding.Unicode,
                loggingTextWriter.Encoding,
                "Logging must support Unicode.");
        }

        [TestMethod]
        public void LoggingTextWriter_Write_WhenOnlyNewlines_ThenNoLoggingOccurs()
        {
            // ----- Arrange -----
            // by using Strict mode, if ANY undefined methods on this mock are called, then an 
            // exception occurs.
            var loggerMoq = new Mock<ILogger>(MockBehavior.Strict);

            var loggingTextWriter = new LoggingTextWriter(loggerMoq.Object);

            // ----- Act -----
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\n');
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\n');
            loggingTextWriter.Write('\r');

            // ----- Assert -----
            // if we reach this point then the loggerMoq was never used (or else there would have
            // been an exception.
            Assert.IsTrue(true); 
        }

        [TestMethod]
        public void LoggingTextWriter_Write_WhenLevelIsNone_ThenNoLoggingOccurs()
        {
            // ----- Arrange -----
            // by using Strict mode, if ANY undefined methods on this mock are called, then an 
            // exception occurs.
            var loggerMoq = new Mock<ILogger>(MockBehavior.Strict);

            var loggingTextWriter = new LoggingTextWriter(loggerMoq.Object, LogLevel.None);

            // ----- Act -----
            loggingTextWriter.Write('t');
            loggingTextWriter.Write('h');
            loggingTextWriter.Write('e');
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\n');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('r');
            loggingTextWriter.Write('m');
            loggingTextWriter.Write('!');
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\n');

            // ----- Assert -----
            // if we reach this point then the loggerMoq was never used (or else there would have
            // been an exception.
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void LoggingTextWriter_Write_WhenLevelIsTrace_AndWriteStringEndingWithNewline_ThenStringLogged()
        {
            // ----- Arrange -----
            var logger = new LoggingMock();

            var loggingTextWriter = new LoggingTextWriter(logger);

            // ----- Act -----
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('h');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write('n');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write('b');
            loggingTextWriter.Write('r');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write('n');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write('c');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write('?');
            loggingTextWriter.Write('\n');
            loggingTextWriter.Write('t');
            loggingTextWriter.Write('h');
            loggingTextWriter.Write('e');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write('e');
            loggingTextWriter.Write('n');
            loggingTextWriter.Write('d');
            loggingTextWriter.Write('.');
            loggingTextWriter.Write('\r');

            // ----- Assert -----
            Assert.IsTrue(logger.CallHappened("how now brown cow?", LogLevel.Trace));
            Assert.IsTrue(logger.CallHappened("the end.", LogLevel.Trace));
        }

        [TestMethod]
        public void LoggingTextWriter_Write_WhenLevelIsInfo_AndWriteStringEndingWithNewline_ThenStringLogged()
        {
            // ----- Arrange -----
            var logger = new LoggingMock();

            var loggingTextWriter = new LoggingTextWriter(logger, LogLevel.Information);

            // ----- Act -----
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('\r');
            loggingTextWriter.Write('h');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write('n');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write('b');
            loggingTextWriter.Write('r');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write('n');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write('c');
            loggingTextWriter.Write('o');
            loggingTextWriter.Write('w');
            loggingTextWriter.Write('?');
            loggingTextWriter.Write('\n');
            loggingTextWriter.Write('t');
            loggingTextWriter.Write('h');
            loggingTextWriter.Write('e');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write('e');
            loggingTextWriter.Write('n');
            loggingTextWriter.Write('d');
            loggingTextWriter.Write('.');
            loggingTextWriter.Write('\r');

            // ----- Assert -----
            Assert.IsTrue(logger.CallHappened("how now brown cow?", LogLevel.Information));
            Assert.IsTrue(logger.CallHappened("the end.", LogLevel.Information));
        }

        [TestMethod]
        public void LoggingTextWriter_Write_WhenMixOfStringsAndIntegersWritten_ThenStringLoggedOnNewline()
        {
            // ----- Arrange -----
            var logger = new LoggingMock();

            var loggingTextWriter = new LoggingTextWriter(logger);

            // ----- Act -----
            loggingTextWriter.Write("the tree was ");
            loggingTextWriter.Write(45);
            loggingTextWriter.WriteLine(" years old");

            // ----- Assert -----
            Assert.IsTrue(logger.CallHappened("the tree was 45 years old"));
        }

        [TestMethod]
        public void LoggingTextWriter_Write_WhenMixOfFormattedStringsAndBooleansWritten_ThenStringLoggedOnNewline()
        {
            // ----- Arrange -----
            var logger = new LoggingMock();

            var loggingTextWriter = new LoggingTextWriter(logger);

            // ----- Act -----
            loggingTextWriter.Write("the tree was {0} years old: ", 45);
            loggingTextWriter.WriteLine(true);

            // ----- Assert -----
            Assert.IsTrue(logger.CallHappened("the tree was 45 years old: True"));
        }

        [TestMethod]
        public void LoggingTextWriter_WriteLine_WhenWriteLineAlone_ThenNoLoggingOccurs()
        {
            // ----- Arrange -----
            // by using Strict mode, if ANY undefined methods on this mock are called, then an 
            // exception occurs.
            var loggerMoq = new Mock<ILogger>(MockBehavior.Strict);

            var loggingTextWriter = new LoggingTextWriter(loggerMoq.Object);

            // ----- Act -----
            loggingTextWriter.WriteLine();
            loggingTextWriter.WriteLine();
            loggingTextWriter.WriteLine();

            // ----- Assert -----
            // if we reach this point then the loggerMoq was never used (or else there would have
            // been an exception.
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void LoggingTextWriter_Write_WhenValuesBuffered_AndCharNewlineWritten_ThenStringLogged()
        {
            // ----- Arrange -----
            var logger = new LoggingMock();

            var loggingTextWriter = new LoggingTextWriter(logger);

            // ----- Act -----
            loggingTextWriter.Write("abcdef");
            loggingTextWriter.Write('\n');
            loggingTextWriter.Write(21);
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write(49.3d);
            loggingTextWriter.Write(string.Empty);
            loggingTextWriter.Write('\n');

            // ----- Assert -----
            Assert.IsTrue(logger.CallHappened("abcdef"));
            Assert.IsTrue(logger.CallHappened("21 49.3"));
        }

        [TestMethod]
        public void LoggingTextWriter_WriteAsync_WhenAsynchronousCalls_ThenStringLoggedOnNewline()
        {
            // ----- Arrange -----
            var logger = new LoggingMock();

            var loggingTextWriter = new LoggingTextWriter(logger);

            // ----- Act -----
            loggingTextWriter.WriteAsync("the broom handle must be replaced").Wait();
            loggingTextWriter.WriteLineAsync().Wait();

            // ----- Assert -----
            Assert.IsTrue(logger.CallHappened("the broom handle must be replaced"));
        }

        [TestMethod]
        public void LoggingTextWriter_WriteLineAsync_WhenMultipleValuesBuffered_ThenStringLoggedOnNewline()
        {
            // ----- Arrange -----
            var logger = new LoggingMock();

            var loggingTextWriter = new LoggingTextWriter(logger);

            // ----- Act -----
            loggingTextWriter.Write('t');
            loggingTextWriter.Write('h');
            loggingTextWriter.Write('e');
            loggingTextWriter.Write(' ');
            loggingTextWriter.Write("jug holds ");
            loggingTextWriter.Write(660f);
            loggingTextWriter.Write(" millilitres.");
            loggingTextWriter.WriteLineAsync().Wait();

            // ----- Assert -----
            Assert.IsTrue(logger.CallHappened("the jug holds 660 millilitres."));
        }
    }
}
