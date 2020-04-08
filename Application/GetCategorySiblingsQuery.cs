using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Domain;
using Dtos;

namespace Application
{
  public sealed class GetCategorySiblingsQuery : IQuery<List<CategoryDto>>
  {
    public GetCategorySiblingsQuery(int id)
    {
      Id = id;
    }

    public int Id { get; }

    internal sealed class GetCategorySiblingsHandler : IQueryHandler<GetCategorySiblingsQuery, List<CategoryDto>>
    {
      private readonly ICategoryQueryRepository _categoryQueryRepository;

      public GetCategorySiblingsHandler(ICategoryQueryRepository categoryQueryRepository)
      {
        _categoryQueryRepository = categoryQueryRepository;
      }

      public List<CategoryDto> Handle(GetCategorySiblingsQuery query)
      {
        // var categories = _categoryQueryRepository.GetSiblingsOf(query.Id);
        var category = _categoryQueryRepository.GetById(query.Id);
        var categories = category.ParentCategory.Id == 0
          ? _categoryQueryRepository.GetAllParents()
          : _categoryQueryRepository.GetAllChilds(category.ParentCategory.Id);

        var categoryDtos = MapService.Map(categories);

        return categoryDtos;
      }
    }
  }
}