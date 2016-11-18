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
            var normal = new  DijkstraBenchmark();
            normal.Setup();

            var stopWatch = Stopwatch.StartNew(); 
            IShortPathResult result = normal.GetPath();
            stopWatch.Stop();

            uint[] path = result.GetPath().ToArray();

            Console.WriteLine($"Dijkstra takes {stopWatch.ElapsedMilliseconds} ms. Length of path is {path.Length}.");
            Console.WriteLine($"Path: {path.Select(x => x.ToString()).Aggregate((a, b) => a + " -> " + b)}");

            Console.ReadKey();
        }
    }
}
