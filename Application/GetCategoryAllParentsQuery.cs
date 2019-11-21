using System.Collections.Generic;
using Application.Decorators;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{

    public sealed class GetCategoryAllParentsQuery : IQuery<List<CategoryDto>>
    {
        public GetCategoryAllParentsQuery()
        {
        }

        public sealed class GetCategoryAllParentsQueryHandler : IQueryHandler<GetCategoryAllParentsQuery, List<CategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoryAllParentsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategoryAllParentsQuery query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetAllParents();
                var categoriesDtos = MapService.Map(categories);
                _unitOfWork.Commit();
                return categoriesDtos;
            }
        }
    }
}
