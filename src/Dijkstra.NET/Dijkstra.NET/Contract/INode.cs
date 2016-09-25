namespace Dijkstra.NET.Contract
{
    using System.Collections.Generic;

    public interface INode<T>
    {
        IList<IEdge<T>> Children { get; }
        IList<IEdge<T>> Parents { get; }
        T Item { get; }
        uint Key { get; }
        uint Distance { get; set; }
    }
}
