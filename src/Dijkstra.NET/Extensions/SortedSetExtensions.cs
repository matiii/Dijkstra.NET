using System.Collections.Generic;
using System.Linq;

namespace Dijkstra.NET.Extensions
{
    internal static class SortedSetExtensions
    {
        public static T Deque<T>(this SortedSet<T> set)
        {
            T item = set.First();

            set.Remove(item);

            return item;
        }
    }
}
