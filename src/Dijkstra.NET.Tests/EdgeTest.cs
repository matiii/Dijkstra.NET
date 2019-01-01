using Dijkstra.NET.Graph;
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
    }
}
