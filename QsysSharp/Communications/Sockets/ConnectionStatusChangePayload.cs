
namespace QsysSharp.Communications.Sockets
{
    /// <summary>
    /// Represents the payload containing connection status change information.
    /// </summary>
    public class ConnectionStatusChangePayload
    {
        /// <summary>
        /// Gets or sets the index associated with the connection status change.
        /// </summary>
        public ushort Index { get; set; }

        /// <summary>
        /// Gets or sets the status of the connection.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStatusChangePayload"/> class.
        /// </summary>
        public ConnectionStatusChangePayload()
        {
            Index = ushort.MinValue;
            Status = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStatusChangePayload"/> class with the specified index and status.
        /// </summary>
        /// <param name="index">The index associated with the connection status change.</param>
        /// <param name="status">The status of the connection.</param>
        public ConnectionStatusChangePayload(ushort index, string status)
        {
            Index = index;
            Status = status;
        }
    }
}