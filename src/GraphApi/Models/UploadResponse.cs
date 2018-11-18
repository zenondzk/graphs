using System;
using System.Collections.Generic;


namespace GraphApi.Models
{
  public class UploadResponse
  {
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
  }
}
