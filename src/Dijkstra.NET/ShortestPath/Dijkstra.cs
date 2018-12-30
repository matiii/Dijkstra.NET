using System;
using System.Collections.Generic;
using Dijkstra.NET.Graph;

namespace Dijkstra.NET.ShortestPath
{
    internal static class Dijkstra
    {
        public static ShortestPathResult GetShortestPath<T, TEdgeCustom>(IGraph<T, TEdgeCustom> graph, uint from, uint to,
            int depth, int modulo = 1, int r = 0)
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
                    if (e.Node.Key % modulo == r && Distance(e.Node.Key) > Distance(u) + e.Cost)
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