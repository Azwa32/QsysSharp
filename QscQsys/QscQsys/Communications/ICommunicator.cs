
namespace QscQsys.Communications
{
    /// <summary>
    /// Represents a communication interface for sending and receiving commands.
    /// </summary>
    public interface ICommunicator
    {
        /// <summary>
        /// Gets a value indicating whether the communicator has been disposed.
        /// </summary>
        bool Disposed { get; }

        /// <summary>
        /// Gets the ID of the communicator.
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Gets or sets the debug level for logging. Valid values are 0 (Disabled), 1 (LoggingEnabled), 2 (DebugEnabled), and 3 (AllEnabled).
        /// </summary>
        ushort DebugLevel { get; set; }

        /// <summary>
        /// Disposes the communicator and releases any resources.
        /// </summary>
        void Dispose();
    }
}