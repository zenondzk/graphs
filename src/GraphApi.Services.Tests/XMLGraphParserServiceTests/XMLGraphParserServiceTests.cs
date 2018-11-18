using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace GraphApi.Services.Tests.XMLGraphParserServiceTests
{
  [TestClass]
  public class XMLGraphParserServiceTests
  {
    [TestMethod]
    public void ValidateGraph_Should_ReturnCorrectErrors()
    {
      var service = new XMLGraphParserService("");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("format")).ShouldNotBeNull();

      service = new XMLGraphParserService(@"<graph></graph>");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("id")).ShouldNotBeNull();

      service = new XMLGraphParserService(@"<graph><id>test</id></graph>");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("name")).ShouldNotBeNull();

    }

    [TestMethod]
    public void ValidateGraphNodes_Should_ReturnCorrectErrors()
    {
      var service = new XMLGraphParserService(
        @"<graph>
            <id>test</id>
            <name>graph 1</name>
          </graph>");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("one")).ShouldNotBeNull();


      service = new XMLGraphParserService(
        @"<graph>
            <id>test</id>
            <name>graph 1</name>
            <nodes></nodes>
          </graph>");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("one")).ShouldNotBeNull();


      service = new XMLGraphParserService(
        @"<graph>
            <id>test</id>
            <name>graph 1</name>
            <nodes>
              <node><id>a</id></node>
              <node><id>a</id></node>
            </nodes>
          </graph>");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("unique")).ShouldNotBeNull();


      service = new XMLGraphParserService(
        @"<graph>
            <id>test</id>
            <name>graph 1</name>
            <nodes>
              <node><id>a</id></node>
              <node><id>b</id></node>
            </nodes>
          </graph>");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("name")).ShouldNotBeNull();

      service = new XMLGraphParserService(
        @"<graph>
            <id>test</id>
            <name>graph 1</name>
            <nodes>
              <node><id>a</id><name>test</name></node>
              <node><id>b</id><name>test2</name></node>
            </nodes>
          </graph>");
      service.IsValid().ShouldBeTrue();

    }

    [TestMethod]
    public void ValidateGraphEdges_Should_ReturnCorrectErrors()
    {
      var service = new XMLGraphParserService(
        @"<graph>
            <id>test</id>
            <name>graph 1</name>
             <nodes>
              <node><id>a</id><name>test</name></node>
              <node><id>b</id><name>test2</name></node>
            </nodes>
            <edges>
              <edge><id>a1</id><from>a</from><from>b</from><to>a</to></edge>
            </edges>
          </graph>");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("from")).ShouldNotBeNull();

      service = new XMLGraphParserService(
        @"<graph>
            <id>test</id>
            <name>graph 1</name>
             <nodes>
              <node><id>a</id><name>test</name></node>
              <node><id>b</id><name>test2</name></node>
            </nodes>
            <edges>
              <edge><id>a1</id><from>a</from><to>c</to></edge>
            </edges>
          </graph>");
      service.IsValid().ShouldBeFalse();
      service.Errors.FirstOrDefault(x => x.Contains("nodes")).ShouldNotBeNull();

      service = new XMLGraphParserService(
        @"<graph>
            <id>test</id>
            <name>graph 1</name>
             <nodes>
              <node><id>a</id><name>test</name></node>
              <node><id>b</id><name>test2</name></node>
            </nodes>
            <edges>
              <edge><id>a1</id><from>a</from><to>b</to></edge>
            </edges>
          </graph>");
      service.IsValid().ShouldBeTrue();

    }
  }
}

