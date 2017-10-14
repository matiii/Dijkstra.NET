using Dijkstra.NET.Contract;
using Dijkstra.NET.Model;
using Dijkstra.NET.ShortestPath;

namespace Dijkstra.NET.Benchmark
{
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
