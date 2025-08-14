using System;
using JetBrains.Annotations;

namespace QsysSharp.Intermediaries
{
    public interface IQsysIntermediaryControl : IQsysIntermediary
    {
        event EventHandler<QsysInternalEventsArgs> OnStateChanged;
        event EventHandler<DataBoolEventArgs> OnSubscribeChanged;

        [CanBeNull]
        QsysStateData State { get; }
        bool Subscribe { get; }

        void SendChangePosition(double position);
        void SendChangeDoubleValue(double value);
        void SendChangeStringValue(string value);
    }
}