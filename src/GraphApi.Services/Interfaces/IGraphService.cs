using GraphApi.Data.Entities;


namespace GraphApi.Services.Interfaces
{
  public interface IGraphService
  {
    Graph Get(string id);
    bool Create(Graph graph);
  }
}
