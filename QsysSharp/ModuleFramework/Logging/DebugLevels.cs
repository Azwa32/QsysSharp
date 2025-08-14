
namespace QsysSharp.ModuleFramework.Logging
{
    /// <summary>
    /// Specifies the debug levels for logging and debugging.
    /// </summary>
    public enum DebugLevels
    {
        /// <summary>
        /// Debugging and logging are disabled.
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// Logging is enabled, but debugging is disabled.
        /// </summary>
        LoggingEnabled = 1,

        /// <summary>
        /// Debugging is enabled, but logging is disabled.
        /// </summary>
        DebugEnabled = 2,

        /// <summary>
        /// Both debugging and logging are enabled.
        /// </summary>
        AllEnabled = 3,
    }
}