using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphApi.Data.Entities;
using Shouldly;
using GraphApi.Services.Tests.PathFinderServiceTests;

namespace GraphApi.Services.Tests
{
  [TestClass]
  public class FindAllPathsTests
  {
    [TestClass]
    public class PathFinderServiceTests
    { 

      [TestMethod]
      public void TwoNodeQuery_Should_ReturnCorrectPaths()
      {
        var service = new PathFinderService(GraphProvider.GetGraph());
        var paths = service.FindAllPaths("a", "b");
        paths.Count.ShouldBe(1);
      }

      [TestMethod]
      public void MultiplePathQuery_Should_ReturnCorrectPaths()
      {
        var service = new PathFinderService(GraphProvider.GetGraph());
        var paths = service.FindAllPaths("a", "c");
        paths.Count.ShouldBe(2);
      }

      [TestMethod]
      public void UnlinkedPathQuery_Should_ReturnNoPaths()
      {
        var service = new PathFinderService(GraphProvider.GetGraph());
        var paths = service.FindAllPaths("e", "h");
        paths.Count.ShouldBe(0);
      }

      [TestMethod]
      public void ComplexPath_Should_ReturnCorrectPaths()
      {
        var service = new PathFinderService(GraphProvider.GetGraph());
        var paths = service.FindAllPaths("g", "e");
        paths.Count.ShouldBe(3);
      }

    }
  }
}
