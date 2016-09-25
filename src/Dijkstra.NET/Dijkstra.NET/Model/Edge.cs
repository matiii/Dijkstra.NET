namespace Dijkstra.NET.Model
{
    using Contract;
    public class Edge<T>: IEdge<T>
    {
        public Edge(INode<T> node, uint cost)
        {
            Node = node;
            Cost = cost;
        }

        public INode<T> Node { get; }
        public uint Cost { get; }
    }
}
