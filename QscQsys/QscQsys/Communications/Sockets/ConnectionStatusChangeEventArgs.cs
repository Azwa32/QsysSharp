using QscQsys.ModuleFramework.Events;

namespace QscQsys.Communications.Sockets
{
    /// <summary>
    /// Event arguments class for connection status change events.
    /// </summary>
    public class ConnectionStatusChangeEventArgs : GenericEventArgs<ConnectionStatusChangePayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStatusChangeEventArgs"/> class with an empty payload.
        /// </summary>
        public ConnectionStatusChangeEventArgs()
            : base(new ConnectionStatusChangePayload())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStatusChangeEventArgs"/> class with the specified payload.
        /// </summary>
        /// <param name="payload">The connection status change payload.</param>
        public ConnectionStatusChangeEventArgs(ConnectionStatusChangePayload payload)
            : base(payload)
        {
        }
    }
}