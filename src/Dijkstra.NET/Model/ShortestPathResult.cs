using System;
using System.Collections.Generic;
using System.Linq;

namespace Dijkstra.NET.Model
{
    public readonly struct ShortestPathResult
    {
        private readonly IDictionary<uint, uint> _path;

        internal ShortestPathResult(uint @from, uint to, int distance, IDictionary<uint, uint> path)
        {
            FromNode = @from;
            ToNode = to;
            Distance = distance;
            _path = path;
        }

        internal ShortestPathResult(uint @from, uint to): this(@from, @to, Int32.MaxValue, null) { }

        public int Distance { get; }

        public uint FromNode { get; }

        public uint ToNode { get; }

        public bool IsFounded => Distance != Int32.MaxValue;

        public IEnumerable<uint> GetReversePath()
        {
            if (_path == null)
            {
                yield break;
            }

            uint result = ToNode;

            while (true)
            {
                yield return result;

                if (result == FromNode)
                    break;

                result = _path[result];
            }
        }

        public IEnumerable<uint> GetPath() => GetReversePath().Reverse();
    }
}