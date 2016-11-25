namespace Dijkstra.NET.Benchmark
{
    using Contract;
    using Model;
    using ShortestPath;

    public class DijkstraBenchmark: DijkstraBenchmarkBase
    {

        public DijkstraBenchmark()
        {
        }

        public DijkstraBenchmark(Graph<int, string> graph, uint from, uint to) : base(graph, from, to)
        {
        }

        public override IShortestPathResult GetPath()
        {
            var dijkstra = new Dijkstra<int, string>(_graph);
            return dijkstra.Process(_from, _to);
        }
        
    }
}
