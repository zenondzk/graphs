using GraphApi.Data.Context;
using GraphApi.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GraphApi.Data.Repos
{
  public class GraphRepo : IGraphRepo
  {
    IGraphContext graphContext;
    public GraphRepo(IGraphContext context)
    {
      graphContext = context;
    }


    public Graph Get(string id)
    {
      var graph = graphContext.Graphs.Include(x => x.Nodes).Include(x => x.Edges).FirstOrDefault(x => x.Id == id);
      return graph;
    }

    public bool Create(Graph graph)
    {
      graphContext.Graphs.Add(graph);
      return graphContext.SaveChanges() > 0;
    }
  }
}
