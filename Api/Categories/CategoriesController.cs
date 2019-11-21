using Application;
using Application.Utils;
using CSharpFunctionalExtensions;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Presentation.Utils;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Presentation.Categories
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly Messages _messages;

        public CategoriesController(Messages messages) : base()
        {
            _messages = messages;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var result = _messages.Dispatch(new GetCategoryAllParentsQuery());
            return Ok(result);
        }

        [Route("[action]/{parentId}")]
        public IActionResult GetChildsOf(int parentId)
        {
            var result = _messages.Dispatch(new GetCategoryAllChildsQuery(parentId));
            return Ok(result);
        }

        [Route("[action]/{categoryId}")]
        public IActionResult GetSiblingsOf(int categoryId)
        {
            var result = _messages.Dispatch(new GetCategorySiblingsQuery(categoryId));
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _messages.Dispatch(new GetCategoryByIdQuery(id));
            return Ok(result);
        }

        // GET api/values/name
        [HttpGet("{name}")]
        public IActionResult Get(string name, bool exactMatch = true)
        {
            var result = exactMatch ? 
                _messages.Dispatch(new GetCategoryByExactNameQuery(name)) : 
                _messages.Dispatch(new GetCategoryByPartialNameQuery(name));

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CategoryCreateDto categoryCreateDto)
        {
            CategoryCreateCommand categoryCreateCommand = MapService.Map(categoryCreateDto);
            Result result = _messages.Dispatch(categoryCreateCommand);
            return FromResult(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody]CategoryUpdateDto categoryUpdateDto)
        {
            CategoryUpdateCommand categoryUpdateCommand = MapService.Map(categoryUpdateDto);
            Result result = _messages.Dispatch(categoryUpdateCommand);
            return FromResult(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]CategoryDeleteDto categoryDeleteDto)
        {
            CategoryDeleteCommand categoryDeleteCommand = MapService.Map(categoryDeleteDto);
            Result result = _messages.Dispatch(categoryDeleteCommand);
            return FromResult(result);
        }
    }
}