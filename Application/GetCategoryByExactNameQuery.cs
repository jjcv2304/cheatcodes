using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
    public sealed class GetCategoryByExactNameQuery : IQuery<List<CategoryDto>>
    {
        public string CategoryName { get; }

        public GetCategoryByExactNameQuery(string categoryName)
        {
            CategoryName = categoryName;
        }

        internal sealed class GetCategoryByExactNameHandler : IQueryHandler<GetCategoryByExactNameQuery, List<CategoryDto>>
        {
            private ICategoryQueryRepository _categoryQueryRepository;

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
