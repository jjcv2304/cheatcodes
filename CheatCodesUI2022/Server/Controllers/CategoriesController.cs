using Api.Utils;
using Application;
using Application.Utils;
using AutoMapper;
using CheatCodesUI2022.Shared;
using CSharpFunctionalExtensions;
using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Api.Categories
{
    //[TypeFilter(typeof(TrackPerformance))]
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly Messages _messages;
        private readonly IMapper _mapper;

        public CategoriesController(Messages messages, IMapper mapper)
        {
            _messages = messages;
            _mapper = mapper;
        }

        // GET api/values
        //[HttpGet]
        ////public IActionResult Get()
        //public IEnumerable<CategoryVM> Get()
        //{
        //    var result = _messages.Dispatch(new GetCategoryAllParentsQuery());

        //    var categoryVms = _mapper.Map<List<CategoryDto>, List<CategoryVM>>(result!);
        //    return categoryVms;
        //}

        [HttpGet]
        public IActionResult Get()
        {
            var result = _messages.Dispatch(new GetCategoryAllParentsQuery());

            var categoryVms = _mapper.Map<List<CategoryDto>, List<CategoryVM>>(result!);
            return Ok(categoryVms);
        }

        [HttpGet]
        [Route("[action]/{rootId}")]
        public IActionResult GetBreadCrumbs(int rootId)
        {
            if (rootId < 1) return BadRequest("The rootId must be greater than 0");

            var result = _messages.Dispatch(new GetCategoryBreadCrumbs(rootId));
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{parentId}")]
        //  public IActionResult GetChildsOf(int parentId)
        public IEnumerable<CategoryVM> GetChildsOf(int parentId)
        {
            var result = _messages.Dispatch(new GetCategoryAllChildsQuery(parentId));

            var categoryVms = _mapper.Map<List<CategoryDto>, List<CategoryVM>>(result!);

          //  return Ok(categoryVms);
          return categoryVms;
        }

        /// <summary>
        ///   Get categories that are located at the same tree level
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
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
        //[HttpGet("{name}")]
        [Route("[action]/{name}")]
        //public IActionResult Get(string name, bool exactMatch = false)
        public IEnumerable<CategoryVM> GetByName(string name)
        {
            bool exactMatch = false;
            var result = exactMatch
              ? _messages.Dispatch(new GetCategoryByExactNameQuery(name))
              : _messages.Dispatch(new GetCategoryByPartialNameQuery(name));

            //return Ok(result);
            var categoryVms = _mapper.Map<List<CategoryDto>, List<CategoryVM>>(result!);
            return categoryVms;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryCreateDto categoryCreateDto)
        {
            CategoryCreateCommand categoryCreateCommand = MapService.Map(categoryCreateDto);
            Result result = _messages.Dispatch(categoryCreateCommand);
            return FromResult(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            CategoryUpdateCommand categoryUpdateCommand = MapService.Map(categoryUpdateDto);
            Result result = _messages.Dispatch(categoryUpdateCommand);
            return FromResult(result);
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult MoveUp([FromBody] CategoryMoveUpDto categoryMoveUpDto)
        {
            CategoryMoveUpCommand categoryMoveUpCommand = MapService.Map(categoryMoveUpDto);
            Result result = _messages.Dispatch(categoryMoveUpCommand);
            return FromResult(result);
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult MoveToSibling([FromBody] CategoryMoveToSiblingDto categoryMoveToSiblingDto)
        {
            CategoryMoveToSiblingCommand categoryMoveToSiblingCommand = MapService.Map(categoryMoveToSiblingDto);
            Result result = _messages.Dispatch(categoryMoveToSiblingCommand);
            return FromResult(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] CategoryDeleteDto categoryDeleteDto)
        {
            CategoryDeleteCommand categoryDeleteCommand = MapService.Map(categoryDeleteDto);
            Result result = _messages.Dispatch(categoryDeleteCommand);
            return FromResult(result);
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateField([FromBody] CategoryFieldValuedUpdateDto categoryFieldUpdateDto)
        {
            CategoryFieldValueUpdateCommand categoryFieldUpdateCommand = MapService.Map(categoryFieldUpdateDto);
            Result result = _messages.Dispatch(categoryFieldUpdateCommand);
            return FromResult(result);
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult AddField([FromBody] FieldCreateCommand fieldCreateCommand)
        {
            //CategoryFieldValueUpdateCommand categoryFieldUpdateCommand = MapService.Map(categoryFieldUpdateDto);
            Result result = _messages.Dispatch(fieldCreateCommand);
            return FromResult(result);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ExportToJson()
        {
            var result = _messages.Dispatch(new ExportCategoriesToJsonQuery());
            return Ok(result);
        }
    }
}