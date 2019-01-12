using System;

namespace Dijkstra.NET.Graph.Simple
{
    internal readonly struct ReadonlyEdge: IEquatable<ReadonlyEdge>
    {
        public ReadonlyEdge(uint key, int cost)
        {
            Key = key;
            Cost = cost;
        }

        public uint Key { get; }

        public int Cost { get; }

        public bool Equals(ReadonlyEdge other)
        {
            return Key == other.Key && Cost == other.Cost;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ReadonlyEdge other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Key * 397) ^ Cost;
            }
        }
    }
}