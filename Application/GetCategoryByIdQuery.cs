using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
    public sealed class GetCategoryByIdQuery : IQuery<CategoryDto>
    {
        public int Id { get; }

        public GetCategoryByIdQuery(int Id)
        {
            Id = Id;
        }

        internal sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public CategoryDto Handle(GetCategoryByIdQuery query)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var category = categoryRepository.GetById(query.Id);
                var categoryDto = MapService.Map(category);

                return categoryDto;
            }
        }
    }
}
