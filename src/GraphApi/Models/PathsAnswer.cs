using System.Collections.Generic;
namespace GraphApi.Models
{
    public class PathsAnswer:Answer
    {
      public IList<string> Paths { get; set; }
    }
}
