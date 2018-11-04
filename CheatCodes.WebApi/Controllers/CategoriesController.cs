using System.Collections.Generic;
using CheatCodes.WebApi.Models;
using CheatCodes.WebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace CheatCodes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController:ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        // GET api/values
        [HttpGet]
        public ICollection<Category> Get()
        {
            return _categoryService.Get();
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            var category = new Category()
            {
                Id = 0,
                Name = "C#",
                Description = "Programing language"
            };
            return category;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] CategoryVM category)
        {
            var l = category;
            // _categoryService.Add(category);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}