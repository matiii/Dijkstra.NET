using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
using System.Linq;
using Xunit;

namespace Dijkstra.NET.Tests
{
    public class EdgeTest
    {
        
        [Fact]
        public void Two_Edges_Should_Be_Equal()
        {
            var g = new Graph<string, int>() + "node1";
            var n = g >> 1;
            
            var a = new Edge<string, int>(n, 1, 1);
            var b = new Edge<string, int>(n,1, 1);

            bool act = a.Equals(b);

            Assert.True(act);
        }

        [Fact]
        public void Two_Edges_Should_Be_Equal_With_Null_References()
        {
            var g = new Graph<string, string>() + "node1";
            var n = g >> 1;
            
            var a = new Edge<string, string>(n, 1, null);
            var b = new Edge<string, string>(n,1, null);

            bool act = a.Equals(b);

            Assert.True(act);
        }

        [Fact]
        public void Two_Edges_Should_Be_Diffrent_With_Null_Reference()
        {
            var g = new Graph<string, string>() + "node1";
            var n = g >> 1;
            
            var a = new Edge<string, string>(n, 1, null);
            var b = new Edge<string, string>(n,1, "a");

            bool act = a.Equals(b);
            bool act2 = b.Equals(a);

            Assert.False(act);
            Assert.False(act2);
        }

        [Fact]
        public void Two_Edges_Should_Be_Diffrent_With_Diffrent_Parameter()
        {
            var g = new Graph<string, string>() + "node1";
            var n = g >> 1;
            
            var a = new Edge<string, string>(n, 1, "b");
            var b = new Edge<string, string>(n,1, "a");

            bool act = a.Equals(b);
            bool act2 = b.Equals(a);

            Assert.False(act);
            Assert.False(act2);
        }

        [Fact]
        public void Two_Edges_Should_Be_Diffrent_With_Diffrent_Costs()
        {
            var g = new Graph<string, string>() + "node1";
            var n = g >> 1;
            
            var a = new Edge<string, string>(n, 3, "a");
            var b = new Edge<string, string>(n,1, "a");

            bool act = a.Equals(b);
            bool act2 = b.Equals(a);

            Assert.False(act);
            Assert.False(act2);
        }

        [Fact]
        public void Two_Edges_Should_Be_Diffrent_With_Diffrent_Nodes()
        {
            var g = new Graph<string, string>() + "node1" + "node2";
            var n1 = g >> 1;
            var n2 = g >> 2;
            
            var a = new Edge<string, string>(n1, 1, "a");
            var b = new Edge<string, string>(n2,1, "a");

            bool act = a.Equals(b);
            bool act2 = b.Equals(a);

            Assert.False(act);
            Assert.False(act2);
        }

        [Fact]
        public void Edges_Get_TEdgeCustom_Should_Work()
        {
            var g = new Graph<int, string>();
            g.AddNode(1);
            g.AddNode(2);
            g.AddNode(3);

            g.Connect(1, 2, 1, "First");
            g.Connect(1, 3, 1, "Second");

            var node = g >> 1;
            var first = node.GetFirstEdgeCustom(2);
            var second = node.GetFirstEdgeCustom(3);


            bool act = first == "First" && second == "Second";

            Assert.True(act);
        }
        [Fact]
        public void Edges_Get_Full_Path_Custom()
        {
            var g = new Graph<int, string>();
            g.AddNode(1);
            g.AddNode(2);
            g.AddNode(3);

            g.Connect(1, 2, 1, "First");
            g.Connect(2, 3, 1, "Second");

            var path = g.Dijkstra(1, 3);
            var pathCustom = path.GetPathEdgesCustom(g);
            var pathCustomReversed = path.GetReversedPathEdgesCustom(g);
                       
            bool act = pathCustom.Count() == 2;
            bool act2 = pathCustomReversed.Count() == 2;
            bool act3 = pathCustom.First() == "First";
            bool act4 = pathCustomReversed.First() == "Second";

            Assert.True(act);
            Assert.True(act2);
            Assert.True(act3);
            Assert.True(act3);

        }

    }
}
