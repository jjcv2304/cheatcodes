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
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoryByPartialNameQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategoryByPartialNameQuery query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetByPartialName(query.CategoryName);
                var categoriesDtos = MapService.Map(categories);

                return categoriesDtos;
            }
        }
    }
}
