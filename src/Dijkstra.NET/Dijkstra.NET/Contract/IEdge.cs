namespace Dijkstra.NET.Contract
{
    public interface IEdge<T>
    {
        INode<T> Node { get; }
        uint Cost { get; }
    }
}
