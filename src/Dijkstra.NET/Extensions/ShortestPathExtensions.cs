using System;
using System.Collections.Generic;
using Dijkstra.NET.Contract;
using Dijkstra.NET.Model;
using Dijkstra.NET.Utility;

namespace Dijkstra.NET.Extensions
{
    public static class ShortestPathExtensions
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
            var path = new Dictionary<uint, uint>();
            var distance = new Dictionary<uint, int> {[from] = 0};
            var d = new Dictionary<uint, int> {[from] = 0};
            var q = new SortedSet<uint>(new[] {from}, new NodeComparer(distance));
            var current = new HashSet<uint>();

            int Distance(uint key)
            {
                return distance.ContainsKey(key) ? distance[key] : Int32.MaxValue;
            }

            do
            {
                uint u = q.Deque();

                if (u == to)
                {
                    return new ShortestPathResult(from, to, distance[u], path);
                }

                current.Remove(u);

                if (depth == d[u])
                {
                    continue;
                }

                graph[u].EachChild((in Edge<T, TEdgeCustom> e) =>
                {
                    if (Distance(e.Node.Key) > Distance(u) + e.Cost)
                    {
                        if (current.Contains(e.Node.Key))
                        {
                            q.Remove(e.Node.Key);
                        }

                        distance[e.Node.Key] = Distance(u) + e.Cost;
                        q.Add(e.Node.Key);
                        current.Add(e.Node.Key);
                        path[e.Node.Key] = u;
                        d[e.Node.Key] = d[u] + 1;
                    }
                });

            } while (q.Count > 0 && depth > 0);

            return new ShortestPathResult(from, to);
        }
    }
}