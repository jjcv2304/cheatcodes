using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Utils;
using Application.Utils.Interfaces;
using Domain;
using Dtos;

namespace Application
{
  public class ExportCategoriesToJsonQuery : IQuery<List<CategoryTreeDto>>
  {
    public ExportCategoriesToJsonQuery()
    {
    }
    public sealed class ExportCategoriesToJsonQueryHandler : IQueryHandler<ExportCategoriesToJsonQuery, List<CategoryTreeDto>>
    {
      private readonly ICategoryQueryRepository _categoryQueryRepository;

      public ExportCategoriesToJsonQueryHandler(ICategoryQueryRepository categoryQueryRepository)
      {
        _categoryQueryRepository = categoryQueryRepository;
      }

      public List<CategoryTreeDto> Handle(ExportCategoriesToJsonQuery query)
      {
        var categories = _categoryQueryRepository.ExportToJson();
        return categories.ToList();
        //var categoryTreeDtos = MapService.MapToTreeDto(categories);
        //return categoryTreeDtos.ToList();
      }
    }
  }
}
