using System;

namespace Dijkstra.NET.ShortestPath.Model
{
    [Obsolete]
    public struct ShortPathJob
    {
        public uint NodeKey { get; set; }

        public int Distance { get; set; }

    }
}