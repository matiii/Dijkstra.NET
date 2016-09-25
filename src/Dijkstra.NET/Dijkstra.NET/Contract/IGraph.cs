namespace Dijkstra.NET.Contract
{
    using System.Collections.Generic;

    public interface IGraph<T>: IEnumerable<INode<T>>
    {
        INode<T> this[uint node] { get; }
    }
}
