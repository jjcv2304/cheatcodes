using System.Collections.Generic;
using Domain;

namespace Application.Utils.Interfaces
{
    public interface ICategoryRepository
    {
        IList<Category> GetByPartialName(string categoryName);

        IList<Category> GetByExactName(string categoryName);

        IList<Category> GetAllParents();

        IList<Category> GetAllChilds(int categoryParentId);

        IList<Category> GetSiblingsOf(int categoryChildId);

        Category GetById(int id);

        void Create(Category category);

        void Update(Category category);

        void Delete(Category category);
    }
}