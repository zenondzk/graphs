using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace GraphApi.Data.Entities
{

  [XmlType("edge")]
  public class Edge
  {
    [Required(ErrorMessage = "An Edge requires an id")]
    [XmlElement("id")]
    public string Id { get; set; }

    [Required(ErrorMessage = "An Edge requires a from")]
    [XmlElement("from")]
    public string From { get; set; }

    [Required(ErrorMessage = "An Edge requires a to")]
    [XmlElement("to")]
    public string To { get; set; }

    [XmlElement("cost")]
    public decimal? Cost { get; set; }

    public string GraphId { get; set; }
    public Node ToNode { get; set; }

    public Node FromNode { get; set; }
  }
}

