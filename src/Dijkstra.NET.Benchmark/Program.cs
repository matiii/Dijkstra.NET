using BenchmarkDotNet.Running;

namespace Dijkstra.NET.Benchmark
{
    class Program
    {
        static void Main()
        {
            var summary = BenchmarkRunner.Run<BenchmarkIt>();
        }
    }
}
