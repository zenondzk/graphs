
using GraphApi.Models;
using GraphApi.Services;
using GraphApi.Services.Interfaces;
using GraphApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace GraphApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class QueryController : ControllerBase
  {
    IGraphService graphservice;

    public QueryController(IGraphService service)
    {
      graphservice = service;
    }
    // POST api/values
    [HttpPost]
    public string Post([FromBody] QueryRequest request)
    {
      var graph = graphservice.Get(request.GraphID);
      var pathfinder = new PathFinderService(graph);
      var result = new List<AnswerContainer>();

      foreach (var query in request.Queries)
      {

        if (query.Cheapest != null)
        {
          var path = pathfinder.FindCheapestPath(query.Cheapest.Start, query.Cheapest.End);
          result.Add(new AnswerContainer
          {
            Cheapest =
            new CheapestAnswer
            {
              Path = string.IsNullOrEmpty(path) ? "false" : path,
              From = query.Cheapest.Start,
              To = query.Cheapest.End
            }
          });
        }
        if (query.Paths != null)
        {
          var paths = pathfinder.FindAllPaths(query.Paths.Start, query.Paths.End);
          result.Add(new AnswerContainer
          {
            Paths =
            new PathsAnswer
            {
              Paths = paths,
              From = query.Paths.Start,
              To = query.Paths.End
            }
          });
        }
      }
      return JsonConvert.SerializeObject(new QueryResponse { Answers = result },
        Formatting.Indented,
        new JsonSerializerSettings
        {
          NullValueHandling = NullValueHandling.Ignore,
          ContractResolver = new LowercaseContractResolver()
        });
    }
  }
}
