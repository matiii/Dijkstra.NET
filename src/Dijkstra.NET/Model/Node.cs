using System;
using System.Collections.Generic;
using Dijkstra.NET.Contract;

namespace Dijkstra.NET.Model
{
    public class Node<T, TEdgeCustom>: INode<T, TEdgeCustom> where TEdgeCustom: IEquatable<TEdgeCustom>
    {
        public Node(uint key, T item)
        {
            Key = key;
            Item = item;
            Distance = Int32.MaxValue;
        }

        public IList<Edge<T, TEdgeCustom>> Children { get; } = new List<Edge<T, TEdgeCustom>>();

        public uint Key { get; }

        public T Item { get; }

        public int Distance { get; set; }

        public int QueueIndex { get; set; }
    }
}
