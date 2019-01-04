using System;
using System.Collections.Generic;
using Dijkstra.NET.PageRank;
using Dijkstra.NET.ShortestPath;
using System.Linq;
namespace Dijkstra.NET.Graph
{
    public class Node<T, TEdgeCustom>: IPageRank, IDijkstra, INode<T, TEdgeCustom> where TEdgeCustom: IEquatable<TEdgeCustom>
    {
        public Edge<T, TEdgeCustom>[] Edges;
        private readonly HashSet<Node<T,TEdgeCustom>> _parents = new HashSet<Node<T, TEdgeCustom>>();
        private readonly HashSet<uint> _children = new HashSet<uint>();

        internal Node(uint key, T item, Graph<T, TEdgeCustom> graph)
        {
            Key = key;
            Item = item;
            Edges = new Edge<T, TEdgeCustom>[5];
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
        
        public int NumberOfEdges => _children.Count;

        public IEnumerable<IPageRank> Parents => _parents;

        internal Graph<T, TEdgeCustom> Graph { get; }
        
        public void EachEdge(Edge edge)
        {
            for (int i = 0; i < EdgesCount; i++)
            {
                ref Edge<T, TEdgeCustom> e = ref Edges[i];

                edge(e.Node.Key, e.Cost);
            }
        }
        /// <summary>
        /// Get custom info from node edges by edge key
        /// </summary>
        /// <param name="nodeEdgeKey">edge key</param>
        /// <returns>TEdgeCustom</returns>
        public TEdgeCustom GetFirstEdgeCustom(int nodeEdgeKey)
        {
            return Edges.First(c => c.Node.Key == nodeEdgeKey).Item;
        }
      

        internal void AddEdge(in Edge<T, TEdgeCustom> edge)
        {
            if (Edges.Length == EdgesCount)
            {
                int newSize = Edges.Length;

                if (EdgesCount < NodeConstants.MaxSize)
                {
                    newSize *= 2;
                }
                else
                {
                    long bigSize = (long) (newSize * 1.5);

                    newSize = bigSize < Int32.MaxValue ? (int)bigSize : Int32.MaxValue;
                }

                Array.Resize(ref Edges, newSize);
            }

            Edges[EdgesCount] = edge;
            EdgesCount++;
            _children.Add(edge.Node.Key);
        }

        internal void AddParent(Node<T, TEdgeCustom> parent)
        {
            _parents.Add(parent);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
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
    }
}
