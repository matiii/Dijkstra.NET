using Dijkstra.NET.Contract;
using Dijkstra.NET.ShortestPath;

namespace Dijkstra.NET.Benchmark
{
    public class BfsParallelBenchmark: DijkstraBenchmarkBase
    {
        public override IShortestPathResult GetPath()
        {
            var bfs = new BfsParallel<int, string>(Graph);
            return bfs.Process(From, To);
        }
    }
}
