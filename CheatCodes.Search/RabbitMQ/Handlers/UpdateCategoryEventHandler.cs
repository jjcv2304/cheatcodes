using CheatCodes.Search.RabbitMQ.Models;
using CheatCodes.Search.Repositories;

namespace CheatCodes.Search.RabbitMQ.Handlers
{
  public interface IUpdateCategoryEventHandler
  {
    void Handle(UpdateCategoryEvent updateCategoryEvent);
  }

  public class UpdateCategoryEventHandler : IUpdateCategoryEventHandler
  {
    private readonly ICategoriesChangesRepository _categoriesChangesRepository;

    public UpdateCategoryEventHandler(ICategoriesChangesRepository categoriesChangesRepository)
    {
      _categoriesChangesRepository = categoriesChangesRepository;
    }

    public void Handle(UpdateCategoryEvent updateCategoryEvent)
    {
      _categoriesChangesRepository.UpdateCategory(updateCategoryEvent);
    }
  }
}