
using QscQsys.ModuleFramework.Events;

namespace QscQsys.Communications.Sockets
{
    /// <summary>
    /// Represents a socket interface for communication.
    /// </summary>
    public interface ISocketCommunicator
    {
        /// <summary>
        /// Event that is raised when the connected state changes.
        /// </summary>
        event BoolEventHandler ConnectedChange;

        /// <summary>
        /// Event that is raised when the connection status changes.
        /// </summary>
        event ConnectionStatusChangeEventHandler ConnectionStatusChange;

        /// <summary>
        /// Event that is triggered when a response is received.
        /// </summary>
        event StringEventHandler ResponseReceived;

        /// <summary>
        /// Gets a value indicating whether the socket is currently connected.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets the connection status of the socket.
        /// </summary>
        ushort ConnectionStatus { get; }

        /// <summary>
        /// Gets or sets the IP address for the socket connection.
        /// </summary>
        string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the port number for the socket connection.
        /// </summary>
        ushort Port { get; set; }

        /// <summary>
        /// Connects to the socket.
        /// </summary>
        void Connect();

        /// <summary>
        /// Connects to the socket with the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The IP address to connect to.</param>
        /// <param name="port">The port to connect to.</param>
        void Connect(string ipAddress, ushort port);

        /// <summary>
        /// Disconnects from the socket.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Sends a command as a string.
        /// </summary>
        /// <param name="command">The command to send as a string.</param>
        void SendCommand(string command);

        /// <summary>
        /// Sends a command as a character array.
        /// </summary>
        /// <param name="command">The command to send as a character array.</param>
        void SendCommand(char[] command);

        /// <summary>
        /// Sends a command as a byte array.
        /// </summary>
        /// <param name="command">The command to send as a byte array.</param>
        void SendCommand(byte[] command);
    }
}