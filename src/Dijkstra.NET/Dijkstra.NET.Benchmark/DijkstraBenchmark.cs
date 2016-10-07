namespace Dijkstra.NET.Benchmark
{
    using Model;

    public class DijkstraBenchmark: DijkstraBenchmarkBase
    {
        public DijkstraResult GetPath()
        {
            var dijkstra = new Dijkstra<int, string>(_graph);
            return dijkstra.Process(@from, to);
        }
        
    }
}
