using System;
using System.Linq;
using Dijkstra.NET.Contract;
using Dijkstra.NET.Extensions;
using Dijkstra.NET.Model;
using Dijkstra.NET.ShortestPath;

namespace Dijkstra.Net40
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph<int, string>();

            graph.AddNode(1);
            graph.AddNode(2);

            graph.Connect(0, 1, 5, "some custom information in edge"); //First node has key equal 0

            var dijkstra = new Dijkstra<int, string>(graph);
            IShortestPathResult result = dijkstra.Process(0, 1); //result contains the shortest path

            var path = result.GetPath();

            graph.Reset();

            var immutablePath = graph.Dijkstra(0, 1);

            var bfs = new BfsParallel<int, string>(graph);

            IShortestPathResult bfsResult = bfs.Process(0, 1);

            var bfsPath = bfsResult.GetPath();

            if (!bfsPath.SequenceEqual(path) || !immutablePath.GetPath().SequenceEqual(path))
            {
                throw new Exception("The path should be the same.");
            }
        }
    }
}
