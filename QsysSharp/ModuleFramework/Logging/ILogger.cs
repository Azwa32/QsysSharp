using System;

namespace QsysSharp.ModuleFramework.Logging
{
    /// <summary>
    /// Represents an interface for logging functionality.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets or sets the debug level for logging and debugging.
        /// </summary>
        DebugLevels DebugLevel { get; set; }

        /// <summary>
        /// Prints the string representation of an object.
        /// </summary>
        /// <param name="obj">The object to print.</param>
        void Print(object obj);

        /// <summary>
        /// Prints a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        void Print(string message, params object[] args);

        /// <summary>
        /// Prints the string representation of an object followed by a new line.
        /// </summary>
        /// <param name="obj">The object to print.</param>
        void PrintLine(object obj);

        /// <summary>
        /// Prints a formatted string using the specified format string and arguments, followed by a new line.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        void PrintLine(string message, params object[] args);

        /// <summary>
        /// Logs a notice-level message by printing the string representation of an object.
        /// </summary>
        /// <param name="obj">The object to log as a notice.</param>
        void LogNotice(object obj);

        /// <summary>
        /// Logs a notice-level message by printing a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        void LogNotice(string message, params object[] args);

        /// <summary>
        /// Logs a warning-level message by printing the string representation of an object.
        /// </summary>
        /// <param name="obj">The object to log as a warning.</param>
        void LogWarning(object obj);

        /// <summary>
        /// Logs a warning-level message by printing a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        void LogWarning(string message, params object[] args);

        /// <summary>
        /// Logs an error-level message by printing the string representation of an object.
        /// </summary>
        /// <param name="obj">The object to log as an error.</param>
        void LogError(object obj);

        /// <summary>
        /// Logs an error-level message by printing a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        void LogError(string message, params object[] args);

        /// <summary>
        /// Logs an exception by printing the exception details.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        void LogException(Exception ex);

        /// <summary>
        /// Logs an exception by printing the exception details along with the string representation of an object.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        /// <param name="obj">The object to log along with the exception.</param>
        void LogException(Exception ex, object obj);

        /// <summary>
        /// Logs an exception by printing the exception details along with a formatted string using the specified format string and arguments.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        /// <param name="message">The format string.</param>
        /// <param name="args">The arguments to format the string.</param>
        void LogException(Exception ex, string message, params object[] args);
    }
}