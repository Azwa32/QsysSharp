
namespace QsysSharp.ModuleFramework.Events
{
    /// <summary>
    /// Represents the method that handles events with a string payload.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="args">The event arguments.</param>
    public delegate void StringEventHandler(object sender, StringEventArgs args);
}