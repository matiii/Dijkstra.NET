using System.Collections.Generic;

namespace Dijkstra.NET.Contract
{
    public interface IShortestPathResult
    {
        IEnumerable<uint> GetReversePath();

        IEnumerable<uint> GetPath();

        bool IsFounded { get; }

        int Distance { get; }

        uint FromNode { get; }

        uint ToNode { get; }
    }
}
