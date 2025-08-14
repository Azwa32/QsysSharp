using System;

namespace QsysSharp.Intermediaries
{
    public interface IQsysIntermediary
    {
        string Name { get; }
        QsysCore Core { get; }
    }
}