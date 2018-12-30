using System.Collections.Generic;
using System.Linq;

namespace Dijkstra.NET.PageRank
{
    public static class PageRankExtensions
    {
        public static PageRankResult CalculatePageRank(this IPageRankGraph graph, double d = 0.85)
        {
            var pageRank0 = new Dictionary<uint, double>();
            var pageRank1 = new Dictionary<uint, double>();
            var pageRank2 = new Dictionary<uint, double>();
            
            // 0
            foreach (var node in graph)
            {
                pageRank0[node.Key] = 1.0 / graph.NodesCount;
            }

            // 1
            foreach (var node in graph)
            {
                pageRank1[node.Key] = (1 - d) / graph.NodesCount + d * node.Parents.Sum(x => pageRank0[x.Key] / x.NumberOfEdges);
            }

            // 2
            foreach (var node in graph)
            {
                pageRank2[node.Key] = (1 - d) / graph.NodesCount + d * node.Parents.Sum(x => pageRank1[x.Key] / x.NumberOfEdges);
            }

            return new PageRankResult(pageRank2);
        }
    }
}