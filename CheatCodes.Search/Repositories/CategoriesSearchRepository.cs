using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.DB.Models;
using CheatCodes.Search.ViewModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CheatCodes.Search.Repositories
{
  public partial class CategoriesSearchRepository : ICategoriesSearchRepository
  {
    private readonly CheatCodesDbContext _context;

    public CategoriesSearchRepository(CheatCodesDbContext context)
    {
      _context = context;
    }

    public async Task<List<Category>> Test()
    {
      return await _context
        .Categories
        .Include(c => c.CategoryFields).ThenInclude(cf => cf.Field)
        .OrderBy(c => c.Id)
        .Take(10)
        .ToListAsync();
    }

    /*

     *
     */

    public Category GetCategoriesByPartialName(string partialName)
    {

      int rootId = 17;


      // var categories = _context.Categories
      //   .FromSqlInterpolated($@"    
      //WITH RECURSIVE ParentCategories (ID, Name, Description, ParentId)
      // AS(
      //   SELECT ID, Name, Description, ParentId
      //   FROM Category
      // Where contains(name,'{partialName}{'*'}') 
      // UNION ALL
      // SELECT e.ID, e.Name, e.ParentId, e.Description
      //   FROM Category e
      // INNER JOIN ParentCategories ON ParentCategories.ID = e.ParentId
      //   )
      // SELECT * FROM ParentCategories")
      //   .ToList();

      //var results = _context.Categories.FromSqlInterpolated(
      //  $"Select * from Category where name like '% {partialName} %'");

      var results = _context.Categories.FromSqlInterpolated(
        $"Select * from Category where name like {"%" + partialName + "%"}");
      var result = results.ToList();

      return null;
      //Category parentCategory = null;
      //parentCategory.ChildCategories = FindAllChildren(categories, parentCategory).ToList();

      //return parentCategory;
    }

    public async Task<Category> GetCategoriesByPartialName_b2(string partialName)
    {
      var categories = await _context
        .Categories
        .Include(c => c.ChildCategories)
        .Where(c => c.Name.Contains(partialName))
        .OrderBy(c => c.Id)
        .ToListAsync();

      var categoryParent = categories.FirstOrDefault(c => c.Name.Contains(partialName));

      var childs = FindAllChildren(categories, categoryParent);
      categoryParent.ChildCategories = childs.ToList();
      return categoryParent;
    }

    private static IEnumerable<Category> FindAllChildren(List<Category> children, Category category)
    {
      if (category == null) return Enumerable.Empty<Category>();
      var childrenByParentId = children.ToLookup(r => r.ParentId);
      return childrenByParentId[category != null ? category.Id : (int?)null].Expand(r => childrenByParentId[r.Id]);
    }
  }
}
