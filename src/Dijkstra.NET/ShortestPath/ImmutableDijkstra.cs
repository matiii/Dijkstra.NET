using System;
using System.Collections.Generic;
using Dijkstra.NET.Contract;
using Dijkstra.NET.Extensions;
using Dijkstra.NET.Model;
using Dijkstra.NET.Utility;

namespace Dijkstra.NET.ShortestPath
{
    public class ImmutableDijkstra<T, TEdgeCustom> : Dijkstra<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        public ImmutableDijkstra(IGraph<T, TEdgeCustom> graph) : base(graph)
        {
        }

        public override IShortestPathResult Process(uint @from, uint to)
        {
            var result = new DijkstraResult(from, to);
            Graph[from].Distance = 0;
            var q = new SortedSet<INode<T, TEdgeCustom>>(new[] { Graph[from] }, new NodeComparer<T, TEdgeCustom>());
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

                for (int i = 0; i < u.Children.Count; i++)
                {
                    Edge<T, TEdgeCustom> e = u.Children[i];

                    if (e.Node.Distance > u.Distance + e.Cost)
                    {
                        if (current.Contains(e.Node.Key))
                            q.Remove(e.Node);

                        e.Node.Distance = u.Distance + e.Cost;
                        q.Add(e.Node);
                        current.Add(e.Node.Key);
                        result.Path[e.Node.Key] = u.Key;
                    }
                }
            }

            return result;
        }
    }
}