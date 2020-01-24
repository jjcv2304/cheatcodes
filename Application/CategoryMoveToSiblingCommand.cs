using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;

namespace Application
{
    public sealed class CategoryMoveToSiblingCommand : ICommand
    {
        public int Id { get; }
        public int SiblingId { get;  }
        
        public CategoryMoveToSiblingCommand(int id, int siblingId)
        {
            SiblingId = siblingId;
            Id = id;
        }

        internal sealed class CategoryMoveToSiblingCommandHandler : ICommandHandler<CategoryMoveToSiblingCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            public CategoryMoveToSiblingCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public Result Handle(CategoryMoveToSiblingCommand categoryMoveToSiblingCommand)
            {
                var categoryRepository = _unitOfWork.CategoryCommandRepository;
                categoryRepository.ChangeParent(categoryMoveToSiblingCommand.Id, categoryMoveToSiblingCommand.SiblingId);

                _unitOfWork.Commit();
                return Result.Ok();
            }
        }
    }
}
;