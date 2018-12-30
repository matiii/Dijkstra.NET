using System.Collections.Generic;

namespace Dijkstra.NET.PageRank
{
    public struct PageRankResult
    {
        private readonly Dictionary<uint, double> _pageRank;

        public PageRankResult(Dictionary<uint, double> pageRank)
        {
            _pageRank = pageRank;
        }

        public double this[uint node] => _pageRank[node];
    }
}