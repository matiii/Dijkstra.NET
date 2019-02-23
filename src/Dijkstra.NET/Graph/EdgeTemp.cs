using System;

namespace Dijkstra.NET.Graph
{
    public readonly struct EdgeTemp<T, TCustom> where TCustom: IEquatable<TCustom>
    {
        internal EdgeTemp(uint nodeFrom, uint nodeTo, Graph<T, TCustom> graph)
        {
            NodeFrom = nodeFrom;
            NodeTo = nodeTo;
            Graph = graph;
            Cost = int.MinValue;
        }

        internal EdgeTemp(uint nodeFrom, uint nodeTo, Graph<T, TCustom> graph, int cost)
        {
            NodeFrom = nodeFrom;
            NodeTo = nodeTo;
            Graph = graph;
            Cost = cost;
        }

        /// <summary>
        /// Define cost of edge
        /// </summary>
        /// <param name="edge">Edge</param>
        /// <param name="cost">Cost of edge</param>
        /// <returns>Temporary edge</returns>
        public static EdgeTemp<T, TCustom> operator>>(EdgeTemp<T, TCustom> edge, int cost)
        {
            return new EdgeTemp<T, TCustom>(edge.NodeFrom, edge.NodeTo, edge.Graph, cost);
        }

        /// <summary>
        /// Create edge between two nodes
        /// </summary>
        /// <param name="edge">Edge</param>
        /// <param name="edgeCustom">Custom information in edge</param>
        /// <returns>True if connected</returns>
        public static bool operator^(EdgeTemp<T, TCustom> edge, TCustom edgeCustom)
        {
            if (edge.Cost == int.MinValue)
            {
                throw new InvalidOperationException("Cost of edge is not defined. Use >> operator to define it.");
            }

            return edge.Graph.Connect(edge.NodeFrom, edge.NodeTo, edge.Cost, edgeCustom);
        }

        internal uint NodeFrom { get; }

        internal uint NodeTo { get; }

        internal Graph<T, TCustom> Graph { get; }

        internal int Cost { get; }
    }
}