using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;

namespace Application
{
  public sealed class CategoryFieldValueUpdateCommand : ICommand
  {
    public CategoryFieldValueUpdateCommand(int fieldId, int categoryId, string value)
    {
      FieldId = fieldId;
      CategoryId = categoryId;
      Value = value;
    }

    public int FieldId { get; }
    public int CategoryId { get; }
    public string Value { get; }

    internal sealed class CategoryFieldValueUpdateCommandHandler : ICommandHandler<CategoryFieldValueUpdateCommand>
    {
      private readonly IUnitOfWork _unitOfWork;

      public CategoryFieldValueUpdateCommandHandler(IUnitOfWork unitOfWork)
      {
        _unitOfWork = unitOfWork;
      }

      public Result Handle(CategoryFieldValueUpdateCommand categoryFieldUpdateCommand)
      {
        var categoryRepository = _unitOfWork.CategoryCommandRepository;
        var categoryFieldValue = MapService.Map(categoryFieldUpdateCommand);
        categoryRepository.UpdateCategoryField(categoryFieldValue);

        _unitOfWork.Commit();
        return Result.Ok();
      }
    }
  }
}