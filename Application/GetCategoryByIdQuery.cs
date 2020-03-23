using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
  public sealed class GetCategoryByIdQuery : IQuery<CategoryDto>
  {
    public GetCategoryByIdQuery(int id)
    {
      Id = id;
    }

    public int Id { get; }

    internal sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
      private readonly ICategoryQueryRepository _categoryQueryRepository;

      public GetCategoryByIdQueryHandler(ICategoryQueryRepository categoryQueryRepository)
      {
        _categoryQueryRepository = categoryQueryRepository;
      }

      public CategoryDto Handle(GetCategoryByIdQuery query)
      {
        var category = _categoryQueryRepository.GetById(query.Id);
        var categoryDto = MapService.Map(category);

        return categoryDto;
      }
    }
  }
}