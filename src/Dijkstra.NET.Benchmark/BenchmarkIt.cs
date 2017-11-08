using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Engines;

namespace Dijkstra.NET.Benchmark
{
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1, warmupCount: 2, targetCount: 3)]
    public class BenchmarkIt
    {
        private readonly DijkstraBenchmarkBase _dijkstra;
        private readonly DijkstraBenchmarkBase _bfs;

        public BenchmarkIt()
        {
            _dijkstra = new DijkstraBenchmark();
            _bfs = new BfsParallelBenchmark();
        }

        [GlobalSetup]
        public void Initialise()
        {
            Console.WriteLine("--- Global Setup ---");

            _dijkstra.Initialise();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            Console.WriteLine("--- Iteration Setup ---");

            _dijkstra.Setup();
        }

        [Benchmark(Baseline = true)]
        public int DijkstraBenchmark()
        {
            var result = _dijkstra.GetPath();

            return result.GetPath().Count();
        }

        [Benchmark]
        public int BfsBenchmark()
        {
            var result = _bfs.GetPath();

            return result.GetPath().Count();
        }
    }
}