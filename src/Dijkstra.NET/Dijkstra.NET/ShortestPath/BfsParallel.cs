namespace Dijkstra.NET.ShortestPath
{
    using System;
    using System.Threading;
    using Contract;
    using Model;
    using NET.Model;
    using Utility;

    public class BfsParallel<T, TEdgeCustom> : Dijkstra<T, TEdgeCustom>, IDisposable where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly ProducerConsumer<T, TEdgeCustom> _table;
        private readonly IConcurrentGraph<T, TEdgeCustom> _graph;

        private DijkstraConcurrentResult _result;

        private bool _disposed;

        public BfsParallel(IConcurrentGraph<T, TEdgeCustom> graph) : base(graph)
        {
            _graph = graph;
            _table = new ProducerConsumer<T, TEdgeCustom>();
        }

        public override IShortestPathResult Process(uint @from, uint to)
        {
            _result = new DijkstraConcurrentResult(from, to);

            IConcurrentNode<T, TEdgeCustom> nodeFrom = _graph.GetConccurentNode(from);
            nodeFrom.Distance = 0;
            _table.Produce(nodeFrom);

            _table.Producing = node =>
            {
                if (node.Key != _result.ToNode)
                {
                    for (int i = 0; i < node.Children.Count; i++)
                    {
                        Edge<T, TEdgeCustom> e = node.Children[i];
                        _table.Consume(new MapReduceJob(node.Key, e.Node.Key, node.Distance, e.Cost));
                    }
                }
            };

            _table.Consuming = job =>
            {
                if (Reduce(_graph.GetConccurentNode(job.To), job.Distance))
                {
                    _result.P.AddOrUpdate(job.To, job.From, (u, u1) => job.From);
                    _table.Produce(_graph.GetConccurentNode(job.To));
                }
            };

            _table.Work();

            _result.Distance = _graph[to].Distance;

            return _result;
        }

        public void SetInsurance(int insurance)
        {
            _table.Insurance = insurance;
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

        public void Dispose()
        {
            if (_disposed)
                return;

            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _table.Dispose();
        }
    }
}
