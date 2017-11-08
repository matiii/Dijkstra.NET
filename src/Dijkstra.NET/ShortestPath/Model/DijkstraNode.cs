namespace Dijkstra.NET.ShortestPath.Model
{
    public struct DijkstraNode
    {
        public DijkstraNode(uint key, int distance, int queueIndex)
        {
            Key = key;
            Distance = distance;
            QueueIndex = queueIndex;
        }

        public uint Key { get; }

        public int Distance { get; set; }

        public int QueueIndex { get; set; }
    }
}