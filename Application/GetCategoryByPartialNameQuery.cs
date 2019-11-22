using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
    public sealed class GetCategoryByPartialNameQuery : IQuery<List<CategoryDto>>
    {
        public string CategoryName { get; }

        public GetCategoryByPartialNameQuery(string categoryName)
        {
            CategoryName = categoryName;
        }

        internal sealed class GetCategoryByPartialNameQueryHandler : IQueryHandler<GetCategoryByPartialNameQuery, List<CategoryDto>>
        {
            private ICategoryQueryRepository _categoryQueryRepository;

            public GetCategoryByPartialNameQueryHandler(ICategoryQueryRepository categoryQueryRepository)
            {
                _categoryQueryRepository = categoryQueryRepository;
            }

            public List<CategoryDto> Handle(GetCategoryByPartialNameQuery query)
            {
                var categories = _categoryQueryRepository.GetByPartialName(query.CategoryName);
                var categoriesDtos = MapService.Map(categories);

                return categoriesDtos;
            }
        }
    }
}
