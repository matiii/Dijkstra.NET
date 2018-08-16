using System;
using Dijkstra.NET.Delegates;

namespace Dijkstra.NET.Contract
{
    public interface INode<T,TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        T Item { get; }

        uint Key { get; }

        void EachChild(ChildAction<T, TEdgeCustom> action);
    }
}
