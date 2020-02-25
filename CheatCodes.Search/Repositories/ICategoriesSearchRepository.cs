using System.Collections.Generic;
using System.Threading.Tasks;
using CheatCodes.Search.DB.Models;

namespace CheatCodes.Search.Repositories
{
  public interface ICategoriesSearchRepository
  {
    Task<List<Category>> Test();
    Category GetCategoriesByPartialName(string partialName);
  }
}