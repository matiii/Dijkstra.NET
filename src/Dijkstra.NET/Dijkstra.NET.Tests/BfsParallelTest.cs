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

        [TestMethod]
        public void Guard_In_BfsParallel_Should_Always_Works()
        {
            var fixture = new TheShortestPathFixture();

            for (int i = 0; i < 100; i++)
                fixture.Algorithm_Should_Find_Path_In_Multi_Paths_Graph(g => new BfsParallel<int, string>(g));
        }
    }
}
