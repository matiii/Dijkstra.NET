using Dijkstra.NET.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dijkstra.NET.ShortestPath
{
    public static class ShortestPathExtensions
    {
        public static IEnumerable<TEdgeCustom> GetReversedPathEdgesCustom<T, TEdgeCustom>(this ShortestPathResult result, Graph<T, TEdgeCustom> graph) where TEdgeCustom : IEquatable<TEdgeCustom>
        {
            return result.GetPathEdgesCustom(graph).Reverse();
        }

        public static IEnumerable<TEdgeCustom> GetPathEdgesCustom<T, TEdgeCustom>(this ShortestPathResult result, Graph<T, TEdgeCustom> graph) where TEdgeCustom : IEquatable<TEdgeCustom>
        {
            if (result.IsFounded)
            {              
                using (var iterator = result.GetPath().GetEnumerator())
                {
                    if (!iterator.MoveNext())
                    {
                        yield break;
                    }
                    var currentNode = iterator.Current;
                    while (iterator.MoveNext())
                    {
                        yield return GetFirstEdgeCustom(graph, currentNode, iterator.Current);
                        currentNode = iterator.Current;
                    }
                }
            }
            else
                throw new System.Exception("path not founded");
        }

        static TEdgeCustom GetFirstEdgeCustom<T, TEdgeCustom>(Graph<T, TEdgeCustom> graph, uint currentNodeKey, uint edgeNodeKey) where TEdgeCustom : IEquatable<TEdgeCustom>
        {
            var currentNode = graph >> (int)currentNodeKey;
            if (currentNode == null)
                throw new ArgumentException("node not founded at graph");
            return currentNode.GetFirstEdgeCustom((int)edgeNodeKey);
        }
    }
}
