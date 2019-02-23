using System;
using System.Collections;
using System.Collections.Generic;
using Dijkstra.NET.Graph.Exceptions;
using Dijkstra.NET.ShortestPath;

namespace Dijkstra.NET.Graph
{
    public class Node<T, TEdgeCustom>: INode<T, TEdgeCustom> where TEdgeCustom: IEquatable<TEdgeCustom>
    {
        private readonly HashSet<Node<T,TEdgeCustom>> _parents = new HashSet<Node<T, TEdgeCustom>>();
        private Edge<T, TEdgeCustom>[] _edges;

        internal Node(uint key, T item, Graph<T, TEdgeCustom> graph)
        {
            Key = key;
            Item = item;
            _edges = new Edge<T, TEdgeCustom>[5];
            Graph = graph;
        }

        /// <summary>
        /// Connect node with node
        /// </summary>
        /// <param name="nodeFrom">Node from</param>
        /// <param name="nodeTo">Node to</param>
        /// <returns>Temporal edge</returns>
        public static EdgeTemp<T, TEdgeCustom> operator>>(Node<T, TEdgeCustom> nodeFrom, int nodeTo)
        {
            return new EdgeTemp<T, TEdgeCustom>(nodeFrom.Key, (uint) nodeTo, nodeFrom.Graph);
        }

        public uint Key { get; }

        public T Item { get; }

        public int EdgesCount { get; internal set; }

        public IEnumerable<Node<T, TEdgeCustom>> Parents => _parents;

        /// <summary>
        /// Get custom info from node edges by node key
        /// </summary>
        /// <param name="nodeKey">Node key</param>
        /// <returns>TEdgeCustom</returns>
        public TEdgeCustom GetFirstEdgeCustom(uint nodeKey)
        {
            for (int i = 0; i < EdgesCount; i++)
            {
                ref Edge<T, TEdgeCustom> e = ref _edges[i];

                if (e.Node.Key == nodeKey)
                {
                    return e.Item;
                }
            }

            throw new EdgeNotFoundException(nodeKey);
        }

        internal Graph<T, TEdgeCustom> Graph { get; }

        internal void EachEdge(Edge edge)
        {
            for (int i = 0; i < EdgesCount; i++)
            {
                ref Edge<T, TEdgeCustom> e = ref _edges[i];

                edge(e.Node.Key, e.Cost);
            }
        }

        internal void AddEdge(in Edge<T, TEdgeCustom> edge)
        {
            if (_edges.Length == EdgesCount)
            {
                int newSize = _edges.Length;

                if (EdgesCount < NodeConstants.MaxSize)
                {
                    newSize *= 2;
                }
                else
                {
                    long bigSize = (long) (newSize * 1.5);

                    newSize = bigSize < Int32.MaxValue ? (int)bigSize : Int32.MaxValue;
                }

                Array.Resize(ref _edges, newSize);
            }

            _edges[EdgesCount] = edge;
            EdgesCount++;
        }

        internal void AddParent(Node<T, TEdgeCustom> parent)
        {
            _parents.Add(parent);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public IEnumerator<Edge<T, TEdgeCustom>> GetEnumerator()
        {
            for (int i = 0; i < EdgesCount; i++)
            {
                yield return _edges[i];
            }
        }

        public override bool Equals(object obj)
        {
            var node = obj as INode;

            return node?.Key == Key;
        }

        public override string ToString()
        {
            return $"[{Key}({Item?.ToString()})]";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}