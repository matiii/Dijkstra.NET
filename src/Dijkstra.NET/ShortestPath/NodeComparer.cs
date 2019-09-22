using System;
using System.Collections.Generic;

namespace Dijkstra.NET.ShortestPath
{
    internal class NodeComparer : IComparer<uint>
    {
        private readonly IDictionary<uint, int> _distance;
        private readonly Func<uint, uint, int> _heuristic;

        public NodeComparer(IDictionary<uint, int> distance)
        {
            _distance = distance;
            _heuristic = null;
        }
        public NodeComparer(IDictionary<uint, int> distance, Func<uint, uint, int> heuristic)
        {
            _distance = distance;
            _heuristic = heuristic;
        }

        public int Compare(uint x, uint y)
        {
            int xDistance = _distance.ContainsKey(x) ?
                _heuristic is null ? _distance[x] : _distance[x] + _heuristic(x, y)
                : Int32.MaxValue;
            int yDistance = _distance.ContainsKey(y) ?
                _heuristic is null ? _distance[y] : _distance[y] + _heuristic(y, x)
                : Int32.MaxValue;

            int comparer = xDistance.CompareTo(yDistance);

            if (comparer == 0)
            {
                return x.CompareTo(y);
            }

            return comparer;
        }
    }
}
