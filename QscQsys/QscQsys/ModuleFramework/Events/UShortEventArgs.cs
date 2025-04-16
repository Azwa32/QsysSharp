
namespace QscQsys.ModuleFramework.Events
{
    /// <summary>
    /// Represents event arguments containing an unsigned short (ushort) payload.
    /// </summary>
    public class UShortEventArgs : GenericEventArgs<ushort>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UShortEventArgs"/> class with the minimum possible ushort value as the default payload.
        /// </summary>
        public UShortEventArgs()
            : base(ushort.MinValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UShortEventArgs"/> class with the specified ushort payload.
        /// </summary>
        /// <param name="payload">The ushort value to be set as the payload.</param>
        public UShortEventArgs(ushort payload)
            : base(payload)
        {
        }
    }
}