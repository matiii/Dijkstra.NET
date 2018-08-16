using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dijkstra.NET.Contract;

namespace Dijkstra.NET.Model
{
    public class Graph<T, TEdgeCustom>: IGraph<T, TEdgeCustom>, IEnumerable<INode<T, TEdgeCustom>> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly IDictionary<uint, Node<T, TEdgeCustom>> _nodes = new Dictionary<uint, Node<T, TEdgeCustom>>();

        /// <summary>
        /// Add node to graph
        /// </summary>
        /// <param name="item">Node</param>
        /// <returns>Key of node</returns>
        public uint AddNode(T item)
        {
            uint key = (uint) _nodes.Count;
            AddNode(key, item);
            return key;
        }

        protected void AddNode(uint key, T item)
        {
            if (_nodes.ContainsKey(key))
                throw new InvalidOperationException("Node have to be unique.", new Exception("The same key of node."));

            _nodes.Add(key, new Node<T, TEdgeCustom>(key, item));
        }

        /// <summary>
        /// Connect node from with node to
        /// </summary>
        /// <param name="from">First node</param>
        /// <param name="to">Second node</param>
        /// <param name="cost">Cost of connection</param>
        /// <param name="custom">Information saved in edge</param>
        /// <returns>Returns true if nodes connected</returns>
        public bool Connect(uint from, uint to, int cost, TEdgeCustom custom)
        {
            if (!_nodes.ContainsKey(from) || !_nodes.ContainsKey(to))
                return false;

            Node<T,TEdgeCustom> nodeFrom = _nodes[from];
            Node<T, TEdgeCustom> nodeTo = _nodes[to];

            nodeFrom.AddChild(new Edge<T, TEdgeCustom>(nodeTo, cost, custom));

            return true;
        }

        public IEnumerator<INode<T, TEdgeCustom>> GetEnumerator() => _nodes.Select(x => x.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public INode<T, TEdgeCustom> this[uint node] => _nodes[node];
    }
}
