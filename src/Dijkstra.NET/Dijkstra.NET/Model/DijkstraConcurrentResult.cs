namespace Dijkstra.NET.Model
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using Contract;

    public class DijkstraConcurrentResult: DijkstraResult
    {
        private readonly ConcurrentDictionary<uint, uint> _path = new ConcurrentDictionary<uint, uint>();
        private readonly ConcurrentDictionary<uint, object> _lockerFactory = new ConcurrentDictionary<uint, object>();

        public DijkstraConcurrentResult(uint @from, uint to) : base(@from, to)
        {
        }

        internal bool Reduce<T, TCustomEdge>(uint from, INode<T, TCustomEdge> to, int distance) where TCustomEdge : IEquatable<TCustomEdge>
        {
            object locker = _lockerFactory.GetOrAdd(to.Key, new object());

            //TODO: replace with Interlocked.CompareExchange solution

            lock (locker)
            {
                if (to.Distance > distance)
                {
                    to.Distance = (uint) distance;
                    _path.AddOrUpdate(to.Key, from, (u, u1) => from);
                    return true;
                }
            }

            //_lockerFactory.TryRemove(to.Key, out locker);

            return false;
        } 

        public override IDictionary<uint, uint> Path => _path;
    }
}
