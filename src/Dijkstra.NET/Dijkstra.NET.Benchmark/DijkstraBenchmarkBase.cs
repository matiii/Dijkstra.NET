namespace Dijkstra.NET.Benchmark
{
    using System;
    using Model;

    public class DijkstraBenchmarkBase
    {
        protected readonly Graph<int, string> _graph = new Graph<int, string>();

        private const int Nodes = 1000000;
        private const int Connections = 10000000;

        protected uint from;
        protected uint to;

        public void Setup()
        {
            for (int i = 0; i < Nodes; i++)
                _graph.AddNode(i);

            var random = new Random();

            for (int i = 0; i < Connections; i++)
            {
                int node1 = random.Next(1, Nodes);
                int node2 = random.Next(1, Nodes);
                int cost = random.Next(1, 50);

                _graph.Connect((uint) node1, (uint) node2, (uint) cost, null);
            }

            _graph.Connect(0, 5, 10, null);
            _graph.Connect(5, 121, 2, null);
            _graph.Connect(121, 115, 1, null);
            _graph.Connect(115, 300, 4, null);
            _graph.Connect(300, 855, 2, null);
            _graph.Connect(855, 1600, 1, null);
            _graph.Connect(1600, 5000, 4, null);
            _graph.Connect(5000, 50, 1, null);
            _graph.Connect(50, 21, 1, null);
            _graph.Connect(21, Nodes-1, 1, null);

            @from = 0;
            to = Nodes-1;
        }
    }
}
