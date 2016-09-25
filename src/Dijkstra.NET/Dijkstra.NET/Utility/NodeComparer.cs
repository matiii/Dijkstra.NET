namespace Dijkstra.NET.Utility
{
    using System.Collections.Generic;
    using Contract;
    internal class NodeComparer<T>: IComparer<INode<T>>
    {
        public int Compare(INode<T> x, INode<T> y)
        {
            int comparer = x.Distance.CompareTo(y.Distance);

            if (comparer == 0) return x.Key.CompareTo(y.Key);

            return comparer;
        }
    }
}
