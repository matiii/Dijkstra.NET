namespace Dijkstra.NET.ShortestPath
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using Contract;
    using Model;
    using NET.Model;

    public class DijkstraParallel<T, TEdgeCustom>: Dijkstra<T, TEdgeCustom> where TEdgeCustom: IEquatable<TEdgeCustom>
    {
        private readonly BlockingCollection<INode<T, TEdgeCustom>> _mapper = new BlockingCollection<INode<T, TEdgeCustom>>(new ConcurrentBag<INode<T, TEdgeCustom>>()); //todo: concern collection
        private readonly BlockingCollection<MapReduceJob> _reducer = new BlockingCollection<MapReduceJob>(new ConcurrentBag<MapReduceJob>());
        private DijkstraConcurrentResult _result;


        public DijkstraParallel(IGraph<T, TEdgeCustom> graph) : base(graph)
        {
        }

        public override IShortestPathResult Process(uint @from, uint to)
        {
            _result = new DijkstraConcurrentResult(from, to);

            INode<T, TEdgeCustom> node = Graph[from];
            node.Distance = 0;

            _mapper.Add(node);

            var map = new Task(Map);
            var reduce = new Task(Reduce);

            map.Start();
            reduce.Start();

            Task.WaitAll(map, reduce);

            return _result;
        }

        private void Map()
        {
            Parallel.ForEach(_mapper.GetConsumingEnumerable(), node =>
            {
                node.Children.GroupBy(x => x.Node.Key).Select(x => x.First(n => n.Cost == x.Min(z => z.Cost)));

                for (int i = 0; i < node.Children.Count; i++)
                {
                    Edge<T, TEdgeCustom> e = node.Children[i];
                    _reducer.Add(new MapReduceJob(node.Key, e.Node.Key, (int) node.Distance, (int) e.Cost));
                }
            });
        }

        private void Reduce()
        {
            Parallel.ForEach(_reducer.GetConsumingEnumerable(), job =>
            {
                if (Graph[job.To].Distance > job.Distance)
                {
                    Graph[job.To].Distance = (uint) job.Distance;
                    _result.Path[job.To] = job.From;
                    _mapper.Add(Graph[job.To]);
                }      
            });
        }
    }
}
