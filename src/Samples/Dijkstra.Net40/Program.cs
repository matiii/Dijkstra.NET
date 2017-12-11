using Dijkstra.NET.Contract;
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
        }
    }
}
