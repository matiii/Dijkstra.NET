using System;
using System.Collections.Generic;
using Dijkstra.NET.Contract;

namespace Dijkstra.NET.Model
{
    public readonly struct Edge<T, TCustom>: IEquatable<Edge<T, TCustom>> where TCustom: IEquatable<TCustom>
    {
        public Edge(INode<T, TCustom> node, int cost, TCustom custom)
        {
            Node = node;
            Cost = cost;
            Item = custom;
        }

        public INode<T, TCustom> Node { get; }

        public int Cost { get; }

        public TCustom Item { get; }

        public bool Equals(Edge<T, TCustom> other) => Node.Key == other.Node.Key && Cost == other.Cost &&
                                                    (EqualityComparer<TCustom>.Default.Equals(Item, default(TCustom)) &&
            EqualityComparer<TCustom>.Default.Equals(other.Item, default(TCustom)) ||
            !EqualityComparer<TCustom>.Default.Equals(Item, default(TCustom)) && !EqualityComparer<TCustom>.Default.Equals(other.Item, default(TCustom)) 
            &&  Item.Equals(other.Item));

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
