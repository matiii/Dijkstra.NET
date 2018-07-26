using System;
using System.Collections.Generic;
using Dijkstra.NET.Contract;
using Dijkstra.NET.Extensions;
using Dijkstra.NET.Model;
using Dijkstra.NET.Utility;

namespace Dijkstra.NET.ShortestPath
{
    /// <summary>
    /// Find the shortest path @from @to
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEdgeCustom"></typeparam>
    [Obsolete("Please use Dijkstra.NET.Extensions.Dijkstra method instead.")]
    public class Dijkstra<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        protected readonly IGraph<T, TEdgeCustom> Graph;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graph">Find the shortest path based on graph</param>
        public Dijkstra(IGraph<T, TEdgeCustom> graph)
        {
            Graph = graph;
        }

        /// <summary>
        /// Get path from @from to @to
        /// </summary>
        /// <param name="from">Start node</param>
        /// <param name="to">End node</param>
        /// <returns>Value with path</returns>
        public virtual IShortestPathResult Process(uint from, uint to)
        {
            var result = new DijkstraResult(from, to);
            Graph[from].Distance = 0;
            var q = new SortedSet<INode<T, TEdgeCustom>>(new[] { Graph[from]}, new NodeComparer<T, TEdgeCustom>());
            var current = new HashSet<uint>();

            while (q.Count > 0)
            {
                INode<T, TEdgeCustom> u = q.Deque();
                current.Remove(u.Key);

                if (u.Key == to)
                {
                    result.Distance = u.Distance;
                    break;
                }

                u.EachChild((in Edge<T, TEdgeCustom> e) =>
                {
                    if (e.Node.Distance > u.Distance + e.Cost)
                    {
                        if (current.Contains(e.Node.Key))
                            q.Remove(e.Node);

                        e.Node.Distance = u.Distance + e.Cost;
                        q.Add(e.Node);
                        current.Add(e.Node.Key);
                        result.Path[e.Node.Key] = u.Key;
                    }
                });
            }

            return result;
        }
    }
}
