using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;

namespace GraphApi.Data.Entities
{
  [XmlType("node")]
  public class Node
  {
    [Required(ErrorMessage = "A Node requires an id")]
    [XmlElement("id")]
    public string Id { get; set; }

    [Required(ErrorMessage = "A node requires a name")]
    [XmlElement("name")]
    public string Name { get; set; }

    public string GraphId { get; set; }
  }
}
