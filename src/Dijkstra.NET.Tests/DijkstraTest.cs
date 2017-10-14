using System;
using Dijkstra.NET.Model;
using Xunit;

namespace Dijkstra.NET.Tests
{
    public class DijkstraTest
    {
        private readonly TheShortestPathFixture _fixture;

        private readonly Func<Graph<int, string>, ShortestPath.Dijkstra<int, string>> _dijkstraFactory = g => new ShortestPath.Dijkstra<int, string>(g);

        public DijkstraTest()
        {
            _fixture = new TheShortestPathFixture();
        }

        [Fact]
        public void Dijkstra_Should_Find_Path_In_Multi_Paths_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(_dijkstraFactory);
        }

        [Fact]
        public void Dijkstra_Should_Find_Path_In_Override_Node()
        {
            _fixture.Algorithm_Should_Find_Path_In_Override_Node(_dijkstraFactory);
        }

        [Fact]
        public void Dijkstra_Should_Find_Path_With_One_Vertex_In_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_With_One_Vertex_In_Graph(_dijkstraFactory);
        }

        [Fact]
        public void Dijkstra_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph(_dijkstraFactory);
        }

        [Fact]
        public void Dijkstra_Should_Find_Path_In_Multi_Edges_Graph()
        {
            _fixture.Algorithm_Should_Find_Path_In_Multi_Edges_Graph(_dijkstraFactory);
        }

        [Fact]
        public void Dijkstra_Not_Should_Find_Path_In_Graph()
        {
            _fixture.Algorithm_Not_Should_Find_Path_In_Graph(_dijkstraFactory);
        }
    }
}
