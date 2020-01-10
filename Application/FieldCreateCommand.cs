using Application.Decorators;
using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;
using Domain;

namespace Application
{
    public sealed class FieldCreateCommand: ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RootCategoryId { get; }

        public FieldCreateCommand(string name, string description, int rootCategoryId)
        {
            Name = name;
            Description = description;
            RootCategoryId = rootCategoryId;
        }

        [AuditLog]
        internal sealed class FieldCreateCommandHandler : ICommandHandler<FieldCreateCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            public FieldCreateCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public Result Handle(FieldCreateCommand fieldCreateCommand)
            {
                var categoryRepository = _unitOfWork.CategoryCommandRepository;
                var field = MapService.Map(fieldCreateCommand);
                var fieldId = categoryRepository.CreateField(field);
                categoryRepository.LinkRecursive(fieldId, fieldCreateCommand.RootCategoryId);

                _unitOfWork.Commit();
                return Result.Ok();
            }
        }
    }
}

