using System;
using System.Collections.Generic;

namespace Dijkstra.NET.ShortestPath
{
    internal class HeuristicNodeComparer : NodeComparer
    {
        private readonly Func<uint, uint, int> _heuristic;

        public HeuristicNodeComparer(IDictionary<uint, int> distance, Func<uint, uint, int> heuristic) : base(distance)
        {
            _heuristic = heuristic;
        }

        public override int Compare(uint x, uint y)
        {
            int xDistance = _distance.ContainsKey(x) ? _distance[x] + _heuristic(x, y) : Int32.MaxValue;
            int yDistance = _distance.ContainsKey(y) ? _distance[y] + _heuristic(y, x) : Int32.MaxValue;

            int comparer = xDistance.CompareTo(yDistance);

            if (comparer == 0)
            {
                return x.CompareTo(y);
            }

            return comparer;
        }
    }
}
