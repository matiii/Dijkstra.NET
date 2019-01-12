using System;
using Dijkstra.NET.Graph;
using Dijkstra.NET.PageRank;
using Xunit;

namespace Dijkstra.NET.Tests.PageRank
{
    public class PageRankExtensionsTests
    {
        [Fact]
        public void CalculatePageRank_Should_Calculate_Correct_PageRank()
        {
            var g = new Graph<string, byte>();

            uint a = g.AddNode("A");
            uint b = g.AddNode("B");
            uint c = g.AddNode("C");
            uint d = g.AddNode("D");

            g.Connect(a, b, 0, 0);
            g.Connect(a, c, 0, 0);
            g.Connect(b, d, 0, 0);
            g.Connect(c, a, 0, 0);
            g.Connect(c, b, 0, 0);
            g.Connect(c, d, 0, 0);
            g.Connect(d, c, 0, 0);


            var result = g.PageRank();

            Assert.True(Math.Abs(result[c] - 0.35625) < 0.0001);
        }

        [Fact]
        public void SimpleGraph_CalculatePageRank_Should_Calculate_Correct_PageRank()
        {
            var g = new Graph.Simple.Graph();

            uint a = g.AddNode();
            uint b = g.AddNode();
            uint c = g.AddNode();
            uint d = g.AddNode();

            g.Connect(a, b);
            g.Connect(a, c);
            g.Connect(b, d);
            g.Connect(c, a);
            g.Connect(c, b);
            g.Connect(c, d);
            g.Connect(d, c);

            var result = g.PageRank();

            Assert.True(Math.Abs(result[c] - 0.35625) < 0.0001);
        }

        [Fact]
        public void CalculatePageRank_Should_Calculate_Correct_PageRank_Wiki()
        {
            var g = new Graph<string, byte>();

            uint a = g.AddNode("A");
            uint b = g.AddNode("B");
            uint c = g.AddNode("C");
            uint d = g.AddNode("D");

            g.Connect(a, b, 0, 0);
            g.Connect(a, d, 0, 0);
            g.Connect(b, a, 0, 0);
            g.Connect(c, b, 0, 0);
            g.Connect(c, a, 0, 0);
            g.Connect(d, c, 0, 0);


            var result = g.PageRank();

            Assert.True(Math.Abs(result[a] - 0.35) < 0.01);
            Assert.True(Math.Abs(result[b] - 0.29) < 0.01);
            Assert.True(Math.Abs(result[c] - 0.15) < 0.01);
            Assert.True(Math.Abs(result[d] - 0.18) < 0.01);
        }

        [Fact]
        public void SimpleGraph_CalculatePageRank_Should_Calculate_Correct_PageRank_Wiki()
        {
            var g = new Graph.Simple.Graph();

            uint a = g.AddNode();
            uint b = g.AddNode();
            uint c = g.AddNode();
            uint d = g.AddNode();

            g.Connect(a, b);
            g.Connect(a, d);
            g.Connect(b, a);
            g.Connect(c, b);
            g.Connect(c, a);
            g.Connect(d, c);

            var result = g.PageRank();

            Assert.True(Math.Abs(result[a] - 0.35) < 0.01);
            Assert.True(Math.Abs(result[b] - 0.29) < 0.01);
            Assert.True(Math.Abs(result[c] - 0.15) < 0.01);
            Assert.True(Math.Abs(result[d] - 0.18) < 0.01);
        }
    }
}