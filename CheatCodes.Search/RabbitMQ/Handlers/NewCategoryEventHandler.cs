using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.RabbitMQ.Models;
using CheatCodes.Search.Repositories;

namespace CheatCodes.Search.RabbitMQ.Handlers
{
  public interface INewCategoryEventHandler
  {
    Task Handle(NewCategoryEvent newCategory, CheatCodesDbContext context);
  }

  public class NewCategoryEventHandler : INewCategoryEventHandler
  {
    private readonly ICategoriesChangesRepository _categoriesChangesRepository;

    public NewCategoryEventHandler(ICategoriesChangesRepository categoriesChangesRepository)
    {
      _categoriesChangesRepository = categoriesChangesRepository;
    }

    public async Task Handle(NewCategoryEvent newCategory, CheatCodesDbContext context)
    {
      await _categoriesChangesRepository.NewCategory(newCategory, context);
    }
  }
}