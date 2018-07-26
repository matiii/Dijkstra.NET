using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dijkstra.NET.Contract;

namespace Dijkstra.NET.Model
{
    public class Graph<T, TEdgeCustom>: IGraph<T, TEdgeCustom>, IEnumerable<INode<T, TEdgeCustom>>, ICloneable<Graph<T, TEdgeCustom>> where TEdgeCustom : IEquatable<TEdgeCustom>
    {
        private readonly IDictionary<uint, Node<T, TEdgeCustom>> _nodes = new Dictionary<uint, Node<T, TEdgeCustom>>();

        public void AddNode(T item)
        {
            uint key = (uint) _nodes.Count;
            AddNode(key, item);
        }

        protected void AddNode(uint key, T item)
        {
            if (_nodes.ContainsKey(key))
                throw new InvalidOperationException("Node have to be unique.", new Exception("The same key of node."));

            _nodes.Add(key, new Node<T, TEdgeCustom>(key, item));
        }

        public bool Connect(uint from, uint to, int cost, TEdgeCustom custom)
        {
            if (!_nodes.ContainsKey(from) || !_nodes.ContainsKey(to))
                return false;

            Node<T,TEdgeCustom> nodeFrom = _nodes[from];
            Node<T, TEdgeCustom> nodeTo = _nodes[to];

            nodeFrom.AddChild(new Edge<T, TEdgeCustom>(nodeTo, cost, custom));

            return true;
        }

        /// <summary>
        /// Reset distance in nodes
        /// </summary>
        [Obsolete]
        public void Reset()
        {
            foreach (var node in this)
                node.Distance = Int32.MaxValue;
        }

        [Obsolete]
        public bool HasToBeReset() => this.Any(x => x.Distance != Int32.MaxValue);

        public IEnumerator<INode<T, TEdgeCustom>> GetEnumerator() => _nodes.Select(x => x.Value).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public INode<T, TEdgeCustom> this[uint node] => _nodes[node];

        /// <summary>
        /// Deep copy of graph
        /// </summary>
        /// <returns>new instance of graph</returns>
        [Obsolete]
        public Graph<T, TEdgeCustom> Clone()
        {
            var graph = new Graph<T, TEdgeCustom>();

            foreach (var node in _nodes.Values)
                graph.AddNode(node.Key, node.Item);

            //todo
//            foreach (var node in _nodes.Values.Where(x => x.Children.Count > 0))
//            {
//                foreach (var edge in node.Children)
//                    graph.Connect(node.Key, edge.Node.Key, edge.Cost, edge.Item);
//            }

            return graph;
        }
    }
}
