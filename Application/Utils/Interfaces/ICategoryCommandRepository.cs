using System.Collections.Generic;
using Domain;

namespace Application.Utils.Interfaces
{
    public interface ICategoryCommandRepository
    {

        Category GetById(int id);

        void Create(Category category);

        void Update(Category category);

        void Delete(Category category);

        int CreateField(Field field);

        void LinkRecursive(int fieldId, int categoryRootId);

        void UpdateCategoryField(CategoryField entity);
    }
}