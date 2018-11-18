using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using GraphApi.Framework.Attributes;

namespace GraphApi.Data.Entities
{
  [XmlRoot("graph")]
  public class Graph
  {
    [Required(ErrorMessage = "An id node is required for a graph.")]
    [XmlElement("id")]
    public string Id { get; set; }

    [Required(ErrorMessage = "An name node is required for a graph.")]
    [XmlElement("name")]
    public string Name { get; set; }

    [ListMustNotBeEmpty(ErrorMessage = "Graph must contain a nodes collection with at least one node defined")]
    [ListMustContainUniqueValues("Id", "All nodes are required to have a unique id.")]
    [XmlArray("nodes")]
    [XmlArrayItem("node")]
    public  List<Node> Nodes { get; set; }

    [ListMustContainUniqueValues("Id", "All nodes are required to have a unique id.")]
    [XmlArray("edges")]
    [XmlArrayItem("edge")]
    public  List<Edge> Edges { get; set; }
  }
}

