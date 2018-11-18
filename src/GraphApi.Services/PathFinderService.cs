using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphApi.Data.Entities;

namespace GraphApi.Services
{
  public class PathFinderService
  {
    private static readonly object CounterLock = new object();

    private static decimal cheapestCost;
    private static string cheapestPath;

    protected Graph Graph { get; }


    public PathFinderService(Graph graph)
    {
      Graph = graph;
    }


    public string FindCheapestPath(string startid, string endid)
    {
      cheapestCost = Decimal.MaxValue;
      cheapestPath = string.Empty;
      var task = FindCheapest(startid, endid, string.Empty, 0);
      task.Wait();
      return cheapestPath;
    }


    public List<string> FindAllPaths(string startid, string endid)
    {
      Paths = new ConcurrentStack<string>();
      var task = FindPath(startid, endid, string.Empty, 0);
      task.Wait();
      return Paths.ToList();
    }

    private async Task FindCheapest(string currentid, string endid, string visited, decimal cost)
    {
    
      var list = string.IsNullOrEmpty(visited) ? new List<string>() : visited.Split(',').ToList();
      list.Add(currentid);
      if (currentid == endid)
      {
        if (cost >= cheapestCost) return;
        lock (CounterLock)
        {
          cheapestPath = string.Join(",", list);
          cheapestCost = cost;
        }
        return;
      }

      var adjacent = Graph.Edges.Where(x => x.From == currentid);
      foreach (var adj in adjacent)
      {
        var newcost = cost + adj.Cost;
        if (!(visited.Contains(adj.To) || newcost >= cheapestCost))
        {
          await FindCheapest(adj.To, endid, string.Join(",", list), cost + adj.Cost ?? 0);
        }
      }

    }

    private ConcurrentStack<string> Paths;

    private async Task FindPath(string currentid, string endid, string visited, decimal cost)
    {
      var list = string.IsNullOrEmpty(visited) ? new List<string>() : visited.Split(',').ToList();
      list.Add(currentid);
      if (currentid == endid)
      {
        Paths.Push(string.Join(",", list));
        return;
      }

      var adjacent = Graph.Edges.Where(x => x.From == currentid);
      foreach (var adj in adjacent)
      {
        var newcost = cost + adj.Cost;
        if (!visited.Contains(adj.To))
        {
          await FindPath(adj.To, endid, string.Join(",", list), cost + adj.Cost ?? 0);
        }

      }

    }
  }
}

