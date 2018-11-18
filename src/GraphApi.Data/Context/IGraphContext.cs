using GraphApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GraphApi.Data.Context
{
  public interface IGraphContext
  {
    DbSet<Graph> Graphs { get; set; }
    DbSet<Node> Nodes { get; set; }

    DbSet<Edge> Edges { get; set; }

    int SaveChanges();

  }
}
