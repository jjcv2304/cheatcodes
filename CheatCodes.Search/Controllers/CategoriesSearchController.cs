using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheatCodes.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesSearchController : ControllerBase
    {
      private readonly CheatCodesDbContext _context;

      public CategoriesSearchController(CheatCodesDbContext context)
      {
        _context = context;
      }


      [HttpGet]
      public async Task<List<Category>> Get()
      {
      return (await _context.Categories.ToListAsync());
    }
  }
}