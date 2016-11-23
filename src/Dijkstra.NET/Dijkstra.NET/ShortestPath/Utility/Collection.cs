namespace Dijkstra.NET.ShortestPath.Utility
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    internal class Collection<T>
    {
        private readonly ConcurrentBag<T> _bag = new ConcurrentBag<T>();

        private int _completeAdding;

        public void Add(T element)
        {
            if (Interlocked.CompareExchange(ref _completeAdding, 1, 1) == 1)
                throw new InvalidOperationException("It's not allowed add new element after CompleteAdding marked.");

            _bag.Add(element);
        }

        public IEnumerable<T> GetConsumingEnumerable()
        {
            var spin = new SpinWait();

            T element;

            while (Interlocked.CompareExchange(ref _completeAdding, 0, 0) == 0)
            {
                if (_bag.TryTake(out element))
                    yield return element;
                else
                    spin.SpinOnce();
            }
        }

        public void CompleteAdding()
        {
            Interlocked.Exchange(ref _completeAdding, 1);
        }

        public int Count => _bag.Count;
    }
}
