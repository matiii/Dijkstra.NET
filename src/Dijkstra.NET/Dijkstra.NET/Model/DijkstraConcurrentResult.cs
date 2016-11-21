namespace Dijkstra.NET.Model
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class DijkstraConcurrentResult: DijkstraResult
    {
        private readonly ConcurrentDictionary<uint, uint> _path = new ConcurrentDictionary<uint, uint>();

        public DijkstraConcurrentResult(uint @from, uint to) : base(@from, to)
        {
        }

        public override IDictionary<uint, uint> Path => _path;
        internal ConcurrentDictionary<uint, uint> P => _path;
    }
}
