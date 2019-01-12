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
        public static PageRankResult PageRank(this IPageRankGraph graph, double d = 0.85)
        {
            var pageRank = new Dictionary<uint, double>();
            var pageRankNext = new Dictionary<uint, double>();

            double initPr = 1.0 / graph.NodesCount;

            // 1
            foreach (var node in graph)
            {
                pageRankNext[node] = (1 - d) / graph.NodesCount + d * graph.Parents(node).Sum(x => initPr / graph.EdgesCount(x));
            }

            // 2
            foreach (var node in graph)
            {
                pageRank[node] = (1 - d) / graph.NodesCount + d * graph.Parents(node).Sum(x => pageRankNext[x] / graph.EdgesCount(x));
            }

            return new PageRankResult(pageRank);
        }
    }
}