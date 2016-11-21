namespace Dijkstra.NET.Benchmark
{
    using Contract;
    using ShortestPath;

    public class DijkstraBenchmark: DijkstraBenchmarkBase
    {
        public IShortestPathResult GetPath()
        {
            var dijkstra = new Dijkstra<int, string>(_graph);
            return dijkstra.Process(@from, to);
        }
        
    }
}
