using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
  public sealed class GetCategoryByExactNameQuery : IQuery<List<CategoryDto>>
  {
    public GetCategoryByExactNameQuery(string categoryName)
    {
      CategoryName = categoryName;
    }

    public string CategoryName { get; }

    internal sealed class GetCategoryByExactNameHandler : IQueryHandler<GetCategoryByExactNameQuery, List<CategoryDto>>
    {
      private readonly ICategoryQueryRepository _categoryQueryRepository;

      public GetCategoryByExactNameHandler(ICategoryQueryRepository categoryQueryRepository)
      {
        _categoryQueryRepository = categoryQueryRepository;
      }

      public List<CategoryDto> Handle(GetCategoryByExactNameQuery query)
      {
        var categories = _categoryQueryRepository.GetByExactName(query.CategoryName);
        var categoriesDtos = MapService.Map(categories);

        return categoriesDtos;
      }
    }
  }
}