namespace Dijkstra.NET.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using ShortestPath;

    [TestClass]
    public class BfsParallelTest
    {

        [TestMethod]
        public void BfsParallel_Should_Find_Path_In_Multi_Paths_Graph()
        {
            var fixture = new TheShortestPathFixture();
            fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(g => new BfsParallel<int, string>(g));
        }
    }
}
