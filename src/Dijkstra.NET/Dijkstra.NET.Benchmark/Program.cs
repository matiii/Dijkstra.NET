namespace Dijkstra.NET.Benchmark
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Contract;

    class Program
    {
        static void Main()
        {
            var bfs = new BfsParallelBenchmark();
            Measure(bfs);

            GC.Collect();

            Measure(new DijkstraBenchmark(bfs.Graph, bfs.From, bfs.To));
            Console.ReadKey();
        }

        static void Measure(DijkstraBenchmarkBase benchmark)
        {
            string benchmarkName = benchmark.GetType().Name;

            Console.WriteLine($"--- Start {benchmarkName} benchmark. --- \r\n");
            benchmark.Setup();

            var stopWatch = Stopwatch.StartNew();
            IShortestPathResult result = benchmark.GetPath();
            stopWatch.Stop();

            uint[] path = result.GetPath().ToArray();

            Console.WriteLine($"{benchmarkName} takes {stopWatch.ElapsedMilliseconds} ms. Length of path is {path.Length}.");
            Console.WriteLine($"Path: {path.Select(x => x.ToString()).Aggregate((a, b) => a + " -> " + b)}");
            Console.WriteLine($"--- End {benchmarkName} benchmark. --- \r\n");
        }
    }
}
