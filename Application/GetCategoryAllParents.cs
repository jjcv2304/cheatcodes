using System;
using System.Collections.Generic;
using System.Text;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{

    public sealed class GetCategoryAllParents : IQuery<List<CategoryDto>>
    {
        public GetCategoryAllParents()
        {
        }

        internal sealed class GetCategoryAllParentsHandler : IQueryHandler<GetCategoryAllParents, List<CategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoryAllParentsHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategoryAllParents query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetAllParents();
                var categoriesDtos = MapService.Map(categories);

                return categoriesDtos;
            }
        }
    }
}
