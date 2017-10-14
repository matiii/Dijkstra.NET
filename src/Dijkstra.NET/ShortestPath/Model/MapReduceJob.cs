namespace Dijkstra.NET.ShortestPath.Model
{
    internal class MapReduceJob
    {
        public MapReduceJob(uint @from, uint to, int fromDistance, int cost)
        {
            From = @from;
            To = to;
            Distance = fromDistance + cost;
        }

        public uint From { get; }
        public uint To { get; }
        public int Distance { get; }
    }
}
