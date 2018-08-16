using Dijkstra.NET.Extensions;
using Dijkstra.NET.Model;

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

            var immutablePath = graph.Dijkstra(0, 1);
        }
    }
}
