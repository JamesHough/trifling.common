namespace Trifling.Common.UnitTests.Internal
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides a mock of an <see cref="ILogger"/> for testing logger operations. 
    /// </summary>
    internal class LoggingMock : ILogger
    {
        /// <summary>
        /// Contains a list of previous calls to the <see cref="Log{TState}(LogLevel, EventId, TState, Exception, Func{TState, Exception, string})"/> method. 
        /// </summary>
        private Dictionary<string, LogLevel> _history = new Dictionary<string, LogLevel>();

        /// <summary>
        /// Begins a "using" scope - ignored by this implementation.
        /// </summary>
        /// <typeparam name="TState">The type of object being scoped.</typeparam>
        /// <param name="state">The value being scoped.</param>
        /// <returns>Always returns a new instance of the <see cref="DisposableMock"/> class.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return new DisposableMock();
        }

        /// <summary>
        /// Determins if the logging level specified is enabled for this logger. Always returns true.
        /// </summary>
        /// <param name="logLevel">The logging level to check.</param>
        /// <returns>Always returns true.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Logs to the logger.
        /// </summary>
        /// <typeparam name="TState">The type of object being logged - typically string.</typeparam>
        /// <param name="logLevel">The log Level of the event.</param>
        /// <param name="eventId">The Id of the event.</param>
        /// <param name="state">The value being logged.</param>
        /// <param name="exception">An exception being logged.</param>
        /// <param name="formatter">A formatter which can format the <paramref name="state"/> value for output to the log.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this._history.ContainsKey(state.ToString()))
            {
                this._history.Add(state.ToString(), logLevel);
            }
        }

        /// <summary>
        /// Checks if the given string was logged on this logger.
        /// </summary>
        /// <param name="messageString">The string to check if it was logged.</param>
        /// <param name="logLevel">(Optional) Restricts the search to make sure that the value was logged with a particular log level.</param>
        /// <returns>Returns true if the message string was logged (at the specified logging level), otherwise returns false.</returns>
        public bool CallHappened(string messageString, LogLevel logLevel = LogLevel.None)
        {
            return this._history.ContainsKey(messageString) 
                && (logLevel == LogLevel.None || this._history[messageString] == logLevel);
        }
    }
}
