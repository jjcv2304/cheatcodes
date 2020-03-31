using System.Threading.Tasks;
using CheatCodes.Search.Repositories;
using CheatCodes.Search.Utils;
using CheatCodes.Search.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheatCodes.Search.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriesSearchController : BaseController
  {
    private readonly ICategoriesSearchRepository _categoriesSearchRepository;

    public CategoriesSearchController(ICategoriesSearchRepository categoriesSearchRepository)
    {
      _categoriesSearchRepository = categoriesSearchRepository;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetCategoriesByPartialNameAsync(string textSearch)
    {
      if (textSearch == null || textSearch.Length < 3)
        return ValidationProblem("The text search must be 3 or more characters long");

      var result = await _categoriesSearchRepository.GetCategoriesByPartialNameAsync(textSearch);
      return Ok(result);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetCategoriesByFiltersAsync([FromQuery] CategorySearchFiltersVM filtersVM)
    {
      var result = await _categoriesSearchRepository.GetCategoriesByFiltersAsync(filtersVM);
      return Ok(result);
    }

    [HttpGet]
    [Route("[action]/{categoryId}")]
    public async Task<IActionResult> GetCategoriesSubTreeByRootId(int categoryId)
    {
      var result = await _categoriesSearchRepository.GetCategoriesSubTreeByRootId(categoryId);
      return Ok(result);
    }
  }
}