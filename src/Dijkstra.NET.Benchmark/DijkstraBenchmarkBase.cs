using System;
using Dijkstra.NET.Contract;
using Dijkstra.NET.Model;

namespace Dijkstra.NET.Benchmark
{
    public abstract class DijkstraBenchmarkBase
    {
        protected static readonly Graph<int, string> Graph = new Graph<int, string>();

        private const int Nodes = 10_000_000;
        private const int Connections = 1_000_000;

        public uint From { get; private set; }
        public uint To { get; private set; }

        public void Initialise()
        {
            for (int i = 0; i < Nodes; i++)
                Graph.AddNode(i);

            var random = new Random();

            for (int i = 0; i < Connections; i++)
            {
                int node1 = random.Next(0, Nodes);
                int node2 = random.Next(0, Nodes);
                int cost = random.Next(15, 50);

                Graph.Connect((uint)node1, (uint)node2, cost, null);
            }

            Graph.Connect(0, 5, 10000000, null);
            Graph.Connect(5, 121, 10000000, null);
            Graph.Connect(121, 115, 10000000, null);
            Graph.Connect(115, 300, 10000000, null);
            Graph.Connect(300, 855, 10000000, null);
            Graph.Connect(855, 1600, 10000000, null);
            Graph.Connect(1600, 5000, 10000000, null);
            Graph.Connect(5000, 50, 10000000, null);
            Graph.Connect(50, 21, 10000000, null);
            Graph.Connect(21, Nodes - 1, 10000000, null);

            From = 0;
            To = Nodes - 1;
        }

        public void Setup()
        {
            Graph.Reset();
        }

        public abstract IShortestPathResult GetPath();
    }
}
