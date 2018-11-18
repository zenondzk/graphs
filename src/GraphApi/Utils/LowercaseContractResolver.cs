using Newtonsoft.Json.Serialization;

namespace GraphApi.Utils
{
  public class LowercaseContractResolver : DefaultContractResolver
  {
    protected override string ResolvePropertyName(string propertyName)
    {
      return propertyName.ToLower();
    }

  }
}
