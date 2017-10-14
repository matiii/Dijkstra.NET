using System;
using Dijkstra.NET.Model;
using Dijkstra.NET.ShortestPath;
using Xunit;

namespace Dijkstra.NET.Tests
{
    public class BfsParallelTest
    {
        private readonly TheShortestPathFixture _fixture;

        private readonly Func<Graph<int, string>, Dijkstra<int, string>> _dijkstraFactory = g => new BfsParallel<int, string>(g);

        public BfsParallelTest()
        {
            _fixture = new TheShortestPathFixture();
        }

        [Fact]
        public void BfsParallel_Should_Find_Path_In_Multi_Paths_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(_dijkstraFactory);
        }

        [Fact]
        public void Guard_In_BfsParallel_Should_Always_Works()
        {
            for (int i = 0; i < 500; i++)
                _fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(g => new BfsParallel<int, string>(g));
        }

        [Fact]
        public void BfsParallel_Should_Find_Path_With_One_Vertex_In_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_With_One_Vertex_In_Graph(_dijkstraFactory);
        }

        [Fact]
        public void BfsParallel_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph(_dijkstraFactory);
        }

        [Fact]
        public void BfsParallel_Should_Find_Path_In_Multi_Edges_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_In_Multi_Edges_Graph(_dijkstraFactory);
        }

        [Fact]
        public void BfsParallel_Not_Should_Find_Path_In_Graph()
        {
            _fixture.Algorithm_Not_Should_Find_Path_In_Graph(_dijkstraFactory);
        }
    }
}
