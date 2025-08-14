using System;

namespace QsysSharp.ModuleFramework.Events
{
    /// <summary>
    /// Represents event arguments containing a boolean payload.
    /// </summary>
    public class BoolEventArgs : GenericEventArgs<ushort>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoolEventArgs"/> class with a default payload value of false.
        /// </summary>
        public BoolEventArgs()
            : base(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolEventArgs"/> class with the specified boolean payload.
        /// </summary>
        /// <param name="payload">The boolean value to be converted and set as the payload.</param>
        public BoolEventArgs(bool payload)
            : base(Convert.ToUInt16(payload))
        {
        }
    }
}