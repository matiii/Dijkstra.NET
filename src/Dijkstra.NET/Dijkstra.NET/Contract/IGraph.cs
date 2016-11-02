namespace Dijkstra.NET.Contract
{
    using System;

    public interface IGraph<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        INode<T, TEdgeCustom> this[uint node] { get; }
    }
}
