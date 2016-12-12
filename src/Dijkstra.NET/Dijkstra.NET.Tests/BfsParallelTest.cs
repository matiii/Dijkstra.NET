namespace Dijkstra.NET.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using ShortestPath;

    [TestClass]
    public class BfsParallelTest
    {
        private TheShortestPathFixture _fixture;
        private readonly Func<Graph<int, string>, Dijkstra<int, string>> _dijkstraFactory = g => new BfsParallel<int, string>(g);

        [TestInitialize]
        public void Initialise()
        {
            _fixture = new TheShortestPathFixture();
        }


        [TestMethod]
        public void BfsParallel_Should_Find_Path_In_Multi_Paths_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(_dijkstraFactory);
        }

        [TestMethod]
        public void Guard_In_BfsParallel_Should_Always_Works()
        {
            for (int i = 0; i < 500; i++)
                _fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(g => new BfsParallel<int, string>(g));
        }

        [TestMethod]
        public void BfsParallel_Should_Find_Path_With_One_Vertex_In_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_With_One_Vertex_In_Graph(_dijkstraFactory);
        }

        [TestMethod]
        public void BfsParallel_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph(_dijkstraFactory);
        }

        [TestMethod]
        public void BfsParallel_Should_Find_Path_In_Multi_Edges_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_In_Multi_Edges_Graph(_dijkstraFactory);
        }

        [TestMethod]
        public void BfsParallel_Not_Should_Find_Path_In_Graph()
        {
            _fixture.Algorithm_Not_Should_Find_Path_In_Graph(_dijkstraFactory);
        }
    }
}
