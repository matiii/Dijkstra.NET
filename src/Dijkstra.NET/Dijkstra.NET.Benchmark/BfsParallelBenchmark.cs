namespace Dijkstra.NET.Benchmark
{
    using Contract;
    using Model;
    using ShortestPath;

    public class BfsParallelBenchmark: DijkstraBenchmarkBase
    {
        public BfsParallelBenchmark()
        {
        }

        public BfsParallelBenchmark(Graph<int, string> graph, uint from, uint to): base(graph, from, to)
        {
        }

        public override IShortestPathResult GetPath()
        {
            var bfs = new BfsParallel<int, string>(_graph);
            return bfs.Process(_from, _to);
        }
    }
}
