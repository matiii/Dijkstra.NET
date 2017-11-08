using Dijkstra.NET.Contract;

namespace Dijkstra.NET.Benchmark
{
    public class DijkstraBenchmark: DijkstraBenchmarkBase
    {
        public override IShortestPathResult GetPath()
        {
            var dijkstra = new ShortestPath.Dijkstra<int, string>(Graph);
            return dijkstra.Process(From, To);
        }
    }
}
