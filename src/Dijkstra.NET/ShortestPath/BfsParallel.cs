using System;
using Dijkstra.NET.Contract;
using Dijkstra.NET.Model;
using Dijkstra.NET.ShortestPath.Model;
using Dijkstra.NET.ShortestPath.Utility;

namespace Dijkstra.NET.ShortestPath
{
    /// <summary>
    /// Find the shortest path
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEdgeCustom"></typeparam>
    [Obsolete("BFS paraller version will not be supported any more and removed in next release.")]
    public class BfsParallel<T, TEdgeCustom> : Dijkstra<T, TEdgeCustom> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly ProducerConsumer<T, TEdgeCustom> _table;

        private BfsConcurrentResult _result;

        /// <summary>
        /// Contstructor
        /// </summary>
        /// <param name="graph">Find the short path based on graph</param>
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
                    node.EachChild((in Edge<T, TEdgeCustom> e) =>
                    {
                        _table.Consume(new MapReduceJob(node.Key, e.Node.Key, node.Distance, e.Cost));
                    });
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
