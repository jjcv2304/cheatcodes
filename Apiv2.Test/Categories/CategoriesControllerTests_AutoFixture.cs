using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Domain;
using Xunit;

namespace Api.Test.Categories
{
  public class CategoriesControllerTests_AutoFixture
  {

    [Fact]
    public async Task RandomCategories()
    {
      var fixture = new Fixture();
      try
      {

        var cat3 = fixture.Build<Category>()
          .Without(f => f.ParentCategory)
          .Without(f => f.ChildCategories)
          .Without(f => f.CategoryFields)
          .Create();

      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }



    }
  }
}
