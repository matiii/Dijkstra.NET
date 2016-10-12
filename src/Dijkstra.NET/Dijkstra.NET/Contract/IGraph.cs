namespace Dijkstra.NET.Contract
{
    using System.Collections.Generic;

    public interface IGraph<T, TEdgeCustom> where TEdgeCustom: class
    {
        INode<T, TEdgeCustom> this[uint node] { get; }
    }
}
