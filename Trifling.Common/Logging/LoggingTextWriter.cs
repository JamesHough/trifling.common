// <copyright company="James Hough">
//   Copyright (c) James Hough. Licensed under MIT License - refer to LICENSE.md
// </copyright>
namespace Trifling.Logging
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides a <see cref="TextWriter"/> to write text to a <see cref="ILogger"/>. 
    /// </summary>
    public class LoggingTextWriter : TextWriter
    {
        /// <summary>
        /// An instance of an <see cref="ILogger"/> to which log entries are written. 
        /// </summary>
        private readonly ILogger _targetLogger;

        /// <summary>
        /// The log level at which all output log entries will be written. Default: Trace.
        /// </summary>
        private readonly LogLevel _outputLogLevel;

        /// <summary>
        /// A short-term buffer of characters not yet written to the target logger.
        /// </summary>
        private StringBuilder buffer = new StringBuilder();

        /// <summary>
        /// Indicates if the previous character received was a newline character. Multiple newline
        /// characters are ignored.
        /// </summary>
        private bool previousWasNewline = false;

        /// <summary>
        /// Initialises a new instance of the <see cref="LoggingTextWriter"/> class with the given
        /// instance of a target logger to receive written messages.
        /// </summary>
        /// <param name="targetLogger">An instance of an <see cref="ILogger"/> to which log entries are written.</param>
        /// <param name="outputLogLevel">The log level at which all output log entries will be written. Default: Trace.</param>
        public LoggingTextWriter(ILogger targetLogger, LogLevel outputLogLevel = LogLevel.Trace)
        {
            this._targetLogger = targetLogger;
            this._outputLogLevel = outputLogLevel;
        }

        /// <summary>
        /// Gets the encoding used for this text writer (Unicode).
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return Encoding.Unicode;
            }
        }

        /// <summary>
        /// Gets the format provider used for string formatting (Invariant).
        /// </summary>
        public override IFormatProvider FormatProvider
        {
            get
            {
                return System.Globalization.CultureInfo.InvariantCulture;
            }
        }

        /// <summary>
        /// Writes a character to the buffer before it is written out to the target logger. 
        /// The buffer is written whenever newline characters are encountered or when on of 
        /// the many <see cref="LoggingTextWriter.WriteLine()"/> methods are called. 
        /// </summary>
        /// <param name="value">The character value to write to the logger.</param>
        public override void Write(char value)
        {
            // if we encounter newlines then the buffer must be written to the logger.
            if (value.Equals('\n') || value.Equals('\r'))
            {
                if (this.previousWasNewline)
                {
                    // ignore multiple newlines.
                    return;
                }

                // only write to the logger if the buffer isn't empty.
                if (this.buffer.Length > 0)
                {
                    this.WriteBuffer();
                    this.ClearBuffer();
                }

                // remember that this was a newline character (to ignore multiple in a row).
                this.previousWasNewline = true;
                return;
            }

            // this is not a newline character, write it to the buffer.
            this.previousWasNewline = false;
            this.buffer.Append(value);
        }

        /// <summary>
        /// Writes a string value to the buffer before it is written out to the target logger.
        /// The buffer is written when on of the many <see cref="LoggingTextWriter.WriteLine()"/> 
        /// methods are called. 
        /// </summary>
        /// <param name="value">The unformatted string value to write to the log.</param>
        public override void Write(string value)
        {
            this.buffer.Append(value);
            this.previousWasNewline = false;
        }

        /// <summary>
        /// Writes a formatted value to the buffer before it is written out to the target logger.
        /// The buffer is written when on of the many <see cref="LoggingTextWriter.WriteLine()"/> 
        /// methods are called. 
        /// </summary>
        /// <param name="format">The format string to write.</param>
        /// <param name="arg0">An argument to substitute in the format string.</param>
        public override void Write(string format, object arg0)
        {
            this.buffer.AppendFormat(this.FormatProvider, format, arg0);
            this.previousWasNewline = false;
        }

        /// <summary>
        /// Writes a formatted value to the buffer before it is written out to the target logger.
        /// The buffer is written when on of the many <see cref="LoggingTextWriter.WriteLine()"/> 
        /// methods are called. 
        /// </summary>
        /// <param name="format">The format string to write.</param>
        /// <param name="arg0">The first argument to substitute in the format string.</param>
        /// <param name="arg1">The second argument to substitute in the format string.</param>
        public override void Write(string format, object arg0, object arg1)
        {
            this.buffer.AppendFormat(this.FormatProvider, format, arg0, arg1);
            this.previousWasNewline = false;
        }

        /// <summary>
        /// Writes a formatted value to the buffer before it is written out to the target logger.
        /// The buffer is written when on of the many <see cref="LoggingTextWriter.WriteLine()"/> 
        /// methods are called. 
        /// </summary>
        /// <param name="format">The format string to write.</param>
        /// <param name="arg0">The first argument to substitute in the format string.</param>
        /// <param name="arg1">The second argument to substitute in the format string.</param>
        /// <param name="arg2">The third argument to substitute in the format string.</param>
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            this.buffer.AppendFormat(this.FormatProvider, format, arg0, arg1, arg2);
            this.previousWasNewline = false;
        }

        /// <summary>
        /// Writes a formatted value to the buffer before it is written out to the target logger.
        /// The buffer is written when on of the many <see cref="LoggingTextWriter.WriteLine()"/> 
        /// methods are called. 
        /// </summary>
        /// <param name="format">The format string to write.</param>
        /// <param name="arg">All of the arguments to substitute in the format string.</param>
        public override void Write(string format, params object[] arg)
        {
            this.buffer.AppendFormat(this.FormatProvider, format, arg);
            this.previousWasNewline = false;
        }

        /// <summary>
        /// Writes the current contents of the buffer to the target logger. If the buffer is 
        /// empty then no log entry is written to the target logger.
        /// </summary>
        public override void WriteLine()
        {
            if (this.buffer.Length > 0)
            {
                this.WriteBuffer();
                this.ClearBuffer();
                this.previousWasNewline = false;
            }
            else
            {
                this.previousWasNewline = true;
            }
        }

        /// <summary>
        /// Writes the current contents of the buffer (with the given unformatted string appended)
        /// to the target logger. If the string is empty then nothing is written to the target logger.
        /// </summary>
        /// <param name="value">The unformatted string value to write to the logger.</param>
        public override void WriteLine(string value)
        {
            this.Write(value);
            this.WriteLine();            
        }

        /// <summary>
        /// Writes the current contents of the buffer (with the given formatted string appended)
        /// to the target logger. If the string is empty then nothing is written to the target logger.
        /// </summary>
        /// <param name="format">The format string to write.</param>
        /// <param name="arg0">An argument to substitute in the format string.</param>
        public override void WriteLine(string format, object arg0)
        {
            this.Write(format, arg0);
            this.WriteLine();
        }

        /// <summary>
        /// Writes the current contents of the buffer (with the given formatted string appended)
        /// to the target logger. If the string is empty then nothing is written to the target logger.
        /// </summary>
        /// <param name="format">The format string to write.</param>
        /// <param name="arg0">The first argument to substitute in the format string.</param>
        /// <param name="arg1">The second argument to substitute in the format string.</param>
        public override void WriteLine(string format, object arg0, object arg1)
        {
            this.Write(format, arg0, arg1);
            this.WriteLine();
        }

        /// <summary>
        /// Writes the current contents of the buffer (with the given formatted string appended)
        /// to the target logger. If the string is empty then nothing is written to the target logger.
        /// </summary>
        /// <param name="format">The format string to write.</param>
        /// <param name="arg0">The first argument to substitute in the format string.</param>
        /// <param name="arg1">The second argument to substitute in the format string.</param>
        /// <param name="arg2">The third argument to substitute in the format string.</param>
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            this.Write(format, arg0, arg1, arg2);
            this.WriteLine();
        }

        /// <summary>
        /// Writes the current contents of the buffer (with the given formatted string appended)
        /// to the target logger. If the string is empty then nothing is written to the target logger.
        /// </summary>
        /// <param name="format">The format string to write.</param>
        /// <param name="arg">All arguments to substitute in the format string.</param>
        public override void WriteLine(string format, params object[] arg)
        {
            this.Write(format, arg);
            this.WriteLine();
        }

        /// <summary>
        /// Flushes the current buffer contents to the target logger.
        /// </summary>
        public override void Flush()
        {
            base.Flush();

            if (this.buffer.Length > 0)
            {
                this.WriteBuffer();
                this.ClearBuffer();
            }
        }

        /// <summary>
        /// Asynchronously flushes the current buffer contents to the target logger.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> for the flush operation.</returns>
        public override Task FlushAsync()
        {
            return new TaskFactory().StartNew(
                () => this.Flush(),
                TaskCreationOptions.AttachedToParent);
        }

        /// <summary>
        /// Flushes any buffered values to the target logger before disposing of this object.
        /// </summary>
        /// <param name="disposing">Indicator of whether this is final disposal.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this.buffer.Length > 0)
            {
                this.WriteBuffer();
                this.ClearBuffer();
            }
        }

        /// <summary>
        /// Writes an unformatted string to the target logger with the logging level
        /// defined in <see cref="_outputLogLevel"/>. 
        /// </summary>
        /// <param name="value">The unformatted string value to write to the logger.</param>
        private void WriteString(string value)
        {
            switch (this._outputLogLevel)
            {
                case LogLevel.Critical:
                    this._targetLogger.LogCritical(value);
                    break;

                case LogLevel.Debug:
                    this._targetLogger.LogDebug(value);
                    break;

                case LogLevel.Error:
                    this._targetLogger.LogError(value);
                    break;

                case LogLevel.Information:
                    this._targetLogger.LogInformation(value);
                    break;

                case LogLevel.Trace:
                    this._targetLogger.LogTrace(value);
                    break;

                case LogLevel.Warning:
                    this._targetLogger.LogWarning(value);
                    break;
            }
        }

        /// <summary>
        /// Writes a full formatted string to the target logger with the logging level
        /// defined in <see cref="_outputLogLevel"/>. 
        /// </summary>
        /// <param name="format">The format string which defines where the <paramref name="args"/> 
        /// will appear in the string.</param>
        /// <param name="args">The argument values used when constructing a format string.</param>
        private void WriteString(string format, params object[] args)
        {
            switch (this._outputLogLevel)
            {
                case LogLevel.Critical:
                    this._targetLogger.LogCritical(string.Format(this.FormatProvider, format, args));
                    break;

                case LogLevel.Debug:
                    this._targetLogger.LogDebug(string.Format(this.FormatProvider, format, args));
                    break;

                case LogLevel.Error:
                    this._targetLogger.LogError(string.Format(this.FormatProvider, format, args));
                    break;

                case LogLevel.Information:
                    this._targetLogger.LogInformation(string.Format(this.FormatProvider, format, args));
                    break;

                case LogLevel.Trace:
                    this._targetLogger.LogTrace(string.Format(this.FormatProvider, format, args));
                    break;

                case LogLevel.Warning:
                    this._targetLogger.LogWarning(string.Format(this.FormatProvider, format, args));
                    break;
            }
        }

        /// <summary>
        /// Writes the current content of the buffer to the target logger with the logging level
        /// defined in <see cref="_outputLogLevel"/>. 
        /// </summary>
        private void WriteBuffer()
        {
            switch (this._outputLogLevel)
            {
                case LogLevel.Critical:
                    this._targetLogger.LogCritical(this.buffer.ToString());
                    break;

                case LogLevel.Debug:
                    this._targetLogger.LogDebug(this.buffer.ToString());
                    break;

                case LogLevel.Error:
                    this._targetLogger.LogError(this.buffer.ToString());
                    break;

                case LogLevel.Information:
                    this._targetLogger.LogInformation(this.buffer.ToString());
                    break;

                case LogLevel.Trace:
                    this._targetLogger.LogTrace(this.buffer.ToString());
                    break;

                case LogLevel.Warning:
                    this._targetLogger.LogWarning(this.buffer.ToString());
                    break;
            }
        }

        /// <summary>
        /// Clears the current buffer of characters waiting to be logged.
        /// </summary>
        private void ClearBuffer()
        {
            this.buffer = new StringBuilder();
            this.previousWasNewline = false;
        }
    }
}
