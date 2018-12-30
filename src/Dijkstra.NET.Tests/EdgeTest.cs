using Dijkstra.NET.Graph;
using Xunit;

namespace Dijkstra.NET.Tests
{
    public class EdgeTest
    {
        [Fact]
        public void Two_Edges_Should_Be_Equal()
        {
            var a = new Edge<string, int>(new Node<string, int>(0, "node1"), 1, 1);
            var b = new Edge<string, int>(new Node<string, int>(0, "node2"),1, 1);

            bool act = a.Equals(b);

            Assert.True(act);
        }

        [Fact]
        public void Two_Edges_Should_Be_Equal_With_Null_References()
        {
            var a = new Edge<string, string>(new Node<string, string>(0, "node1"), 1, null);
            var b = new Edge<string, string>(new Node<string, string>(0, "node2"),1, null);

            bool act = a.Equals(b);

            Assert.True(act);
        }

        [Fact]
        public void Two_Edges_Should_Be_Diffrent_With_Null_Reference()
        {
            var a = new Edge<string, string>(new Node<string, string>(0, "node1"), 1, null);
            var b = new Edge<string, string>(new Node<string, string>(0, "node2"),1, "a");

            bool act = a.Equals(b);
            bool act2 = b.Equals(a);

            Assert.False(act);
            Assert.False(act2);
        }

        [Fact]
        public void Two_Edges_Should_Be_Diffrent_With_Diffrent_Parameter()
        {
            var a = new Edge<string, string>(new Node<string, string>(0, "node1"), 1, "b");
            var b = new Edge<string, string>(new Node<string, string>(0, "node2"),1, "a");

            bool act = a.Equals(b);
            bool act2 = b.Equals(a);

            Assert.False(act);
            Assert.False(act2);
        }

        [Fact]
        public void Two_Edges_Should_Be_Diffrent_With_Diffrent_Costs()
        {
            var a = new Edge<string, string>(new Node<string, string>(0, "node1"), 3, "a");
            var b = new Edge<string, string>(new Node<string, string>(0, "node2"),1, "a");

            bool act = a.Equals(b);
            bool act2 = b.Equals(a);

            Assert.False(act);
            Assert.False(act2);
        }

        [Fact]
        public void Two_Edges_Should_Be_Diffrent_With_Diffrent_Nodes()
        {
            var a = new Edge<string, string>(new Node<string, string>(0, "node1"), 1, "a");
            var b = new Edge<string, string>(new Node<string, string>(1, "node2"),1, "a");

            bool act = a.Equals(b);
            bool act2 = b.Equals(a);

            Assert.False(act);
            Assert.False(act2);
        }
    }
}
