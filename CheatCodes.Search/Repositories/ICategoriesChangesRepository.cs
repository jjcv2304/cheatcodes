using System.Threading.Tasks;
using CheatCodes.Search.RabbitMQ.Models;

namespace CheatCodes.Search.Repositories
{
  public interface ICategoriesChangesRepository
  {
    Task NewCategory(NewCategoryEvent newCategoryEvent);
    Task UpdateCategory(NewCategoryEvent newCategory);
    Task DeleteCategory(NewCategoryEvent newCategory);
  }
}