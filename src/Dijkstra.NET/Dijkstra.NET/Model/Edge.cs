namespace Dijkstra.NET.Model
{
    using System;
    using Contract;
    public struct Edge<T, TCustom>: IEquatable<Edge<T, TCustom>> where TCustom: class
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

        public bool Equals(Edge<T, TCustom> other) => Node.Key == other.Node.Key && Cost == other.Cost && ReferenceEquals(Item, other.Item);

        public override int GetHashCode()
        {
            int hash = 13;
            hash = hash * 7 + (int)Cost;
            hash = hash * 7 + (int)Node.Key;
            return hash;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Edge<T, TCustom>?;

            if (other == null)
                return false;

            return Equals(other.Value);
        }

    }
}
