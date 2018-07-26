using System;

namespace Dijkstra.NET.Model
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    [Obsolete]
    public class BfsConcurrentResult: DijkstraResult
    {
        private readonly ConcurrentDictionary<uint, uint> _path = new ConcurrentDictionary<uint, uint>();

        public BfsConcurrentResult(uint @from, uint to) : base(@from, to)
        {
        }

        public override IDictionary<uint, uint> Path => _path;
    }
}
