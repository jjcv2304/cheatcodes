using System.Collections.Generic;
using System.Threading.Tasks;
using CheatCodes.Search.DB.Models;
using CheatCodes.Search.ViewModels;

namespace CheatCodes.Search.Repositories
{
  public interface ICategoriesSearchRepository
  {
    Task<List<Category>> Test();
    Task<List<CategoryBasicVM>> GetCategoriesByPartialNameAsync(string partialName);
    Task<Category> GetCategoriesSubTreeByRootId(int rootId);
  }
}