using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.RabbitMQ.Models;

namespace CheatCodes.Search.Repositories
{
  public interface ICategoriesChangesRepository
  {
    Task NewCategory(NewCategoryEvent newCategoryEvent, CheatCodesDbContext context);
    Task UpdateCategory(UpdateCategoryEvent newCategory, CheatCodesDbContext context);
    Task DeleteCategory(DeleteCategoryEvent newCategory, CheatCodesDbContext context);
  }
}