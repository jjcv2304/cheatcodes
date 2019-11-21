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
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoryAllChildsHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategoryAllChildsQuery query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetAllChilds(query.Id);
                var categoriesDtos = MapService.Map(categories);

                return categoriesDtos;
            }
        }
    }
}
