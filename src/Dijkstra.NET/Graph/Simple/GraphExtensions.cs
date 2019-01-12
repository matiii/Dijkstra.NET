using System.Linq;
using Dijkstra.NET.Extensions;

namespace Dijkstra.NET.Graph.Simple
{
    public static class GraphExtensions
    {
        /// <summary>
        /// Create graph with nodes
        /// </summary>
        /// <param name="nodes">Number of nodes</param>
        /// <returns>Graph(nodes)</returns>
        public static Graph ToGraph(this int nodes)
        {
            var g = new Graph();
            Enumerable.Range(0, nodes).Each(_ => g.AddNode());
            return g;
        }
    }
}