using Domain;

namespace Application.Utils.Interfaces
{
  public interface ICategoryCommandRepository
  {
    Category GetById(int id);

    int Create(Category category);

    void Update(Category category);

    void Delete(Category category);

    int CreateField(Field field);

    void LinkRecursive(int fieldId, int categoryRootId);

    void UpdateCategoryField(CategoryField entity);

    void LinkToFieldsFromSameLevel(int currentCategory, int parentCategoryId);

    void LinkToCategoriesSameLevel(int newFieldId, int rootCategoryId);

    void ChangeParent(int categoryId, int? newParentId);
  }
}