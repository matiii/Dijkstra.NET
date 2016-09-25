namespace Dijkstra.NET
{
    using System.Collections.Generic;
    using Contract;
    using Extensions;
    using Model;
    using Utility;

    public class Dijkstra<T>
    {
        private readonly IGraph<T> _graph;

        public Dijkstra(IGraph<T> graph)
        {
            _graph = graph;
        }

        public DijkstraResult Process(uint from, uint to)
        {
            var result = new DijkstraResult(from, to);
            _graph[from].Distance = 0;
            var q = new SortedSet<INode<T>>(_graph, new NodeComparer<T>());

            while (q.Count > 0)
            {
                INode<T> u = q.Deque();

                if (u.Key == to)
                {
                    result.Distance = u.Distance;
                    break;
                }

                for (int i = 0; i < u.Children.Count; i++)
                {
                    IEdge<T> e = u.Children[i];
                    
                    if (e.Node.Distance > u.Distance + e.Cost)
                    {
                        q.Remove(e.Node);
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
