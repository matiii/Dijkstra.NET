namespace Dijkstra.NET.Contract
{
    using System;

    public interface IConcurrentNode<T, TEdgeCustom> : INode<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        bool TrySetDistance(int distance, int comparator);
    }
}
