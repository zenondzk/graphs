using GraphApi.Data.Entities;
using GraphApi.Data.Repos;
using GraphApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphApi.Services
{
  public class GraphService: IGraphService
  {
    IGraphRepo graphRepo;
    public GraphService(IGraphRepo repo)
    {
      graphRepo = repo;
    }

    public bool Create(Graph graph)
    {
      return graphRepo.Create(graph); ;
    }

    public Graph Get(string id)
    {
      return graphRepo.Get(id);
    }
  }
}
