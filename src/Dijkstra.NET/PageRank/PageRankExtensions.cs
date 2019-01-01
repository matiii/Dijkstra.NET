using System.Collections.Generic;
using System.Linq;

namespace Dijkstra.NET.PageRank
{
    public static class PageRankExtensions
    {
        /// <summary>
        /// Calculate page rank for graph
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="d">Damping factor</param>
        /// <returns>Calculated page rank</returns>
        public static PageRankResult CalculatePageRank(this IPageRankGraph graph, double d = 0.85)
        {
            var pageRank = new Dictionary<uint, double>();
            var pageRankNext = new Dictionary<uint, double>();
            
            // 0
            foreach (var node in graph)
            {
                pageRank[node.Key] = 1.0 / graph.NodesCount;
            }

            // 1
            foreach (var node in graph)
            {
                pageRankNext[node.Key] = (1 - d) / graph.NodesCount + d * node.Parents.Sum(x => pageRank[x.Key] / x.NumberOfEdges);
            }

            // 2
            foreach (var node in graph)
            {
                pageRank[node.Key] = (1 - d) / graph.NodesCount + d * node.Parents.Sum(x => pageRankNext[x.Key] / x.NumberOfEdges);
            }

            return new PageRankResult(pageRank);
        }
    }
}