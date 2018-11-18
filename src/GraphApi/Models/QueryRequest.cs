using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphApi.Models
{
  public class QueryRequest
  {
    public string GraphID { get; set; }
    public IList<QueryContainer> Queries { get; set; }
  }

}
