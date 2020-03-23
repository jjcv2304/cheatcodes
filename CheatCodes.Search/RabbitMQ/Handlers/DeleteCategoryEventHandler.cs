using CheatCodes.Search.DB;
using CheatCodes.Search.RabbitMQ.Models;
using CheatCodes.Search.Repositories;

namespace CheatCodes.Search.RabbitMQ.Handlers
{
  public interface IDeleteCategoryEventHandler
  {
    void Handle(DeleteCategoryEvent deleteCategoryEvent, CheatCodesDbContext context);
  }

  public class DeleteCategoryEventHandler : IDeleteCategoryEventHandler
  {
    private readonly ICategoriesChangesRepository _categoriesChangesRepository;

    public DeleteCategoryEventHandler(ICategoriesChangesRepository categoriesChangesRepository)
    {
      _categoriesChangesRepository = categoriesChangesRepository;
    }

    public void Handle(DeleteCategoryEvent deleteCategoryEvent, CheatCodesDbContext context)
    {
      _categoriesChangesRepository.DeleteCategory(deleteCategoryEvent, context); 
    }
  }
}