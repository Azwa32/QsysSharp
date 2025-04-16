
namespace QscQsys.ModuleFramework.Events
{
    /// <summary>
    /// Represents event arguments containing a string payload.
    /// </summary>
    public class StringEventArgs : GenericEventArgs<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringEventArgs"/> class with an empty string as the default payload.
        /// </summary>
        public StringEventArgs()
            : base(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringEventArgs"/> class with the specified string payload.
        /// </summary>
        /// <param name="payload">The string value to be set as the payload.</param>
        public StringEventArgs(string payload)
            : base(payload)
        {
        }
    }
}