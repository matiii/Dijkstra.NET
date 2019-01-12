using System;

namespace Dijkstra.NET.Graph.Exceptions
{
    public class EdgeNotFoundException: Exception
    {
        internal EdgeNotFoundException(uint node)
            :base($"Edge with {node} nod key doesn't exist.")
        {
        }
    }
}