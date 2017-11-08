using Dijkstra.NET.Contract;
using Dijkstra.NET.ShortestPath;

namespace Dijkstra.NET.Benchmark
{
    public class DijkstraOldBenchmark: DijkstraBenchmarkBase
    {
        public override IShortestPathResult GetPath()
        {
            var dijkstra = new DijkstraOld<int, string>(Graph);
            return dijkstra.Process(From, To);
        }
    }
}