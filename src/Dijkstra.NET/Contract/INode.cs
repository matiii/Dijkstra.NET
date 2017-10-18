using System;
using System.Collections.Generic;
using Dijkstra.NET.Model;

namespace Dijkstra.NET.Contract
{
    public interface INode<T,TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        IList<Edge<T, TEdgeCustom>> Children { get; }

        T Item { get; }

        uint Key { get; }

        int Distance { get; set; }

        int QueueIndex { get; set; }
    }
}
