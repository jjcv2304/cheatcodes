using System.Threading.Tasks;
using CheatCodes.Search.RabbitMQ.Models;

namespace CheatCodes.Search.Repositories
{
  public interface ICategoriesChangesRepository
  {
    Task NewCategory(NewCategoryEvent newCategoryEvent);
    Task UpdateCategory(UpdateCategoryEvent newCategory);
    Task DeleteCategory(DeleteCategoryEvent newCategory);
  }
}