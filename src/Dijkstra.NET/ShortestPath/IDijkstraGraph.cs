using System;

namespace Dijkstra.NET.ShortestPath
{
    public interface IDijkstraGraph
    {
        Action<Edge> this[uint node] { get; }
    }
}