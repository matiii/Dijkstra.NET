using BenchmarkDotNet.Running;

namespace Dijkstra.NET.Benchmark
{
    class Program
    {
        static void Main()
        {
#if DEBUG
            var b = new BenchmarkIt();
            b.Initialise();
            b.DijkstraExtensionBenchmark();
            b.PageRankExtensionBenchmark();
#else
            var summary = BenchmarkRunner.Run<BenchmarkIt>();
#endif
        }
    }
}