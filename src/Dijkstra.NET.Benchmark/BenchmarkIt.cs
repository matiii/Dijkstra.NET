using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Engines;
using Dijkstra.NET.ShortestPath;

namespace Dijkstra.NET.Benchmark
{
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1, warmupCount: 2, targetCount: 3)]
    [MemoryDiagnoser]
    public class BenchmarkIt
    {
        public BenchmarkIt()
        {
        }

        [GlobalSetup]
        public void Initialise()
        {
            Console.WriteLine("--- Global Setup ---");
        }

        [IterationSetup]
        public void IterationSetup()
        {
            Console.WriteLine("--- Iteration Setup ---");
        }

        [Benchmark(Baseline = true)]
        public int DijkstraExtensionBenchmark()
        {
            var result = DijkstraBenchmarkBase.Graph.Dijkstra(DijkstraBenchmarkBase.From, DijkstraBenchmarkBase.To);

            return result.GetPath().Count();
        }
    }
}