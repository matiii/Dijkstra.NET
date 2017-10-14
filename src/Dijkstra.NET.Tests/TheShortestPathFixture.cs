using System;
using Dijkstra.NET.Model;
using Xunit;
using System.Linq;

namespace Dijkstra.NET.Tests
{
    internal class TheShortestPathFixture
    {
        public void Algorithm_Should_Find_Path_In_Multi_Paths_Graph(Func<Graph<int, string>, ShortestPath.Dijkstra<int, string>> algorithm)
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
            graph.Connect(3, 5, 6, null);

            var dijkstra = algorithm(graph);
            var result = dijkstra.Process(0, 5);
            uint[] path = result.GetPath().ToArray();

            Dispose(dijkstra);

            Assert.Equal<uint>(0, path[0]);
            Assert.Equal<uint>(2, path[1]);
            Assert.Equal<uint>(3, path[2]);
            Assert.Equal<uint>(5, path[3]);

            Assert.Equal(11, result.Distance);
            Assert.True(result.IsFounded);
        }

        public void Algorithm_Should_Find_Path_In_Override_Node(Func<Graph<int, string>, ShortestPath.Dijkstra<int, string>> algorithm)
        {
            var graph = new Graph<int, string>();

            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);
            graph.AddNode(0);

            graph.Connect(0, 1, 2, null);
            graph.Connect(0, 2, 6, null);
            graph.Connect(1, 2, 2, null);
            graph.Connect(2, 3, 1, null);
            graph.Connect(3, 4, 1, null);
            graph.Connect(0, 5, 5, null);

            var dijkstra = algorithm(graph);
            var result = dijkstra.Process(0, 4);
            uint[] path = result.GetPath().ToArray();

            Dispose(dijkstra);

            Assert.Equal<uint>(0, path[0]);
            Assert.Equal<uint>(1, path[1]);
            Assert.Equal<uint>(2, path[2]);
            Assert.Equal<uint>(3, path[3]);
            Assert.Equal<uint>(4, path[4]);

            Assert.Equal(6, result.Distance);
            Assert.True(result.IsFounded);
        }

        public void Algorithm_Should_Find_Path_With_One_Vertex_In_Graph(Func<Graph<int, string>, ShortestPath.Dijkstra<int, string>> algorithm)
        {
            var graph = new Graph<int, string>();
            graph.AddNode(0);

            var dijkstra = algorithm(graph);
            var result = dijkstra.Process(0, 0);
            uint[] path = result.GetPath().ToArray();

            Dispose(dijkstra);

            Assert.Equal<uint>(0, path[0]);
            Assert.Equal(0, result.Distance);
            Assert.True(result.IsFounded);
        }

        public void Algorithm_Should_Find_Path_With_One_Vertex_And_One_Edge_In_Graph(Func<Graph<int, string>, ShortestPath.Dijkstra<int, string>> algorithm)
        {
            var graph = new Graph<int, string>();
            graph.AddNode(0);

            graph.Connect(0, 0, 5, null);

            var dijkstra = algorithm(graph);
            var result = dijkstra.Process(0, 0);
            uint[] path = result.GetPath().ToArray();

            Dispose(dijkstra);

            Assert.Equal<uint>(0, path[0]);
            Assert.Equal(0, result.Distance);
            Assert.True(result.IsFounded);
        }

        public void Algorithm_Should_Find_Path_In_Multi_Edges_Graph(Func<Graph<int, string>, ShortestPath.Dijkstra<int, string>> algorithm)
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

            var dijkstra = algorithm(graph);
            var result = dijkstra.Process(0, 5);
            uint[] path = result.GetPath().ToArray();

            Dispose(dijkstra);

            Assert.Equal<uint>(0, path[0]);
            Assert.Equal<uint>(2, path[1]);
            Assert.Equal<uint>(3, path[2]);
            Assert.Equal<uint>(5, path[3]);

            Assert.Equal(11, result.Distance);
            Assert.True(result.IsFounded);
        }

        public void Algorithm_Not_Should_Find_Path_In_Graph(Func<Graph<int, string>, ShortestPath.Dijkstra<int, string>> algorithm)
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

            var dijkstra = algorithm(graph);
            

            var result = dijkstra.Process(0, 5);

            Dispose(dijkstra);

            Assert.False(result.IsFounded);
        }

        private void Dispose(ShortestPath.Dijkstra<int, string> algorithm)
        {
            IDisposable disposable = algorithm as IDisposable;
            disposable?.Dispose();
        }
    }
}
