using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;

namespace Application
{
    public sealed class CategoryMoveUpCommand : ICommand
    {
        public int Id { get; }
        public int ParentId { get;  }
        
        public CategoryMoveUpCommand(int id, int parentId)
        {
            ParentId = parentId;
            Id = id;
        }

        internal sealed class CategoryMoveUpCommandHandler : ICommandHandler<CategoryMoveUpCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            public CategoryMoveUpCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public Result Handle(CategoryMoveUpCommand categoryMoveUpCommand)
            {
                var categoryRepository = _unitOfWork.CategoryCommandRepository;
                var categoryParent = categoryRepository.GetById(categoryMoveUpCommand.ParentId);
                int? newParentId = null;
                if (categoryParent.ParentCategory.Id != 0) newParentId = categoryParent.ParentCategory.Id;
                categoryRepository.ChangeParent(categoryMoveUpCommand.Id, newParentId);

                _unitOfWork.Commit();
                return Result.Ok();
            }
        }
    }
}
;