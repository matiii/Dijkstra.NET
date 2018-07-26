using System;
using Dijkstra.NET.Model;

namespace Dijkstra.NET.Delegates
{
    public delegate void ChildAction<T, TEdgeCustom>(in Edge<T, TEdgeCustom> edge) where TEdgeCustom : IEquatable<TEdgeCustom>;
}