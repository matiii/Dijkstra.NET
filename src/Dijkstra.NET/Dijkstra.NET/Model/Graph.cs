namespace Dijkstra.NET.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Contract;
    public class Graph<T>: IGraph<T>, ICloneable
    {
        private readonly IDictionary<uint, INode<T>> _nodes = new Dictionary<uint, INode<T>>();


        public void AddNode(T item)
        {
            uint key = (uint) _nodes.Count;
            AddNode(key, item);
        }

        protected void AddNode(uint key, T item)
        {
            if (_nodes.ContainsKey(key))
                throw new InvalidOperationException("Node have to be unique.", new Exception("The same key of node."));

            _nodes.Add(key, new Node<T>(key, item));
        }

        public bool Connect(uint from, uint to, uint cost)
        {
            if (!_nodes.ContainsKey(from) || !_nodes.ContainsKey(to))
                return false;

            INode<T> nodeFrom = this[from];
            INode<T> nodeTo = this[to];

            nodeFrom.Children.Add(new Edge<T>(nodeTo, cost));
            nodeTo.Parents.Add(new Edge<T>(nodeFrom, cost));

            return true;
        }

        /// <summary>
        /// Reset distance in nodes
        /// </summary>
        public void Reset()
        {
            foreach (var node in this)
                node.Distance = UInt32.MaxValue;
        }

        public bool HasToBeReset() => this.Any(x => x.Distance != UInt32.MaxValue);

        public IEnumerator<INode<T>> GetEnumerator() => _nodes.Select(x => x.Value).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public INode<T> this[uint node] => _nodes[node];


        /// <summary>
        /// Deep copy of graph
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var graph = new Graph<T>();

            foreach (var node in _nodes.Values)
                graph.AddNode(node.Key, node.Item);

            foreach (var node in _nodes.Values.Where(x => x.Children.Count > 0))
            {
                foreach (var edge in node.Children)
                    graph.Connect(node.Key, edge.Node.Key, edge.Cost);
            }

            return graph;
        }
    }
}
