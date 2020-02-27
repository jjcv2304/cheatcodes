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

    public async Task<List<CategoryBasicVM>> GetCategoriesByPartialNameAsync(string partialName)
    {
      var categories = await _context
        .Categories
        .Where(c => c.Name.ToLower().Contains(partialName.ToLower()))
        .Select(c => new CategoryBasicVM()
        {
          Id = c.Id,
          Name = c.Name
        })
        .OrderBy(c => c.Id)
        .ToListAsync();

      return categories;
    }

    //public async Task<CategoryNameTreeVM> GetCategoriesSubTreeByRootId(int rootId)
    //{
    //  var categories = await _context.Categories
    //    .FromSqlInterpolated($@"    
    //  WITH RECURSIVE ParentCategories (ID, Name, Description, ParentId)
    //   AS(
    //     SELECT ID, Name, Description, ParentId
    //     FROM Category
    //   Where Id = {rootId} 
    //   UNION ALL
    //   SELECT e.ID, e.Name, e.Description, e.ParentId
    //     FROM Category e
    //   INNER JOIN ParentCategories ON ParentCategories.ID = e.ParentId
    //     )
    //   SELECT * FROM ParentCategories")
    //    .Select(c => new CategoryNameTreeVM()
    //    {
    //      Id = c.Id,
    //      Name = c.Name,
    //      Description = c.Description,
    //      ParentId = c.ParentId,
    //      Childs = new List<CategoryNameTreeVM>()
    //    })
    //    .ToListAsync();

    //  var categoryParent = categories.SingleOrDefault(c => c.Id == rootId);
    //  if (categoryParent == null) return null;

    //  var childs = FindAllChildren(categories, categoryParent).ToList();
    //  categoryParent.Childs = childs;
    //  return categoryParent;
    //}

    //private static IEnumerable<CategoryNameTreeVM> FindAllChildren(List<CategoryNameTreeVM> children, CategoryNameTreeVM category)
    //{
    //  var childrenByParentId = children.ToLookup(r => r.ParentId);
    //  return childrenByParentId[category != null ? category.Id:(int?)null].Expand(r => childrenByParentId[r.Id]);
    //}

    public async Task<Category> GetCategoriesSubTreeByRootId(int rootId)
    {
      var categories = await _context.Categories
        .FromSqlInterpolated($@"    
      WITH RECURSIVE ParentCategories (ID, Name, Description, ParentId)
       AS(
         SELECT ID, Name, Description, ParentId
         FROM Category
       Where Id = {rootId} 
       UNION ALL
       SELECT e.ID, e.Name, e.Description, e.ParentId
         FROM Category e
       INNER JOIN ParentCategories ON ParentCategories.ID = e.ParentId
         )
       SELECT * FROM ParentCategories")
        .ToListAsync();

      var categoryParent = categories.SingleOrDefault(c => c.Id == rootId);

      //var childs = FindAllChildren(categories, categoryParent).ToList();
      //categoryParent.ChildCategories = childs;
      return categoryParent;
    }
    private static IEnumerable<Category> FindAllChildren(List<Category> children, Category category)
    {
      var childrenByParentId = children.ToLookup(r => r.ParentId);
      return childrenByParentId[category != null ? category.Id : (int?)null].Expand(r => childrenByParentId[r.Id]);
    }

  }
}
