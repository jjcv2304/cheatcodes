using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;
using Dtos;
using NHibernate.Mapping;

namespace Application
{
    public sealed class GetCategoryAllChilds : IQuery<List<CategoryDto>>
    {
        public int Id { get; }

        public GetCategoryAllChilds(int id)
        {
            Id = id;
        }

        internal sealed class GetCategoryAllChildsHandler : IQueryHandler<GetCategoryAllChilds, List<CategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoryAllChildsHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategoryAllChilds query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetAllChilds(query.Id);
                var categoriesDtos = MapService.Map(categories);

                return categoriesDtos;
            }
        }
    }
}
