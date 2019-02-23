using System.Linq;
using Dijkstra.NET.Graph;
using Dijkstra.NET.Graph.Simple;
using Dijkstra.NET.ShortestPath;
using Xunit;

namespace Dijkstra.NET.Tests.ShortestPath
{
    public class DijkstraExtensionsTests
    {
        [Fact]
        public void DijkstraGraphShould_Find_Path_In_Multi_Paths_Graph()
        {
            var graph = new Graph<int, string>() + 0 + 0 + 0 + 0 + 0 + 0;

            graph.Connect(1, 2, 2, null);
            graph.Connect(1, 3, 3, null);
            graph.Connect(2, 4, 4, null);
            graph.Connect(3, 4, 2, null);
            graph.Connect(3, 5, 1, null);
            graph.Connect(4, 6, 6, null);

            var result = graph.Dijkstra(1, 6);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal<uint>(3, path[1]);
            Assert.Equal<uint>(4, path[2]);
            Assert.Equal<uint>(6, path[3]);

            Assert.Equal(11, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraSimpleGraphShould_Find_Path_In_Multi_Paths_Graph()
        {
            var graph = new Graph.Simple.Graph() + 6;

            var connected = graph >> 1 >> 2 ^ 2;
            Assert.True(connected);
            connected = graph >> 1 >> 3 ^ 3;
            Assert.True(connected);

            connected = graph >> 2 >> 4 ^ 4;
            Assert.True(connected);

            connected = graph >> 3 >> 4 ^ 2;
            Assert.True(connected);

            connected = graph >> 3 >> 5 ^ 1;
            Assert.True(connected);

            connected = graph >> 4 >> 6 ^ 6;
            Assert.True(connected);

            var result = graph.Dijkstra(1, 6);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal<uint>(3, path[1]);
            Assert.Equal<uint>(4, path[2]);
            Assert.Equal<uint>(6, path[3]);

            Assert.Equal(11, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraGraphShould_Find_Path_In_Override_Node()
        {
            var graph = new Graph<int, string>();

            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);

            graph.Connect(1, 2, 2, null);
            graph.Connect(1, 3, 6, null);
            graph.Connect(2, 3, 2, null);
            graph.Connect(3, 4, 1, null);
            graph.Connect(4, 5, 1, null);
            graph.Connect(1, 6, 5, null);

            var result = graph.Dijkstra(1, 5);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal<uint>(2, path[1]);
            Assert.Equal<uint>(3, path[2]);
            Assert.Equal<uint>(4, path[3]);
            Assert.Equal<uint>(5, path[4]);

            Assert.Equal(6, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraSimpleGraphShould_Find_Path_In_Override_Node()
        {
            var graph = new Graph.Simple.Graph();

            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();

            graph.Connect(1, 2, 2);
            graph.Connect(1, 3, 6);
            graph.Connect(2, 3, 2);
            graph.Connect(3, 4, 1);
            graph.Connect(4, 5, 1);
            graph.Connect(1, 6, 5);

            var result = graph.Dijkstra(1, 5);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal<uint>(2, path[1]);
            Assert.Equal<uint>(3, path[2]);
            Assert.Equal<uint>(4, path[3]);
            Assert.Equal<uint>(5, path[4]);

            Assert.Equal(6, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraGraphShould_Find_Path_With_One_Vertex_In_Graph()
        {
            var graph = new Graph<int, string>();
            graph.AddNode(0);

            var result = graph.Dijkstra(1, 1);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal(0, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraSimpleGraphShould_Find_Path_With_One_Vertex_In_Graph()
        {
            var graph = 1.ToGraph();

            var result = graph.Dijkstra(1, 1);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal(0, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraGraphShould_Find_Path_With_One_Vertex_In_Graph_And_Depth_Zero()
        {
            var graph = new Graph<int, string>();
            graph.AddNode(0);

            var result = graph.Dijkstra(1, 1, 0);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal(0, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraSimpleGraphShould_Find_Path_With_One_Vertex_In_Graph_And_Depth_Zero()
        {
            var graph = 1.ToGraph();

            var result = graph.Dijkstra(1, 1, 0);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal(0, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraGraphShould_Find_Path_With_One_Vertex_And_One_Edge_In_Graph()
        {
            var graph = new Graph<int, string>();
            graph.AddNode(0);

            graph.Connect(1, 1, 5, null);

            var result = graph.Dijkstra(1, 1);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal(0, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraSimpleGraphShould_Find_Path_With_One_Vertex_And_One_Edge_In_Graph()
        {
            var graph = 1.ToGraph();

            graph.Connect(1, 1, 5);

            var result = graph.Dijkstra(1, 1);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal(0, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraGraphShould_Find_Path_In_Multi_Edges_Graph()
        {
            var graph = new Graph<int, string>();

            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);

            graph.Connect(1, 2, 2, null);
            graph.Connect(1, 3, 3, null);
            graph.Connect(2, 4, 4, null);
            graph.Connect(3, 4, 3, null);
            graph.Connect(3, 4, 2, null);
            graph.Connect(3, 4, 4, null);
            graph.Connect(3, 5, 1, null);
            graph.Connect(4, 6, 6, null);

            var result = graph.Dijkstra(1, 6);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal<uint>(3, path[1]);
            Assert.Equal<uint>(4, path[2]);
            Assert.Equal<uint>(6, path[3]);

            Assert.Equal(11, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraSimpleGraphShould_Find_Path_In_Multi_Edges()
        {
            var graph = new Graph.Simple.Graph();

            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();

            graph.Connect(1, 2, 2);
            graph.Connect(1, 3, 3);
            graph.Connect(2, 4, 4);
            graph.Connect(3, 4, 3);
            graph.Connect(3, 4, 2);
            graph.Connect(3, 4, 4);
            graph.Connect(3, 5, 1);
            graph.Connect(4, 6, 6);

            var result = graph.Dijkstra(1, 6);
            uint[] path = result.GetPath().ToArray();

            Assert.Equal<uint>(1, path[0]);
            Assert.Equal<uint>(3, path[1]);
            Assert.Equal<uint>(4, path[2]);
            Assert.Equal<uint>(6, path[3]);

            Assert.Equal(11, result.Distance);
            Assert.True(result.IsFounded);
        }

        [Fact]
        public void DijkstraGraphNot_Should_Find_Path_In_Graph()
        {
            var graph = new Graph<int, string>();

            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);

            graph.Connect(1, 2, 2, null);
            graph.Connect(1, 3, 3, null);
            graph.Connect(2, 4, 4, null);
            graph.Connect(3, 4, 2, null);
            graph.Connect(3, 5, 1, null);

            var result = graph.Dijkstra(1, 6);

            Assert.False(result.IsFounded);
        }

        [Fact]
        public void DijkstraSimpleGraphNot_Should_Find_Path_In_Graph()
        {
            var graph = new Graph.Simple.Graph();

            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();

            graph.Connect(1, 2, 2);
            graph.Connect(1, 3, 3);
            graph.Connect(2, 4, 4);
            graph.Connect(3, 4, 2);
            graph.Connect(3, 5, 1);

            var result = graph.Dijkstra(1, 6);

            Assert.False(result.IsFounded);
        }

        [Fact]
        public void Dijkstra_Should_Concern_Depth_In_Graph()
        {
            var graph = new Graph<int, string>();

            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);

            graph.Connect(1, 2, 1, null);
            graph.Connect(1, 3, 1, null);
            graph.Connect(1, 4, 5, null);
            graph.Connect(3, 4, 2, null);

            var result = graph.Dijkstra(1, 4, 1);

            Assert.True(result.IsFounded);
            Assert.Equal(5, result.Distance);

            uint[] path = result.GetPath().ToArray();

            Assert.Equal((uint)1, path[0]);
            Assert.Equal((uint)4, path[1]);
        }

        [Fact]
        public void Dijkstra_Should_Concern_Depth_In_SimpleGraph()
        {
            var graph = new Graph.Simple.Graph();

            graph.AddNode();
            graph.AddNode();
            graph.AddNode();
            graph.AddNode();

            graph.Connect(1, 2, 1);
            graph.Connect(1, 3, 1);
            graph.Connect(1, 4, 5);
            graph.Connect(3, 4, 2);

            var result = graph.Dijkstra(1, 4, 1);

            Assert.True(result.IsFounded);
            Assert.Equal(5, result.Distance);

            uint[] path = result.GetPath().ToArray();

            Assert.Equal((uint)1, path[0]);
            Assert.Equal((uint)4, path[1]);
        }
    }
}