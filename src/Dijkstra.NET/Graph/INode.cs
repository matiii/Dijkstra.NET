using System;

namespace Dijkstra.NET.Graph
{
    public interface INode
    {
        uint Key { get; }
    }
    
    public interface INode<T, TEdgeCustom> : INode where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        T Item { get; }
    }
}
