using System;
using System.Collections.Generic;

namespace Dijkstra.NET.Extensions
{
    internal static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> iterator, Action<T> action)
        {
            foreach (var t in iterator)
            {
                action(t);
            }
        }
    }
}