using System.Linq;
using Application.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CheatCodes.WebApi.Categories
{
    [Route("api/[controller]")]
    public class CategoriesController: Controller
    {
        private readonly IGetCategories _repository;

        public CategoriesController(IGetCategories repository)
        {
            _repository = repository;
        }
        
        // GET api/values
        [HttpGet]
        public CategoryVM Get()
        {
           return _repository.Execute().FirstOrDefault();
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public CategoryVM Get(int id)
        {
            return _repository.Execute().FirstOrDefault(c => c.Id==id);
        }
    }
}