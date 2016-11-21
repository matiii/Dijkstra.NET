namespace Dijkstra.NET.ShortestPath
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Contract;
    using Model;
    using NET.Model;
    using Utility;

    public class BfsParallel<T, TEdgeCustom> : Dijkstra<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly ProducerConsumer<T, TEdgeCustom> _table;
        private readonly IConcurrentGraph<T, TEdgeCustom> _graph;

        private DijkstraConcurrentResult _result;

        public BfsParallel(IConcurrentGraph<T, TEdgeCustom> graph) : base(graph)
        {
            _graph = graph;
            _table = new ProducerConsumer<T, TEdgeCustom>();
        }

        public override IShortestPathResult Process(uint @from, uint to)
        {
            _result = new DijkstraConcurrentResult(from, to);

            IConcurrentNode<T, TEdgeCustom> node = _graph.GetConccurentNode(from);
            node.Distance = 0;
            _table.Produce(node);

            var map = new Task(Map);
            var reduce = new Task(Reduce);

            map.Start();
            reduce.Start();

            _table.StartGuard();

            Task.WaitAll(map, reduce);

            _result.Distance = _graph[to].Distance;

            return _result;
        }

        private void Map()
        {
            _table.Producing(node =>
            {
                if (node.Key != _result.ToNode && node.Children.Count > 0)
                {
                    for (int i = 0; i < node.Children.Count; i++)
                    {
                        Edge<T, TEdgeCustom> e = node.Children[i];
                        _table.Consume(new MapReduceJob(node.Key, e.Node.Key, node.Distance, e.Cost));
                    }
                }
                else
                    _table.NotifyGuard();
            });
        }

        private void Reduce()
        {
            _table.Consuming(job =>
            {
                if (Reduce(_graph.GetConccurentNode(job.To), job.Distance))
                {
                    _result.P.AddOrUpdate(job.To, job.From, (u, u1) => job.From);
                    _table.Produce(_graph.GetConccurentNode(job.To));
                }
                else
                    _table.NotifyGuard();
            });
        }

        private bool Reduce(IConcurrentNode<T, TEdgeCustom> to, int distance)
        {
            var spin = new SpinWait();

            while (true)
            {
                int initialDistance = to.Distance;

                if (initialDistance > distance)
                {
                    if (to.TrySetDistance(distance, initialDistance))
                        return true;

                    spin.SpinOnce();
                }
                else
                    return false;
            }
        }
    }
}
