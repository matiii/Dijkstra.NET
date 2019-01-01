using System;
using Dijkstra.NET.Graph;

namespace Dijkstra.NET.Benchmark
{
    public abstract class BenchmarkBase
    {
        public  Graph<int, string> Graph { get; set; }

        private const int Nodes = 1_000_000;
        private const int Connections = 1_000_000;

        public static uint First { get; private set; }
        public static uint Last { get; private set; }

        public void InitialiseGraph()
        {
            if (Graph != null)
            {
                return;
            }
            
            Graph = new Graph<int, string>();
            
            for (int i = 0; i < Nodes; i++)
                Graph.AddNode(i);

            var random = new Random();

            for (int i = 0; i < Connections; i++)
            {
                int node1 = random.Next(1, Nodes);
                int node2 = random.Next(2, Nodes);
                int cost = random.Next(15, 50);

                Graph.Connect((uint)node1, (uint)node2, cost, null);
            }

            Graph.Connect(1, 5, 10000000, null);
            Graph.Connect(5, 121, 10000000, null);
            Graph.Connect(121, 115, 10000000, null);
            Graph.Connect(115, 300, 10000000, null);
            Graph.Connect(300, 855, 10000000, null);
            Graph.Connect(855, 1600, 10000000, null);
            Graph.Connect(1600, 5000, 10000000, null);
            Graph.Connect(5000, 50, 10000000, null);
            Graph.Connect(50, 21, 10000000, null);
            Graph.Connect(21, Nodes - 1, 10000000, null);

            First = 1;
            Last = Nodes - 1;
        }
    }
}
