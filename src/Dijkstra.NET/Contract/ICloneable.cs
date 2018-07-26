using System;

namespace Dijkstra.NET.Contract
{
    [Obsolete]
    public interface ICloneable<out T>
    {
        T Clone();
    }
}
