namespace Dijkstra.NET.Benchmark
{
    using System;
    using Contract;
    using Model;

    public abstract class DijkstraBenchmarkBase
    {
        protected readonly Graph<int, string> _graph;

        private const int Nodes = 10000000;
        private const int Connections = 10000000;

        private const int ConnectionLevel = 300000;

        protected uint _from;
        protected uint _to;

        private bool _graphFromCtor;

        protected DijkstraBenchmarkBase()
        {
            _graph = new Graph<int, string>();
        }

        protected DijkstraBenchmarkBase(Graph<int, string> graph, uint from, uint to)
        {
            _graph = graph;
            _from = from;
            _to = to; 
            _graphFromCtor = true;
        }

        public Graph<int, string> Graph => _graph;
        public uint From => _from;
        public uint To => _to;


        public void Setup()
        {
            if (_graphFromCtor)
            {
                _graph.Reset();
                return;
            }

            for (int i = 0; i < Nodes; i++)
                _graph.AddNode(i);

            var random = new Random();

            for (int i = 0; i < Connections; i++)
            {
                int node1 = random.Next(1, Nodes);
                int node2 = random.Next(1, Nodes);
                int cost = random.Next(15, 50);

                _graph.Connect((uint) node1, (uint) node2, cost, null);
            }

            for (uint i = 0; i < ConnectionLevel; i++)
            {
                int cost = random.Next(1, 5);
                _graph.Connect(0, i + 1, cost, null);
            }

            for (uint i = 0; i < ConnectionLevel; i++)
            {
                int node1 = random.Next(1, ConnectionLevel);
                int node2 = random.Next(ConnectionLevel, ConnectionLevel*2);
                int cost = random.Next(1, 5);

                _graph.Connect((uint) node1, (uint) node2, cost, null);
            }

            for (uint i = 0; i < ConnectionLevel; i++)
            {
                int node1 = random.Next(ConnectionLevel, 2*ConnectionLevel);
                int node2 = random.Next(2*ConnectionLevel, 3*ConnectionLevel);
                int cost = random.Next(1, 5);

                _graph.Connect((uint) node1, (uint) node2, cost, null);
            }

            _graph.Connect(0, 5, 10000000, null);
            _graph.Connect(5, 121, 10000000, null);
            _graph.Connect(121, 115, 10000000, null);
            _graph.Connect(115, 300, 10000000, null);
            _graph.Connect(300, 855, 10000000, null);
            _graph.Connect(855, 1600, 10000000, null);
            _graph.Connect(1600, 5000, 10000000, null);
            _graph.Connect(5000, 50, 10000000, null);
            _graph.Connect(50, 21, 10000000, null);
            _graph.Connect(21, Nodes-1, 10000000, null);

            _from = 0;
            _to = Nodes-1;
        }

        public abstract IShortestPathResult GetPath();
    }
}
