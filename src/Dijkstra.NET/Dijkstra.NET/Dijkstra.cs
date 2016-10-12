namespace Dijkstra.NET
{
    using System.Collections.Generic;
    using Contract;
    using Extensions;
    using Model;
    using Utility;

    public class Dijkstra<T, TEdgeCustom> where TEdgeCustom: class
    {
        private readonly IGraph<T, TEdgeCustom> _graph;

        public Dijkstra(IGraph<T, TEdgeCustom> graph)
        {
            _graph = graph;
        }

        public DijkstraResult Process(uint from, uint to)
        {
            var result = new DijkstraResult(from, to);
            _graph[from].Distance = 0;
            var q = new SortedSet<INode<T, TEdgeCustom>>(new[] {_graph[from]}, new NodeComparer<T, TEdgeCustom>());

            while (q.Count > 0)
            {
                INode<T, TEdgeCustom> u = q.Deque();

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
                        e.Node.Distance = u.Distance + e.Cost;
                        q.Add(e.Node);
                        result.Path[e.Node.Key] = u.Key;
                    }
                }
            }

            return result;
        }
    }
}
