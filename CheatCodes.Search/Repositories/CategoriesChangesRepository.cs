using System;
using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.DB.Models;
using CheatCodes.Search.RabbitMQ.Models;

namespace CheatCodes.Search.Repositories
{
  public class CategoriesChangesRepository : ICategoriesChangesRepository
  {
    private readonly CheatCodesDbContext _context;

    public CategoriesChangesRepository(CheatCodesDbContext context)
    {
      _context = context;
    }

    public async Task NewCategory(NewCategoryEvent newCategoryEvent)
    {
      Category newParent = null;
      if (newCategoryEvent.ParentId != 0) newParent = new Category {Id = newCategoryEvent.ParentId};
      var newCategory = new Category
      {
        Id = newCategoryEvent.Id,
        Name = newCategoryEvent.Name,
        Description = newCategoryEvent.Description,
        ParentCategory = newParent
      };

      _context.Add(newCategory);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateCategory(UpdateCategoryEvent updateCategoryEvent)
    {
      throw new NotImplementedException();
    }

    public async Task DeleteCategory(DeleteCategoryEvent deleteCategoryEvent)
    {
      throw new NotImplementedException();
    }
  }
}