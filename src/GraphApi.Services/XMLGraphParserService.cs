using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using GraphApi.Data.Entities;

namespace GraphApi.Services
{

  public class XMLGraphParserService
  {
    protected string XML { get; }
    public Graph Graph { get; }

    public List<string> Errors { get; }

    public XMLGraphParserService(string xml)
    {
      Errors = new List<string>();
      XML = xml;
      try
      {
        var serializer = new XmlSerializer(typeof(Graph));
        serializer.UnknownNode += UnknownNode;
        using (TextReader reader = new StringReader(XML))
        {
          Graph = (Graph)serializer.Deserialize(reader);
        }
      }
      catch (Exception ex)
      {
        Graph = null;
      }
    }

    public void UnknownNode(object sender, XmlNodeEventArgs e)
    {
      if (e.LocalName == "from" && e.ObjectBeingDeserialized.GetType() == typeof(Edge))
      {
        Errors.Add("Only one 'from' node is allowed in an edge.");
      }
      if (e.LocalName == "to" && e.ObjectBeingDeserialized.GetType() == typeof(Edge))
      {
        Errors.Add("Only one 'to' node is allowed in an edge.");
      }
    }

    public bool IsValid()
    {
      if (Graph == null || Errors.Any())
      {
        Errors.Add("Xml document is not inexpected format.");
        return false;
      }

      var validationResults = new List<ValidationResult>();
      Validator.TryValidateObject(Graph, new ValidationContext(Graph, null, null), validationResults, true);
      Graph.Nodes?.ToList().ForEach(x =>
        Validator.TryValidateObject(x, new ValidationContext(x, null, null), validationResults, true));

      Graph.Edges?.ToList().ForEach(x =>
        Validator.TryValidateObject(x, new ValidationContext(x, null, null), validationResults, true));

      if (validationResults.Any())
      {
        Errors.AddRange(validationResults.Select(x => x.ErrorMessage).ToList());
      }

      if (!EdgesAreValid())
      {
        Errors.Add("Not all to and from are in the nodes specified.");
      }
      
      return !Errors.Any();
    }

    private bool EdgesAreValid()
    {
      var allFromValid = !Graph.Edges?.Select(x => x.From).Except(Graph.Nodes?.Select(x => x.Id) ?? new List<string>()).Any() ?? false;

      var allToValid = !Graph.Edges?.Select(x => x.To).Except(Graph.Nodes?.Select(x => x.Id) ?? new List<string>()).Any() ?? false;

      return allFromValid && allToValid;
    }


    private void ValidateNodesCollection(XElement nodes, List<string> errors)
    {
      if (nodes == null || !nodes.Elements("node").Any())
      {
        errors.Add("Graph must contain at least one node in nodes collection");
      }
      else
      {
        var ids = nodes.Elements().Select(x => x.Element("id")?.Value).ToList().Distinct().ToList();

        if (nodes.Elements().Select(x => x.Element("id")?.Value).ToList().Distinct().Count() != nodes.Elements("node").Count())
        {
          errors.Add("All nodes must have a unique id node");
        }
      }
    }
  }
}

