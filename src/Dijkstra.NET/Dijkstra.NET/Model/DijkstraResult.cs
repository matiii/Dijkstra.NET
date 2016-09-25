namespace Dijkstra.NET.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct DijkstraResult
    {
        private readonly uint _from;
        private readonly uint _to;

        public DijkstraResult(uint @from, uint to)
        {
            _from = @from;
            _to = to;
            Distance = UInt32.MaxValue;
            Path = new Dictionary<uint, uint>();
        }

        public uint Distance { get; set; }
        public Dictionary<uint, uint> Path { get; }

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
