using Application.Decorators;
using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;

namespace Application
{
    public sealed class CategoryCreateCommand: ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentId { get; }

        public CategoryCreateCommand(string name, string description, int parentId)
        {
            Name = name;
            Description = description;
            ParentId = parentId;
        }

        [AuditLog]
        internal sealed class CategoryCreateCommandHandler: ICommandHandler<CategoryCreateCommand>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CategoryCreateCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public Result Handle(CategoryCreateCommand categoryCreateCommand)
            {
                var categoryRepository = _unitOfWork.CategoryCommandRepository;
                var category = MapService.Map(categoryCreateCommand);
                categoryRepository.Create(category);

                _unitOfWork.Commit();
                return Result.Ok();
            }
        }
    }
}
