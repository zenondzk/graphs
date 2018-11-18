using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace GraphApi.Framework.Attributes
{
  public class ListMustNotBeEmpty : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      if (value is IList list)
      {
        return list.Count > 0;
      }

      return false;
    }
  }
}

