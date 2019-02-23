using System;

namespace Dijkstra.NET.Graph.Simple
{
    public readonly struct EdgeTemp
    {
        internal EdgeTemp(Graph graph, uint nodeFrom)
            : this(graph, nodeFrom, default)
        {
        }

        internal EdgeTemp(Graph graph, uint nodeFrom, uint nodeTo)
        {
            Graph = graph;
            NodeFrom = nodeFrom;
            NodeTo = nodeTo;
        }

        internal Graph Graph { get; }

        internal uint NodeFrom { get; }

        internal uint NodeTo { get; }

        /// <summary>
        /// Connect two nodes in graph
        /// </summary>
        /// <param name="edge">Node from</param>
        /// <param name="node">Node to</param>
        /// <returns>Temporary edge</returns>
        public static EdgeTemp operator >>(EdgeTemp edge, int node)
        {
            return new EdgeTemp(edge.Graph, edge.NodeFrom, (uint)node);
        }

        /// <summary>
        /// Create edge between two nodes
        /// </summary>
        /// <param name="edge">Temporary edge</param>
        /// <param name="cost">Connection cost</param>
        /// <returns>True if connected</returns>
        public static bool operator^(EdgeTemp edge, int cost)
        {
            if (edge.NodeTo == default)
            {
                throw new InvalidOperationException("Destination node is not defined. Use >> operator to define it.");
            }

            return edge.Graph.Connect(edge.NodeFrom, edge.NodeTo, cost);
        }
    }
}