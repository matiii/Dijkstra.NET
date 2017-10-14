namespace Dijkstra.NET.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

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
