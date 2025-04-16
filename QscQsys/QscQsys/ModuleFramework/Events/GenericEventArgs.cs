using System;

namespace QscQsys.ModuleFramework.Events
{
    /// <summary>
    /// Represents generic event arguments with a payload of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the payload.</typeparam>
    public class GenericEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Gets the payload value.
        /// </summary>
        public T Payload { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEventArgs{T}"/> class with the default payload value.
        /// </summary>
        public GenericEventArgs()
        {
            Payload = default(T);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEventArgs{T}"/> class with the specified payload.
        /// </summary>
        /// <param name="payload">The payload value.</param>
        public GenericEventArgs(T payload)
        {
            Payload = payload;
        }
    }
}