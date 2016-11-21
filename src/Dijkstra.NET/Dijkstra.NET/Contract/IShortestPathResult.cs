namespace Dijkstra.NET.Contract
{
    using System.Collections.Generic;

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
