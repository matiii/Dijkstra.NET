using System.Collections.Generic;
using Dijkstra.NET.Graph;

namespace Dijkstra.NET.PageRank
{
    public interface IPageRank : INode
    {
        int NumberOfEdges { get; }

        IEnumerable<IPageRank> Parents { get; }
    }
}