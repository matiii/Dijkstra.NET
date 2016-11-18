namespace Dijkstra.NET.Benchmark
{
    using Contract;
    using ShortPath;

    public class DijkstraBenchmark: DijkstraBenchmarkBase
    {
        public IShortPathResult GetPath()
        {
            var dijkstra = new Dijkstra<int, string>(_graph);
            return dijkstra.Process(@from, to);
        }
        
    }
}
