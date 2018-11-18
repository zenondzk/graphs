using GraphApi.Models;
using GraphApi.Services;
using GraphApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace GraphApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UploadController:ControllerBase
    {
    IGraphService graphService;

    public UploadController(IGraphService service)
    {
      graphService = service;

    }
    [HttpPost]
    public string Post([FromBody] UploadRequest request)
    {
      string xml;
      try
      {
        var webRequest = WebRequest.Create(request.Url);

        using (var response = webRequest.GetResponse())
        using (var content = response.GetResponseStream())
        using (var reader = new StreamReader(content))
        {
          xml = reader.ReadToEnd();
        }

        var service = new XMLGraphParserService(xml);
        if (!service.IsValid() && graphService.Create(service.Graph))
        {
          return JsonConvert.SerializeObject(new UploadResponse { Success = false, Errors = service.Errors }, Formatting.Indented);
        }

        return JsonConvert.SerializeObject(new UploadResponse { Success = true });
      }catch(Exception ex)
      {
        return JsonConvert.SerializeObject(new UploadResponse { Success = false, Errors = new List<string> { ex.Message } });
      }

    }
  }
}
