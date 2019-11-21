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
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoryByExactNameHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategoryByExactNameQuery query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetByExactName(query.CategoryName);
                var categoriesDtos = MapService.Map(categories);

                return categoriesDtos;
            }
        }
    }
}
