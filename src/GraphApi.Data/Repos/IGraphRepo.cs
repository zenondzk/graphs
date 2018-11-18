using GraphApi.Data.Entities;

namespace GraphApi.Data.Repos
{
  public interface IGraphRepo
  {
    Graph Get(string id);
    bool Create(Graph graph);
  }
}
