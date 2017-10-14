using Dijkstra.NET.Contract;
using Dijkstra.NET.Model;

namespace Dijkstra.NET.Benchmark
{
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
            var dijkstra = new ShortestPath.Dijkstra<int, string>(_graph);
            return dijkstra.Process(_from, _to);
        }
        
    }
}
