using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;

namespace Application
{
    public sealed class CategoryUpdateCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryUpdateCommand(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        internal sealed class CategoryUpdateCommandHandler : ICommandHandler<CategoryUpdateCommand>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CategoryUpdateCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public Result Handle(CategoryUpdateCommand categoryUpdateCommand)
            {
                var categoryRepository = _unitOfWork.CategoryRepository;
                var category = MapService.Map(categoryUpdateCommand);
                categoryRepository.Update(category);

                _unitOfWork.Commit();
                return Result.Ok();
            }
        }
    }
}
