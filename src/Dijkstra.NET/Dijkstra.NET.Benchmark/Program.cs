namespace Dijkstra.NET.Benchmark
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Model;

    class Program
    {
        static void Main(string[] args)
        {
            var normal = new  DijkstraBenchmark();
            normal.Setup();

            var stopWatch = Stopwatch.StartNew(); 
            DijkstraResult result = normal.GetPath();
            stopWatch.Stop();

            Console.WriteLine($"Dijkstra takes {stopWatch.Elapsed.TotalSeconds:F} sec. Length of path is {result.GetPath().Count()}.");

            Console.ReadKey();
        }
    }
}
