using System;
using Crestron.SimplSharp;

namespace QsysSharp.ModuleFramework.Logging
{
    /// <summary>
    /// Represents a logger implementation that implements the ILogger interface.
    /// </summary>
    public class Logger : ILogger
    {
        private readonly string _id;
        private DebugLevels _debugLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the logger.</param>
        public Logger(string id)
        {
            _id = id;
            _debugLevel = DebugLevels.Disabled;
        }

        /// <summary>
        /// Gets or sets the debug level for logging and debugging.
        /// </summary>
        public DebugLevels DebugLevel
        {
            get { return _debugLevel; }
            set { _debugLevel = value; }
        }

        /// <summary>
        /// Gets the ID of the logger.
        /// </summary>
        public string ID { get { return _id; } }

        /// <summary>
        /// Prints the string representation of an object.
        /// </summary>
        /// <param name="obj">The object to print.</param>
        public virtual void Print(object obj)
        {
            Print("{0}: {1}", DateTime.Now, obj);
        }

        /// <summary>
        /// Prints a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        public virtual void Print(string message, params object[] args)
        {
            if (_debugLevel == DebugLevels.DebugEnabled || _debugLevel == DebugLevels.AllEnabled)
                CrestronConsole.Print("{0}: ****{1}**** DEBUG {2}", DateTime.Now, _id, string.Format(message, args));
        }

        /// <summary>
        /// Prints the string representation of an object followed by a new line.
        /// </summary>
        /// <param name="obj">The object to print.</param>
        public virtual void PrintLine(object obj)
        {
            PrintLine("{0}: {1}", DateTime.Now, obj);
        }

        /// <summary>
        /// Prints a formatted string using the specified format string and arguments, followed by a new line.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        public virtual void PrintLine(string message, params object[] args)
        {
            if (_debugLevel == DebugLevels.DebugEnabled || _debugLevel == DebugLevels.AllEnabled)
                CrestronConsole.PrintLine("{0}: ****{1}**** DEBUG {2}", DateTime.Now, _id, string.Format(message, args));
        }

        /// <summary>
        /// Logs a notice-level message by printing the string representation of an object.
        /// </summary>
        /// <param name="obj">The object to log as a notice.</param>
        public virtual void LogNotice(object obj)
        {
            LogNotice("{0}", obj);
        }

        /// <summary>
        /// Logs a notice-level message by printing a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        public virtual void LogNotice(string message, params object[] args)
        {
            if (_debugLevel == DebugLevels.LoggingEnabled || _debugLevel == DebugLevels.AllEnabled)
                ErrorLog.Notice("{0}: {1}", _id, string.Format(message, args));
        }

        /// <summary>
        /// Logs a warning-level message by printing the string representation of an object.
        /// </summary>
        /// <param name="obj">The object to log as a warning.</param>
        public virtual void LogWarning(object obj)
        {
            LogWarning("{0}", obj);
        }

        /// <summary>
        /// Logs a warning-level message by printing a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        public virtual void LogWarning(string message, params object[] args)
        {
            if (_debugLevel == DebugLevels.LoggingEnabled || _debugLevel == DebugLevels.AllEnabled)
                ErrorLog.Warn("{0}: {1}", _id, string.Format(message, args));
        }

        /// <summary>
        /// Logs an error-level message by printing the string representation of an object.
        /// </summary>
        /// <param name="obj">The object to log as an error.</param>
        public virtual void LogError(object obj)
        {
            LogError("{0}", obj);
        }

        /// <summary>
        /// Logs an error-level message by printing a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        public virtual void LogError(string message, params object[] args)
        {
            if (_debugLevel == DebugLevels.LoggingEnabled || _debugLevel == DebugLevels.AllEnabled)
                ErrorLog.Error("{0}: {1}", _id, string.Format(message, args));
        }
        
        /// <summary>
        /// Logs an exception by printing the exception details.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public virtual void LogException(Exception ex)
        {
            LogException(ex, "{0}", ex.Message);
        }

        /// <summary>
        /// Logs an exception by printing the exception details along with the string representation of an object.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        /// <param name="obj">The object to log along with the exception.</param>
        public virtual void LogException(Exception ex, object obj)
        {
            LogException(ex, "{0}", obj);
        }

        /// <summary>
        /// Logs an exception by printing the exception details along with a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        public virtual void LogException(Exception ex, string message, params object[] args)
        {
            if (_debugLevel == DebugLevels.LoggingEnabled || _debugLevel == DebugLevels.AllEnabled)
                ErrorLog.Exception((string.Format("{0}: {1}", _id, string.Format(message, args))), ex);
        }
    }
}