namespace Dijkstra.NET.Model
{
    using System;
    using System.Collections.Generic;
    using Contract;
    public class Node<T>: INode<T>
    {
        public Node(uint key, T item)
        {
            Key = key;
            Item = item;
        }



        public IList<IEdge<T>> Children { get; } = new List<IEdge<T>>();
        public IList<IEdge<T>> Parents { get; } = new List<IEdge<T>>();
        public uint Key { get; }
        public T Item { get; }
        public uint Distance { get; set; } = UInt32.MaxValue;
    }
}
