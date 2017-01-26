namespace Dijkstra.NET.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using ShortestPath;

    [TestClass]
    public class DijkstraTest
    {
        private TheShortestPathFixture _fixture;

        private readonly Func<Graph<int, string>, Dijkstra<int, string>> _dijkstraFactory = g => new Dijkstra<int, string>(g);

        [TestInitialize]
        public void Initialise()
        {
            _fixture = new TheShortestPathFixture();
        }

        [TestMethod]
        public void Dijkstra_Should_Find_Path_In_Multi_Paths_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(_dijkstraFactory);
        }

        [TestMethod, Timeout(2500)]
        public void Dijkstra_Should_Find_Path_In_Override_Node()
        {
            _fixture.Algorithm_Should_Find_Path_In_Override_Node(_dijkstraFactory);
        }

        [TestMethod]
        public void Dijkstra_Should_Find_Path_With_One_Vertex_In_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_With_One_Vertex_In_Graph(_dijkstraFactory);
        }

        [TestMethod]
        public void Dijkstra_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph(_dijkstraFactory);
        }

        [TestMethod]
        public void Dijkstra_Should_Find_Path_In_Multi_Edges_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_In_Multi_Edges_Graph(_dijkstraFactory);
        }

        [TestMethod]
        public void Dijkstra_Not_Should_Find_Path_In_Graph()
        {
            _fixture.Algorithm_Not_Should_Find_Path_In_Graph(_dijkstraFactory);
        }
    }
}
