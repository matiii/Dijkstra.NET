using System;
using System.Collections.Generic;

namespace Dijkstra.NET.Utility
{
    internal class NodeComparer : IComparer<uint>
    {
        private readonly IDictionary<uint, int> _distance;

        public NodeComparer(IDictionary<uint, int> distance)
        {
            _distance = distance;
        }

        public int Compare(uint x, uint y)
        {
            int xDistance = _distance.ContainsKey(x) ? _distance[x] : Int32.MaxValue;
            int yDistance = _distance.ContainsKey(y) ? _distance[y] : Int32.MaxValue;

            int comparer = xDistance.CompareTo(yDistance);

            if (comparer == 0)
            {
                return x.CompareTo(y);
            }

            return comparer;
        }
    }
}
