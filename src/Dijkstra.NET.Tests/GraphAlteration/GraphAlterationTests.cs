using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dijkstra.NET.Tests.GraphAlteration
{
    public class GraphAlterationTests
    {
        [Fact]
        public void UpdateConnectionCost()
        {
            var g = new Graph<int, string>();
            g.AddNode(1);
            g.AddNode(2);
            g.AddNode(3);

            g.Connect(1, 2, 1, "First");
            g.Connect(2, 3, 2, "Second");
            g.Connect(1, 3, 10, "Expensive");

            ShortestPathResult result1 = g.Dijkstra(1, 3);
            Assert.Equal(3, result1.Distance);

            var updateResult = g.UpdateCost(2, 3, "Second", 11);
            Assert.True(updateResult);
            ShortestPathResult result2 = g.Dijkstra(1, 3);
            Assert.Equal(10, result2.Distance);
        }

        [Theory]
        [InlineData(2, 5, "Second")]
        [InlineData(5, 3, "Second")]
        [InlineData(2, 3, "SecondUnknown")]
        public void UpdateFailsUnknownConnectionDoesNotWork(uint startNode, uint endNode, string custom)
        {
            var g = new Graph<int, string>();
            g.AddNode(1);
            g.AddNode(2);
            g.AddNode(3);

            g.Connect(1, 2, 1, "First");
            g.Connect(2, 3, 2, "Second");
            g.Connect(1, 3, 10, "Expensive");

            var updateResult = g.UpdateCost(startNode, endNode, custom, 11);
            Assert.False(updateResult);
        }
    }
}
