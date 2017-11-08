using System;
using System.Collections.Generic;
using System.Linq;
using Dijkstra.NET.Contract;

namespace Dijkstra.NET.Model
{
    public class DijkstraResult: IShortestPathResult
    {
        private readonly uint _from;
        private readonly uint _to;

        private readonly Dictionary<uint, uint> _path = new Dictionary<uint, uint>();

        public DijkstraResult(uint @from, uint to)
        {
            _from = @from;
            _to = to;
            Distance = Int32.MaxValue;
        }

        public int Distance { get; set; }

        public uint FromNode => _from;

        public uint ToNode => _to;

        public virtual IDictionary<uint, uint> Path => _path;

        public bool IsFounded => Distance != Int32.MaxValue;

        public IEnumerable<uint> GetReversePath()
        {
            uint result = _to;

            while (true)
            {
                yield return result;

                if (result == _from)
                    break;

                result = Path[result];
            }
        }

        public IEnumerable<uint> GetPath() => GetReversePath().Reverse();
    }
}
