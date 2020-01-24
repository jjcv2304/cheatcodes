using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
    public sealed class GetCategoryAllChildsQuery : IQuery<List<CategoryDto>>
    {
        public int Id { get; }
        public GetCategoryAllChildsQuery(int id)
        {
            Id = id;
        }
        internal sealed class GetCategoryAllChildsHandler : IQueryHandler<GetCategoryAllChildsQuery, List<CategoryDto>>
        {
            private ICategoryQueryRepository _categoryQueryRepository;
            public GetCategoryAllChildsHandler(ICategoryQueryRepository categoryQueryRepository)
            {
                _categoryQueryRepository = categoryQueryRepository;
            }
            public List<CategoryDto> Handle(GetCategoryAllChildsQuery query)
            {
                var categories = _categoryQueryRepository.GetAllChilds(query.Id);
                var categoriesDtos = MapService.Map(categories);

                return categoriesDtos;
            }
        }
    }
}
