using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.DB.Models;
using CheatCodes.Search.RabbitMQ.Models;
using Microsoft.EntityFrameworkCore;

namespace CheatCodes.Search.Repositories
{
  public class CategoriesChangesRepository : ICategoriesChangesRepository
  {

    public async Task NewCategory(NewCategoryEvent newCategoryEvent, CheatCodesDbContext context)
    {
      Category newParent = null;
      if (newCategoryEvent.ParentId != 0) newParent = new Category { Id = newCategoryEvent.ParentId };
      var newCategory = new Category
      {
        Id = newCategoryEvent.Id,
        Name = newCategoryEvent.Name,
        Description = newCategoryEvent.Description,
        ParentId = newParent?.Id
      };
      context.Add(newCategory);
      await context.SaveChangesAsync();
     
    }

    public async Task UpdateCategory(UpdateCategoryEvent updateCategoryEvent, CheatCodesDbContext context)
    {
      var updateCategory = await context.Categories.SingleAsync(c => c.Id == updateCategoryEvent.Id);
      updateCategory.Name = updateCategoryEvent.Name;
      updateCategory.Description = updateCategoryEvent.Description;
      await context.SaveChangesAsync();
    }

    public async Task DeleteCategory(DeleteCategoryEvent deleteCategoryEvent, CheatCodesDbContext context)
    {
      var deleteCategory = new Category
      {
        Id = deleteCategoryEvent.Id,
      };

      context.Attach(deleteCategory);
      context.Remove(deleteCategory);
      await context.SaveChangesAsync();


    }
  }
}