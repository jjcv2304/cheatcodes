using Application.Decorators;
using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;

namespace Application
{
  public sealed class FieldCreateCommand : ICommand
  {
    public FieldCreateCommand(string name, string description, int categoryId)
    {
      Name = name;
      Description = description;
      CategoryId = categoryId;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; }

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
        var newFieldId = categoryRepository.CreateField(field);
        categoryRepository.LinkToCategoriesSameLevel(newFieldId, fieldCreateCommand.CategoryId);

        _unitOfWork.Commit();
        return Result.Ok();
      }
    }
  }
}