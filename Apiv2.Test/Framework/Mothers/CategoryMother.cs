using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Api.Test.Framework.Mothers
{
  public static class CategoryMother
  {
    public static Category Simple()
    {
      return new Category
      {
        Name = GetRandom.Name(),
      };
    }
    public static Category SimpleWithIdAndParent()
    {
      return new Category
      {
        Id = GetRandom.Id(),
        Name = GetRandom.Name(),
        Description = GetRandom.String(),
        ParentCategory = new Category{Id = GetRandom.Id()}
      };
    }
  }
}
