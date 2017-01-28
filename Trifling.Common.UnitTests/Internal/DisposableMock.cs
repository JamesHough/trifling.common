namespace Trifling.Common.UnitTests.Internal
{
    using System;

    /// <summary>
    /// A mock of a disposable object.
    /// </summary>
    internal class DisposableMock : IDisposable
    {
        /// <summary>
        /// An optional action to perform when dispose is called, for cleaning-up unit tests using
        /// this disposable object.
        /// </summary>
        private readonly Action _callbackAction;

        /// <summary>
        /// Indicates whether or not the <see cref="Dispose()"/> method has already been called.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Initialises a new instance of the <see cref="DisposableMock"/> class. 
        /// </summary>
        /// <param name="callbackAction">(Optional) An action to perform when the dispose event happens.</param>
        public DisposableMock(Action callbackAction = null)
        {
            this._callbackAction = callbackAction;
        }

        /// <summary>
        /// ddd
        /// </summary>
        ~DisposableMock()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes of the current object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // tell the GC not to finalize
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the current object.
        /// </summary>
        /// <param name="disposing">Indicates if this is a final disposal.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed && (this._callbackAction != null))
            {
                // only callback once!
                this._disposed = true;
                this._callbackAction();
            }
        }
    }
}
