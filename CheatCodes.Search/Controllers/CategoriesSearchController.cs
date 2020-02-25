using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.DB.Models;
using CheatCodes.Search.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheatCodes.Search.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriesSearchController : ControllerBase
  {
    private readonly ICategoriesSearchRepository _categoriesSearchRepository;

    public CategoriesSearchController(ICategoriesSearchRepository categoriesSearchRepository)
    {
      _categoriesSearchRepository = categoriesSearchRepository;
    }


    [HttpGet]
    public Category Get()
    {
      var textSearch = "Angular Cli";
      var result = _categoriesSearchRepository.GetCategoriesByPartialName(textSearch);
      return result;
    }
  }
}