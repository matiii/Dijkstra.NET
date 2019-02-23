using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dijkstra.NET.Extensions;
using Dijkstra.NET.PageRank;
using Dijkstra.NET.ShortestPath;

namespace Dijkstra.NET.Graph.Simple
{
    /// <summary>
    /// Simple graph for nodes processing
    /// </summary>
    public class Graph: IDijkstraGraph, IPageRankGraph
    {
        private readonly Dictionary<uint, HashSet<ReadonlyEdge>> _nodes = new Dictionary<uint, HashSet<ReadonlyEdge>>();
        private readonly Dictionary<uint, HashSet<uint>> _nodesParent = new Dictionary<uint, HashSet<uint>>();

        /// <summary>
        /// Connect node with node
        /// </summary>
        /// <param name="graph">Graph</param>
        /// <param name="node">Key of node</param>
        /// <returns>Temporary edge</returns>
        public static EdgeTemp operator >>(Graph graph, int node)
        {
            return new EdgeTemp(graph, (uint)node);
        }

        /// <summary>
        /// Add nodes to graph
        /// </summary>
        /// <param name="graph">Graph</param>
        /// <param name="numberOfNodes">Number of nodes</param>
        /// <returns></returns>
        public static Graph operator +(Graph graph, int numberOfNodes)
        {
            Enumerable
                .Range(0, numberOfNodes)
                .Each(_ => graph.AddNode());

            return graph;
        }

        /// <summary>
        /// Add node to graph
        /// </summary>
        /// <returns></returns>
        public uint AddNode()
        {
            uint key = (uint) (_nodes.Count + 1);
            _nodes.Add(key, new HashSet<ReadonlyEdge>());
            _nodesParent.Add(key, new HashSet<uint>());
            return key;
        }

        /// <summary>
        /// Connect node from to node to with cost
        /// (from)-[cost]->(to)
        /// </summary>
        /// <param name="from">Node from</param>
        /// <param name="to">Node to</param>
        /// <param name="cost">Cost of connection</param>
        /// <returns>True if two nodes exist</returns>
        public bool Connect(uint from, uint to, int cost)
        {
            if (!_nodes.ContainsKey(from) || !_nodes.ContainsKey(to))
                return false;

            _nodesParent[to].Add(from);
            _nodes[from].Add(new ReadonlyEdge(to, cost));

            return true;
        }

        /// <summary>
        /// Connect node from to node to
        /// (from)-[]->(to)
        /// </summary>
        /// <param name="from">Node from</param>
        /// <param name="to">Node to</param>
        /// <returns>True if two nodes exist</returns>
        public bool Connect(uint from, uint to) => Connect(from, to, -1);

        /// <summary>
        /// Get nodes with cost
        /// </summary>
        /// <param name="node"></param>
        public Action<Edge> this[uint node] => e => _nodes[node].Each(n => e(n.Key, n.Cost));

        public IEnumerator<uint> GetEnumerator()
        {
            foreach (var node in _nodes)
            {
                yield return node.Key;
            }
        }

        public override string ToString()
        {
            return $"Simple::Graph({NodesCount})";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int NodesCount => _nodes.Count;

        public int EdgesCount(uint node) => _nodes[node].Count;

        public IEnumerable<uint> Parents(uint node) => _nodesParent[node];
    }
}