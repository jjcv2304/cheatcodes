using System.Linq;
using Application.Categories.Queries;
using Microsoft.AspNetCore.Mvc;
using Persistance.Utils;
using Presentation.Utils;

namespace CheatCodes.WebApi.Categories
{
    [Route("api/[controller]")]
    public class CategoriesController: BaseController
    {
        private readonly IGetCategories _repository;

        public CategoriesController(UnitOfWork unitOfWork, IGetCategories repository):base(unitOfWork)
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
            var result= _repository.Execute().FirstOrDefault(c => c.Id==id);
            base._unitOfWork.Commit();
            return result;
        }
    }
}