using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphApi.Models
{
    public class QueryResponse
    {
      public IList<AnswerContainer> Answers { get; set; }
    }

}
