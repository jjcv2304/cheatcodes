using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
  public sealed class GetCategoryAllParentsQuery : IQuery<List<CategoryDto>>
  {
    public sealed class GetCategoryAllParentsQueryHandler : IQueryHandler<GetCategoryAllParentsQuery, List<CategoryDto>>
    {
      private readonly ICategoryQueryRepository _categoryQueryRepository;

      public GetCategoryAllParentsQueryHandler(ICategoryQueryRepository categoryQueryRepository)
      {
        _categoryQueryRepository = categoryQueryRepository;
      }

      public List<CategoryDto> Handle(GetCategoryAllParentsQuery query)
      {
        var categories = _categoryQueryRepository.GetAllParents();
        var categoriesDtos = MapService.Map(categories);
        return categoriesDtos;
      }
    }
  }
}