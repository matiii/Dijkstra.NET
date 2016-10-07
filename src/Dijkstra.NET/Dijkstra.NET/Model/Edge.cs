namespace Dijkstra.NET.Model
{
    using Contract;
    public struct Edge<T, TCustom> where TCustom: class 
    {
        public Edge(INode<T, TCustom> node, uint cost, TCustom custom)
        {
            Node = node;
            Cost = cost;
            Item = custom;
        }

        public INode<T, TCustom> Node { get; }
        public uint Cost { get; }
        public TCustom Item { get; }
    }
}
