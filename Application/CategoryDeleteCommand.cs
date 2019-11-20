using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;

namespace Application
{
    public sealed class CategoryDeleteCommand : ICommand
    {
        public long Id { get; set; }

        public CategoryDeleteCommand(long id)
        {
            Id = id;
        }

        internal sealed class CategoryDeleteCommandHandler : ICommandHandler<CategoryDeleteCommand>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CategoryDeleteCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public Result Handle(CategoryDeleteCommand categoryDeleteCommand)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var category = MapService.Map(categoryDeleteCommand);
                categoryRepository.Delete(category);

                _unitOfWork.Commit();
                return Result.Ok();
            }
        }
    }
}
