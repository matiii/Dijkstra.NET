using System;

namespace Dijkstra.NET.Contract
{
    public interface IConcurrentGraph<T, TEdgeCustom> : IGraph<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        IConcurrentNode<T, TEdgeCustom> GetConccurentNode(uint node);
    }
}
