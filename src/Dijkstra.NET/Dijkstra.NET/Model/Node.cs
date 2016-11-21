namespace Dijkstra.NET.Model
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Contract;
    public class Node<T, TEdgeCustom>: IConcurrentNode<T, TEdgeCustom> where TEdgeCustom: IEquatable<TEdgeCustom>
    {
        private int _distance;

        public Node(uint key, T item)
        {
            Key = key;
            Item = item;
            _distance = Int32.MaxValue;
        }

        public IList<Edge<T, TEdgeCustom>> Children { get; } = new List<Edge<T, TEdgeCustom>>();
        public uint Key { get; }
        public T Item { get; }

        public int Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        /// <summary>
        /// Thread safe add distance.
        /// </summary>
        /// <param name="distance">New value</param>
        /// <param name="comparator">Old value. If old value is equal then distance was replaced successfully.</param>
        /// <returns>If true distance was replaced successfully.</returns>
        public bool TrySetDistance(int distance, int comparator)
        {
            return Interlocked.CompareExchange(ref _distance, distance, comparator) == comparator;
        }
    }
}
