using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
    public sealed class GetCategorySiblings : IQuery<List<CategoryDto>>
    {
        public int Id { get; }

        public GetCategorySiblings(int Id)
        {
            Id = Id;
        }

        internal sealed class GetCategorySiblingsHandler : IQueryHandler<GetCategorySiblings, List<CategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategorySiblingsHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public List<CategoryDto> Handle(GetCategorySiblings query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var categories = categoryRepository.GetSiblingsOf(query.Id);
                var categoryDtos = MapService.Map(categories);

                return categoryDtos;
            }
        }
    }
}
