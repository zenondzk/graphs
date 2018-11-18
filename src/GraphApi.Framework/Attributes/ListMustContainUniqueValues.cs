using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GraphApi.Framework.Attributes
{

  public class ListMustContainUniqueValues : ValidationAttribute
  {
    protected string PropertyName { get; }

    public ListMustContainUniqueValues(string propertyName, string errorMessage) : base(errorMessage)
    {
      PropertyName = propertyName;
    }

    public override bool IsValid(object value)
    {
      if (!(value is IList list))
        return false;

      var valueList = new List<string>();

      foreach (var item in list)
      {
        var propinfo = item.GetType().GetProperty(PropertyName);
        valueList.Add(propinfo.GetValue(item).ToString());
      }

      return valueList.Distinct().Count() == list.Count;

    }

  }
}

