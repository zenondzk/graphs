using GraphApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphApi.Services.Tests.PathFinderServiceTests
{
  public static class GraphProvider
  {
    public static Graph GetGraph()
    {
      return new Graph()
      {
        Nodes = new List<Node>
        {
          new Node {Id = "a"},
          new Node {Id = "b"},
          new Node {Id = "c"},
          new Node {Id = "d"},
          new Node {Id = "e"},
          new Node {Id = "f"},
          new Node {Id = "g"},
          new Node {Id = "h"}
        },
        Edges = new List<Edge>
        {
          new Edge {From = "a", To = "b", Cost = 2},
          new Edge {From = "b", To = "c", Cost = 3},
          new Edge {From = "a", To = "d", Cost = 4},
          new Edge {From = "d", To = "c", Cost = 2},
          new Edge {From = "c", To = "e"},
          new Edge {From = "f", To = "g"},
          new Edge {From = "g", To = "a"},
          new Edge {From = "g", To = "e"},
          new Edge {From = "e", To = "f"}
        }
      };
    }
  }
}
