using System;
using System.Collections.Generic;
using System.Text;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
    public sealed class GetCategoryByExactName : IQuery<List<CategoryDto>>
    {
        public string CategoryName { get; }

        public GetCategoryByExactName(string categoryName)
        {
            CategoryName = categoryName;
        }

        internal sealed class GetCategoryByExactNameHandler : IQueryHandler<GetCategoryByExactName, List<CategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoryByExactNameHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategoryByExactName query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetByExactName(query.CategoryName);
                var categoriesDtos = MapService.Map(categories);

                return categoriesDtos;
            }
        }
    }
}
