using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using GraphApi.Data.Entities;

namespace GraphApi.Services.Tests.PathFinderServiceTests
{
  [TestClass]
    public class FindCheapestPathTests
    {
    [TestMethod]
    public void SimpleCheapest_Should_ReturnCorrectPath()
    {
      var service = new PathFinderService(GraphProvider.GetGraph());
      var path = service.FindCheapestPath("a", "c");
      path.ShouldBe("a,b,c");
    }
  }
}
