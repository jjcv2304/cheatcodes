using System.Collections.Generic;
using Application;
using Application.Categories.Queries;
using Application.Utils;
using CSharpFunctionalExtensions;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Persistance.Utils;
using Presentation.Utils;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Presentation.Categories
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly Messages _messages;
        private readonly ICategoryQuery _categoryQuery;

        public CategoriesController(UnitOfWork unitOfWork, Messages messages) : base(unitOfWork)
        {
            _messages = messages;
            _categoryQuery = new CategoryQuery(unitOfWork);
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var result = _categoryQuery.AllParents();
            return Ok(result);
        }

        [Route("[action]/{parentId}")]
        public IActionResult GetChildsOf(int parentId)
        {
            var result = _categoryQuery.GetAllChilds(parentId);
            return Ok(result);
        }

        [Route("[action]/{categoryId}")]
        public IActionResult GetSiblingsOf(int categoryId)
        {
            var result = _categoryQuery.GetSiblingsOf(categoryId);
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