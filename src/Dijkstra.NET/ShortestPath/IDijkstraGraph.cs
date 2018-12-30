namespace Dijkstra.NET.ShortestPath
{
    public interface IDijkstraGraph
    {
        IDijkstra this[uint node] { get; }
    }
}