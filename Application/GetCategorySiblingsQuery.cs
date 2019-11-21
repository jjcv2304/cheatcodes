using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
    public sealed class GetCategorySiblingsQuery : IQuery<List<CategoryDto>>
    {
        public int Id { get; }

        public GetCategorySiblingsQuery(int Id)
        {
            Id = Id;
        }

        internal sealed class GetCategorySiblingsHandler : IQueryHandler<GetCategorySiblingsQuery, List<CategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategorySiblingsHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategorySiblingsQuery query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetSiblingsOf(query.Id);
                var categoryDtos = MapService.Map(categories);

                return categoryDtos;
            }
        }
    }
}
