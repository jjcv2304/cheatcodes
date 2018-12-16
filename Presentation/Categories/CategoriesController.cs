using System.Collections.Generic;
using Application.Categories.Queries;
using Application.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Persistance.Utils;
using Presentation.Utils;

namespace Presentation.Categories
{
    [Route("api/[controller]")]
    public class CategoriesController: BaseController
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoriesController(UnitOfWork unitOfWork, ICategoryQuery categoryQuery):base(unitOfWork)
        {
            _categoryQuery = categoryQuery;
        }
       
        // GET api/values
        [HttpGet]
        public List<CategoryVM> Get()
        {
           return _categoryQuery.All();
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public CategoryVM Get(int id)
        {
            var result= _categoryQuery.ById(id);
            base._unitOfWork.Commit();
            return result;
        }
        
        // GET api/values/name
        [HttpGet("{name}")]
        public List<CategoryVM> Get(string name, bool exactMatch=true)
        {
            return exactMatch ?
                _categoryQuery.ByExactName(name) 
                :
                _categoryQuery.ByPartialName(name);
        }
    }
}