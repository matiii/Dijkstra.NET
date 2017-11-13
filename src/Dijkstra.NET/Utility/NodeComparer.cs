using System;
using System.Collections.Generic;
using Dijkstra.NET.Contract;

namespace Dijkstra.NET.Utility
{
    internal class NodeComparer<T, TEdgeCustom> : IComparer<INode<T, TEdgeCustom>> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        public int Compare(INode<T, TEdgeCustom> x, INode<T, TEdgeCustom> y)
        {
            int comparer = x.Distance.CompareTo(y.Distance);

            if (comparer == 0) return x.Key.CompareTo(y.Key);

            return comparer;
        }
    }
}
