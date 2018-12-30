using Dijkstra.NET.Graph;

namespace Dijkstra.NET.ShortestPath
{
    public interface IDijkstra : INode
    {
        void EachEdge(Edge edge);
    }
}