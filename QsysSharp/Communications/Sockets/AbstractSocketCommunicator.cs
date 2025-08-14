using Crestron.SimplSharp.CrestronSockets;
using QsysSharp.ModuleFramework.Events;
using QsysSharp.ModuleFramework.Logging;

namespace QsysSharp.Communications.Sockets
{
    /// <summary>
    /// Abstract base class for a socket communicator that implements the ISocket interface.
    /// </summary>
    public abstract class AbstractSocketCommunicator : AbstractCommunicator, ISocketCommunicator
    {
        protected bool ProtectedConnected;
        protected bool ConnectionRequested;
        protected string ProtectedIpAddress;
        protected ushort ProtectedPort;

        /// <summary>
        /// Event that is raised when the connected state changes.
        /// </summary>
        public event BoolEventHandler ConnectedChange;

        /// <summary>
        /// Event that is raised when the connection status changes.
        /// </summary>
        public event ConnectionStatusChangeEventHandler ConnectionStatusChange;

        /// <summary>
        /// Event that is triggered when a response is received.
        /// </summary>
        public event StringEventHandler ResponseReceived;

        /// <summary>
        /// Gets a value indicating whether the socket is currently connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                lock (MainLock)
                {
                    return ProtectedConnected;
                }
            }
        }

        /// <summary>
        /// Gets the connection status of the socket.
        /// </summary>
        public ushort ConnectionStatus { get; protected set; }

        /// <summary>
        /// Gets or sets the IP address for the socket connection.
        /// </summary>
        public string IpAddress
        {
            get { lock (MainLock) return ProtectedIpAddress; }
            set
            {
                lock (MainLock)
                {
                    if (ProtectedIpAddress == value)
                        return;

                    ProtectedIpAddress = value;

                    if (ConnectionRequested)
                        Connect();
                }
            }
        }

        /// <summary>
        /// Gets or sets the port number for the socket connection.
        /// </summary>
        public ushort Port
        {
            get { lock (MainLock) return ProtectedPort; }
            set
            {
                lock (MainLock)
                {
                    if (ProtectedPort == value)
                        return;

                    ProtectedPort = value;

                    if (ConnectionRequested)
                        Connect();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the SocketCommunicator class.
        /// </summary>
        public AbstractSocketCommunicator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SocketCommunicator class with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the socket communicator.</param>
        public AbstractSocketCommunicator(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SocketCommunicator class with the specified logger.
        /// </summary>
        /// <param name="logger">The logger to use for logging.</param>
        public AbstractSocketCommunicator(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SocketCommunicator class with the specified ID and logger.
        /// </summary>
        /// <param name="id">The ID of the socket communicator.</param>
        /// <param name="logger">The logger to use for logging.</param>
        public AbstractSocketCommunicator(string id, ILogger logger)
            : base(id, logger)
        {
        }

        /// <summary>
        /// Connects to the socket.
        /// </summary>
        public abstract void Connect();

        /// <summary>
        /// Connects to the socket with the specified IP address and port.
        /// </summary>
        /// <param name="ipAddress">The IP address to connect to.</param>
        /// <param name="port">The port to connect to.</param>
        public abstract void Connect(string ipAddress, ushort port);

        /// <summary>
        /// Disconnects from the socket.
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// Sends a command as a string.
        /// </summary>
        /// <param name="command">The command to send as a string.</param>
        public abstract void SendCommand(string command);

        /// <summary>
        /// Sends a command as a character array.
        /// </summary>
        /// <param name="command">The command to send as a character array.</param>
        public abstract void SendCommand(char[] command);

        /// <summary>
        /// Sends a command as a byte array.
        /// </summary>
        /// <param name="command">The command to send as a byte array.</param>
        public abstract void SendCommand(byte[] command);

        /// <summary>
        /// Gets the name of the socket status based on the provided SocketStatus value.
        /// </summary>
        /// <param name="value">The SocketStatus value.</param>
        /// <returns>The name of the socket status.</returns>
        static protected string GetSocketStatusName(SocketStatus value)
        {
            string status;

            switch (value)
            {
                case SocketStatus.SOCKET_STATUS_BROKEN_LOCALLY:
                    status = "SOCKET_STATUS_BROKEN_LOCALLY";
                    break;
                case SocketStatus.SOCKET_STATUS_BROKEN_REMOTELY:
                    status = "SOCKET_STATUS_BROKEN_REMOTELY";
                    break;
                case SocketStatus.SOCKET_STATUS_CONNECTED:
                    status = "SOCKET_STATUS_CONNECTED";
                    break;
                case SocketStatus.SOCKET_STATUS_CONNECT_FAILED:
                    status = "SOCKET_STATUS_CONNECT_FAILED";
                    break;
                case SocketStatus.SOCKET_STATUS_DNS_FAILED:
                    status = "SOCKET_STATUS_DNS_FAILED";
                    break;
                case SocketStatus.SOCKET_STATUS_DNS_LOOKUP:
                    status = "SOCKET_STATUS_DNS_LOOKUP";
                    break;
                case SocketStatus.SOCKET_STATUS_DNS_RESOLVED:
                    status = "SOCKET_STATUS_DNS_RESOLVED";
                    break;
                case SocketStatus.SOCKET_STATUS_LINK_LOST:
                    status = "SOCKET_STATUS_LINK_LOST";
                    break;
                case SocketStatus.SOCKET_STATUS_NO_CONNECT:
                    status = "SOCKET_STATUS_NO_CONNECT";
                    break;
                case SocketStatus.SOCKET_STATUS_SOCKET_NOT_EXIST:
                    status = "SOCKET_STATUS_SOCKET_NOT_EXIST";
                    break;
                case SocketStatus.SOCKET_STATUS_WAITING:
                    status = "SOCKET_STATUS_WAITING";
                    break;
                default:
                    status = string.Empty;
                    break;
            }

            return status;
        }

        /// <summary>
        /// Gets the name of the socket error code based on the provided SocketErrorCodes value.
        /// </summary>
        /// <param name="value">The SocketErrorCodes value.</param>
        /// <returns>The name of the socket error code.</returns>
        static protected string GetSocketErrorCodeName(SocketErrorCodes value)
        {
            string errorCode;

            switch (value)
            {
                case SocketErrorCodes.SOCKET_ADDRESS_NOT_SPECIFIED:
                    errorCode = "SOCKET_ADDRESS_NOT_SPECIFIED";
                    break;
                case SocketErrorCodes.SOCKET_BUFFER_NOT_ALLOCATED:
                    errorCode = "SOCKET_BUFFER_NOT_ALLOCATED";
                    break;
                case SocketErrorCodes.SOCKET_CONNECTION_IN_PROGRESS:
                    errorCode = "SOCKET_CONNECTION_IN_PROGRESS";
                    break;
                case SocketErrorCodes.SOCKET_INVALID_ADDRESS_ADAPTER_BINDING:
                    errorCode = "SOCKET_INVALID_ADDRESS_ADAPTER_BINDING";
                    break;
                case SocketErrorCodes.SOCKET_INVALID_CLIENT_INDEX:
                    errorCode = "SOCKET_INVALID_CLIENT_INDEX";
                    break;
                case SocketErrorCodes.SOCKET_INVALID_PORT_NUMBER:
                    errorCode = "SOCKET_INVALID_PORT_NUMBER";
                    break;
                case SocketErrorCodes.SOCKET_INVALID_STATE:
                    errorCode = "SOCKET_INVALID_STATE";
                    break;
                case SocketErrorCodes.SOCKET_MAX_CONNECTIONS_REACHED:
                    errorCode = "SOCKET_MAX_CONNECTIONS_REACHED";
                    break;
                case SocketErrorCodes.SOCKET_NO_HOSTNAME_RESOLVE:
                    errorCode = "SOCKET_NO_HOSTNAME_RESOLVE";
                    break;
                case SocketErrorCodes.SOCKET_NOT_ALLOWED_IN_SECURE_MODE:
                    errorCode = "SOCKET_NOT_ALLOWED_IN_SECURE_MODE";
                    break;
                case SocketErrorCodes.SOCKET_NOT_CONNECTED:
                    errorCode = "SOCKET_NOT_CONNECTED";
                    break;
                case SocketErrorCodes.SOCKET_OK:
                    errorCode = "SOCKET_OK";
                    break;
                case SocketErrorCodes.SOCKET_OPERATION_PENDING:
                    errorCode = "SOCKET_OPERATION_PENDING";
                    break;
                case SocketErrorCodes.SOCKET_OUT_OF_MEMORY:
                    errorCode = "SOCKET_OUT_OF_MEMORY";
                    break;
                case SocketErrorCodes.SOCKET_SPECIFIED_PORT_ALREADY_IN_USE:
                    errorCode = "SOCKET_SPECIFIED_PORT_ALREADY_IN_USE";
                    break;
                case SocketErrorCodes.SOCKET_UDP_SERVER_RECEIVE_ONLY:
                    errorCode = "SOCKET_UDP_SERVER_RECEIVE_ONLY";
                    break;
                default:
                    errorCode = string.Empty;
                    break;
            }

            return errorCode;
        }

        /// <summary>
        /// Gets the name of the web socket result code based on the provided WEBSOCKET_RESULT_CODES value.
        /// </summary>
        /// <param name="value">The WEBSOCKET_RESULT_CODES value.</param>
        /// <returns>The name of the web socket result code.</returns>
        static protected string GetWebSocketResultCodeName(Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES value)
        {
            string resultCode;

            switch (value)
            {
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_ALREADY_CONNECTED:
                    resultCode = "WEBSOCKET_CLIENT_ALREADY_CONNECTED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_ASYNC_READ_CB_ALREADY_SET:
                    resultCode = "WEBSOCKET_CLIENT_ASYNC_READ_CB_ALREADY_SET";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_ASYNC_WRITE_BUSY_SENDING:
                    resultCode = "WEBSOCKET_CLIENT_ASYNC_WRITE_BUSY_SENDING";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_CLOSE_IN_PROGRESS:
                    resultCode = "WEBSOCKET_CLIENT_CLOSE_IN_PROGRESS";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_HOSTNAME_LOOKUP_BY_IPADDR_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_HOSTNAME_LOOKUP_BY_IPADDR_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_HTTP_HANDSHAKE_RESPONSE_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_HTTP_HANDSHAKE_RESPONSE_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_HTTP_HANDSHAKE_SECURITY_KEY_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_HTTP_HANDSHAKE_SECURITY_KEY_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_HTTP_HANDSHAKE_TOKEN_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_HTTP_HANDSHAKE_TOKEN_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INSUFFICIENT_BUFFER:
                    resultCode = "WEBSOCKET_CLIENT_INSUFFICIENT_BUFFER";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_HANDLE:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_HANDLE";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_HOSTNAME:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_HOSTNAME";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_HOSTNAME_AND_IPADDR:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_HOSTNAME_AND_IPADDR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_IPADDR:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_IPADDR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_PACKET_CTLFLAG:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_PACKET_CTLFLAG";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_PACKET_OPCODE_CTLFLAG:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_PACKET_OPCODE_CTLFLAG";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_PATH:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_PATH";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_POINTER:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_POINTER";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_PROXY_IPADDR:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_PROXY_IPADDR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_INVALID_URL:
                    resultCode = "WEBSOCKET_CLIENT_INVALID_URL";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_IPADDR_FAMILY_NOT_SUPPORTED:
                    resultCode = "WEBSOCKET_CLIENT_IPADDR_FAMILY_NOT_SUPPORTED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_IPADDR_LOOKUP_BY_HOSTNAME_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_IPADDR_LOOKUP_BY_HOSTNAME_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_LINKLIST_INSERT_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_LINKLIST_INSERT_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_LINKLIST_OBTAIN_HANDLE_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_LINKLIST_OBTAIN_HANDLE_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_MEMORY_ALLOC_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_MEMORY_ALLOC_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_PENDING:
                    resultCode = "WEBSOCKET_CLIENT_PENDING";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_PROXY_HOSTNAME_LOOKUP_BY_IPADDR_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_PROXY_HOSTNAME_LOOKUP_BY_IPADDR_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_PROXY_IPADDR_LOOKUP_BY_HOSTNAME_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_PROXY_IPADDR_LOOKUP_BY_HOSTNAME_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_READ_BUFFER_SIZE_INVALID:
                    resultCode = "WEBSOCKET_CLIENT_READ_BUFFER_SIZE_INVALID";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SERVER_CERTIFICATE_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_SERVER_CERTIFICATE_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SOCKET_CONNECTION_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_SOCKET_CONNECTION_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SOCKET_CREATION_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_SOCKET_CREATION_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SOCKET_RECEIVE_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_SOCKET_RECEIVE_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SOCKET_SELECT_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_SOCKET_SELECT_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SSL_CONNECTION_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_SSL_CONNECTION_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SSL_CONTEXT_ALLOC_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_SSL_CONTEXT_ALLOC_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SSL_LIBRARY_INIT_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_SSL_LIBRARY_INIT_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SSL_OPTION_SETTING_FAILED:
                    resultCode = "WEBSOCKET_CLIENT_SSL_OPTION_SETTING_FAILED";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SSL_RECEIVE_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_SSL_RECEIVE_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SSL_SOCKET_SEND_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_SSL_SOCKET_SEND_ERROR";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_SUCCESS:
                    resultCode = "WEBSOCKET_CLIENT_SUCCESS";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_RESULT_CODES.WEBSOCKET_CLIENT_TCP_SOCKET_SEND_ERROR:
                    resultCode = "WEBSOCKET_CLIENT_TCP_SOCKET_SEND_ERROR";
                    break;
                default:
                    resultCode = string.Empty;
                    break;
            }

            return resultCode;
        }

        /// <summary>
        /// Gets the name of the web socket packet type based on the provided WEBSOCKET_PACKET_TYPES value.
        /// </summary>
        /// <param name="value">The WEBSOCKET_PACKET_TYPES value.</param>
        /// <returns>The name of the web socket packet type.</returns>
        static protected string GetWebSocketPackekTypeName(Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_PACKET_TYPES value)
        {
            string packetType;

            switch (value)
            {
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_PACKET_TYPES.LWS_WS_OPCODE_07__BINARY_FRAME:
                    packetType = "LWS_WS_OPCODE_07__BINARY_FRAME";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_PACKET_TYPES.LWS_WS_OPCODE_07__CLOSE:
                    packetType = "LWS_WS_OPCODE_07__CLOSE";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_PACKET_TYPES.LWS_WS_OPCODE_07__CONTINUATION:
                    packetType = "LWS_WS_OPCODE_07__CONTINUATION";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_PACKET_TYPES.LWS_WS_OPCODE_07__NOSPEC__MUX:
                    packetType = "LWS_WS_OPCODE_07__NOSPEC__MUX";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_PACKET_TYPES.LWS_WS_OPCODE_07__PING:
                    packetType = "LWS_WS_OPCODE_07__PING";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_PACKET_TYPES.LWS_WS_OPCODE_07__PONG:
                    packetType = "LWS_WS_OPCODE_07__PONG";
                    break;
                case Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient.WEBSOCKET_PACKET_TYPES.LWS_WS_OPCODE_07__TEXT_FRAME:
                    packetType = "LWS_WS_OPCODE_07__TEXT_FRAME";
                    break;
                default:
                    packetType = string.Empty;
                    break;
            }

            return packetType;
        }

        /// <summary>
        /// Raises the ConnectedChange event with the provided event arguments.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        protected void OnConnectedChange(BoolEventArgs args)
        {
            var h = ConnectedChange;

            if (h == null)
                return;

            h.Invoke(this, args);
        }

        /// <summary>
        /// Raises the ConnectionStatusChange event with the provided event arguments.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        protected void OnConnectionStatusChange(ConnectionStatusChangeEventArgs args)
        {
            var h = ConnectionStatusChange;

            if (h == null)
                return;

            h.Invoke(this, args);
        }

        /// <summary>
        /// Raises the <see cref="ResponseReceived"/> event with the specified event arguments.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        protected void OnResponseReceived(StringEventArgs args)
        {
            var h = ResponseReceived;

            if (h == null)
                return;

            h.Invoke(this, args);
        }
    }
}