using CheatCodes.Search.RabbitMQ.Models;
using CheatCodes.Search.Repositories;

namespace CheatCodes.Search.RabbitMQ.Handlers
{
  public interface INewCategoryEventHandler
  {
    void Handle(NewCategoryEvent newCategory);
  }

  public class NewCategoryEventHandler : INewCategoryEventHandler
  {
    private readonly ICategoriesChangesRepository _categoriesChangesRepository;

    public NewCategoryEventHandler(ICategoriesChangesRepository categoriesChangesRepository)
    {
      _categoriesChangesRepository = categoriesChangesRepository;
    }

    public void Handle(NewCategoryEvent newCategory)
    {
      _categoriesChangesRepository.NewCategory(newCategory);
    }
  }
}