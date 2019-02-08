using System.Collections.Generic;
using Application.Categories.Commands;
using Application.Categories.Queries;
using Application.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Persistance.Utils;
using Presentation.Utils;

namespace Presentation.Categories
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryQuery _categoryQuery;
        private readonly ICategoryCommand _categoryCommand;

        public CategoriesController(UnitOfWork unitOfWork, ICategoryQuery categoryQuery,
            ICategoryCommand categoryCommand) : base(unitOfWork)
        {
            _categoryQuery = categoryQuery;
            _categoryCommand = categoryCommand;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var result= _categoryQuery.AllParents();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _categoryQuery.ById(id);
            base._unitOfWork.Commit();
            return Ok(result);
        }

        // GET api/values/name
        [HttpGet("{name}")]
        public IActionResult Get(string name, bool exactMatch = true)
        {
            var result = exactMatch
                ? _categoryQuery.ByExactName(name)
                : _categoryQuery.ByPartialName(name);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CategoryVM categoryVM)
        {
            _categoryCommand.Add(categoryVM);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(CategoryVM categoryVM)
        {
            _categoryCommand.Update(categoryVM);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Delete(CategoryVM categoryVM)
        {
            _categoryCommand.Delete(categoryVM);
            return Ok();
        }
    }
}