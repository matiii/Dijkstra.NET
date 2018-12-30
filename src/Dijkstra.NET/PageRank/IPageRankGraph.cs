using System.Collections.Generic;

namespace Dijkstra.NET.PageRank
{
    public interface IPageRankGraph : IEnumerable<IPageRank>
    {
        IPageRank this[uint node] { get; }
        
        int NodesCount { get; }
    }
}