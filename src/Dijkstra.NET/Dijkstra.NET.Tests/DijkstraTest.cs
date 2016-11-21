namespace Dijkstra.NET.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using ShortestPath;

    [TestClass]
    public class DijkstraTest
    {
        [TestMethod]
        public void Dijkstra_Should_Find_Path_In_Multi_Paths_Graph()
        {
            var fixture = new TheShortestPathFixture();
            fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(g => new Dijkstra<int, string>(g));
        }

        [TestMethod]
        public void Dijkstra_Should_Find_Path_With_One_Vertex_In_Graph()
        {
            var graph = new Graph<int, string>();
            graph.AddNode(0);

            var dijkstra = new Dijkstra<int, string>(graph);
            var result = dijkstra.Process(0, 0);
            uint[] path = result.GetPath().ToArray();

            Assert.AreEqual<uint>(0, path[0]);
            Assert.AreEqual(0, result.Distance);
            Assert.IsTrue(result.IsFounded);
        }

        [TestMethod]
        public void Dijkstra_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph()
        {
            var graph = new Graph<int, string>();
            graph.AddNode(0);

            graph.Connect(0, 0, 5, null);

            var dijkstra = new Dijkstra<int, string>(graph);
            var result = dijkstra.Process(0, 0);
            uint[] path = result.GetPath().ToArray();

            Assert.AreEqual<uint>(0, path[0]);
            Assert.AreEqual(0, result.Distance);
            Assert.IsTrue(result.IsFounded);
        }

        [TestMethod]
        public void Dijkstra_Should_Find_Path_In_Multi_Edges_Graph()
        {
            var graph = new Graph<int, string>();

            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);

            graph.Connect(0, 1, 2, null);
            graph.Connect(0, 2, 3, null);
            graph.Connect(1, 3, 4, null);
            graph.Connect(2, 3, 3, null);
            graph.Connect(2, 3, 2, null);
            graph.Connect(2, 3, 4, null);
            graph.Connect(2, 4, 1, null);
            graph.Connect(3, 5, 6, null);

            var dijkstra = new Dijkstra<int, string>(graph);
            var result = dijkstra.Process(0, 5);
            uint[] path = result.GetPath().ToArray();

            Assert.AreEqual<uint>(0, path[0]);
            Assert.AreEqual<uint>(2, path[1]);
            Assert.AreEqual<uint>(3, path[2]);
            Assert.AreEqual<uint>(5, path[3]);

            Assert.AreEqual(11, result.Distance);
            Assert.IsTrue(result.IsFounded);
        }

        [TestMethod]
        public void Dijkstra_Not_Should_Find_Path_In_Graph()
        {
            var graph = new Graph<int, string>();

            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);

            graph.Connect(0, 1, 2, null);
            graph.Connect(0, 2, 3, null);
            graph.Connect(1, 3, 4, null);
            graph.Connect(2, 3, 2, null);
            graph.Connect(2, 4, 1, null);

            var dijkstra = new Dijkstra<int, string>(graph);
            var result = dijkstra.Process(0, 5);
            Assert.IsFalse(result.IsFounded);
        }
    }
}
