namespace Dijkstra.NET.Contract
{
    using System.Collections.Generic;
    using Model;

    public interface INode<T,TEdgeCustom> where TEdgeCustom: class
    {
        IList<Edge<T, TEdgeCustom>> Children { get; }
        T Item { get; }
        uint Key { get; }
        uint Distance { get; set; }
    }
}
