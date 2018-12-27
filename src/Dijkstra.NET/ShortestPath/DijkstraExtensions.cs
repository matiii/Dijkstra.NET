using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dijkstra.NET.Graph;

namespace Dijkstra.NET.ShortestPath
{
    public static class DijkstraExtensions
    {
        /// <summary>
        /// Get path from @from to @to
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="from">Start node</param>
        /// <param name="to">End node</param>
        /// <returns>Value with path</returns>
        public static ShortestPathResult Dijkstra<T, TEdgeCustom>(this IGraph<T, TEdgeCustom> graph, uint from, uint to)
            where TEdgeCustom : IEquatable<TEdgeCustom> => Dijkstra(graph, from, to, Int32.MaxValue);

        /// <summary>
        /// Get path from @from to @to
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="from">Start node</param>
        /// <param name="to">End node</param>
        /// <param name="depth">Depth of path</param>
        /// <returns>Value with path</returns>
        public static ShortestPathResult Dijkstra<T, TEdgeCustom>(this IGraph<T, TEdgeCustom> graph, uint from, uint to,
            int depth)
            where TEdgeCustom : IEquatable<TEdgeCustom>
        {
            return ShortestPath.Dijkstra.GetShortestPath(graph, from, to, depth);
        }
        
        /// <summary>
        /// Get path from @from to @to
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="from">Start node</param>
        /// <param name="to">End node</param>
        /// <returns>Value with path</returns>
        public static ShortestPathResult DijkstraBinary<T, TEdgeCustom>(this IGraph<T, TEdgeCustom> graph, uint from, uint to)
            where TEdgeCustom : IEquatable<TEdgeCustom> => DijkstraBinary(graph, from, to, Int32.MaxValue);
        
        /// <summary>
        /// Get path from @from to @to
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="from">Start node</param>
        /// <param name="to">End node</param>
        /// <param name="depth">Depth of path</param>
        /// <returns>Value with path</returns>
        public static ShortestPathResult DijkstraBinary<T, TEdgeCustom>(this IGraph<T, TEdgeCustom> graph, uint from, uint to,
            int depth)
            where TEdgeCustom : IEquatable<TEdgeCustom>
        {
            var p1 = Task.Factory.StartNew(() => ShortestPath.Dijkstra.GetShortestPath(graph, from, to, depth, 2, 0));
            var p2 = Task.Factory.StartNew(() => ShortestPath.Dijkstra.GetShortestPath(graph, from, to, depth, 2, 1));

            Task.WaitAll(p1, p2);

            return p1.Result.Distance < p2.Result.Distance ? p1.Result : p2.Result;
        }
    }
}