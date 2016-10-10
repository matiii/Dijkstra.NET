namespace Dijkstra.NET.Model
{
    using System;
    using System.Collections.Generic;
    using Contract;
    public class Node<T, TEdgeCustom>: INode<T, TEdgeCustom> where TEdgeCustom: class
    {
        public Node(uint key, T item)
        {
            Key = key;
            Item = item;
        }

        public IList<Edge<T, TEdgeCustom>> Children { get; } = new List<Edge<T, TEdgeCustom>>();
        public uint Key { get; }
        public T Item { get; }
        public uint Distance { get; set; } = UInt32.MaxValue;
    }
}
