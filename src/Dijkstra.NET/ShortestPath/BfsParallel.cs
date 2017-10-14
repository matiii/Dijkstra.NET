namespace Dijkstra.NET.ShortestPath
{
    using System;
    using Contract;
    using Model;
    using NET.Model;
    using Utility;

    public class BfsParallel<T, TEdgeCustom> : Dijkstra<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly ProducerConsumer<T, TEdgeCustom> _table;

        private BfsConcurrentResult _result;

        public BfsParallel(IGraph<T, TEdgeCustom> graph) : base(graph)
        {
            _table = new ProducerConsumer<T, TEdgeCustom>();
        }

        public override IShortestPathResult Process(uint @from, uint to)
        {
            _result = new BfsConcurrentResult(from, to);

            INode<T, TEdgeCustom> nodeFrom = Graph[from];
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
                if (Reduce(job.From, Graph[job.To], job.Distance))
                    _table.Produce(Graph[job.To]);
            };

            _table.Work();

            _result.Distance = Graph[to].Distance;

            return _result;
        }

        private bool Reduce(uint from, INode<T, TEdgeCustom> to, int distance)
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
    }
}
