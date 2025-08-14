 using System;
using Crestron.SimplSharp;
using QsysSharp.ModuleFramework.Logging;

namespace QsysSharp.Communications
{
    /// <summary>
    /// Abstract base class for a communicator that implements the <see cref="ICommunicator"/> interface and provides common functionality.
    /// </summary>
    public abstract class AbstractCommunicator : ICommunicator, IDisposable
    {
        private readonly string _id;
        protected ILogger Logger { get; private set; }
        protected readonly object MainLock = new object();
        protected bool ProtectedDisposed;

        /// <summary>
        /// Gets a value indicating whether the communicator has been disposed.
        /// </summary>
        public bool Disposed
        {
            get
            {
                lock (MainLock)
                {
                    return ProtectedDisposed;
                }
            }
        }

        /// <summary>
        /// Gets the ID of the communicator.
        /// </summary>
        public string ID { get { return _id; } }

        /// <summary>
        /// Gets or sets the debug level for logging. Valid values are 0 (Disabled), 1 (LoggingEnabled), 2 (DebugEnabled), and 3 (AllEnabled).
        /// </summary>
        public ushort DebugLevel
        {
            get { return (ushort)Logger.DebugLevel; }
            set
            {
                if ((ushort)Logger.DebugLevel == value || value > 3)
                    return;

                Logger.DebugLevel = (DebugLevels)Enum.ToObject(typeof(DebugLevels), value);
            }
        }

        protected void ThrowIfDisposed()
        {
            if(ProtectedDisposed)
                throw new ObjectDisposedException(this.GetType().FullName);
        }

        /// <summary>
        /// Disposes the communicator and releases any resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCommunicator"/> class with a generated ID and default logger.
        /// </summary>
        public AbstractCommunicator() : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCommunicator"/> class with the specified ID and default logger.
        /// </summary>
        /// <param name="id">The ID of the communicator.</param>
        public AbstractCommunicator(string id) : this(id, new Logger(id))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCommunicator"/> class with a generated ID and the specified logger.
        /// </summary>
        /// <param name="logger">The logger to use for logging.</param>
        public AbstractCommunicator(ILogger logger) : this(Guid.NewGuid().ToString(), logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCommunicator"/> class with the specified ID and logger.
        /// </summary>
        /// <param name="id">The ID of the communicator.</param>
        /// <param name="logger">The logger to use for logging.</param>
        public AbstractCommunicator(string id, ILogger logger)
        {
            _id = id;
            Logger = logger;

            CrestronEnvironment.ProgramStatusEventHandler += CrestronEnvironment_ProgramStatusEventHandler;
        }

        void CrestronEnvironment_ProgramStatusEventHandler(eProgramStatusEventType programEventType)
        {
            switch (programEventType)
            {
                case eProgramStatusEventType.Stopping:
                    Dispose();
                    break;
                case eProgramStatusEventType.Paused:
                    break;
                case eProgramStatusEventType.Resumed:
                    break;
            }
        }
    }
}