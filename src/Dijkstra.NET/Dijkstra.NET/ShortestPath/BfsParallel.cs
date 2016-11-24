namespace Dijkstra.NET.ShortestPath
{
    using System;
    using System.Diagnostics;
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

        public BfsParallel(IConcurrentGraph<T, TEdgeCustom> graph, double guardInterval) : base(graph)
        {
            _graph = graph;
            _table = new ProducerConsumer<T, TEdgeCustom>(guardInterval);
        }

        public override IShortestPathResult Process(uint @from, uint to)
        {
            _result = new DijkstraConcurrentResult(from, to);

            IConcurrentNode<T, TEdgeCustom> nodeFrom = _graph.GetConccurentNode(from);
            nodeFrom.Distance = 0;

            _table.Initialise = () => _table.Produce(nodeFrom);

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
                if (Reduce(job.From,_graph.GetConccurentNode(job.To), job.Distance))
                {
                    Debug.WriteLineIf(job.To == 3, $"Update ({job.From})->({job.To}) [{job.Distance}]");
                    //_result.P.AddOrUpdate(job.To, job.From, (u, u1) =>
                    //{
                    //    return job.From;
                    //});

                    _table.Produce(_graph.GetConccurentNode(job.To));
                }
            };

            _table.Work();

            _result.Distance = _graph[to].Distance;

            return _result;
        }
        private object _locker = new object();
        private object _locker2 = new object();
        public void SetInsurance(int insurance)
        {
            _table.Insurance = insurance;
        }

        private bool Reduce(uint from, IConcurrentNode<T, TEdgeCustom> to, int distance)
        {
            lock (to)
            {
                if (to.Distance > distance)
                {
                    to.Distance = distance;
                    _result.Path[to.Key] = from;

                    return true;
                }

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
