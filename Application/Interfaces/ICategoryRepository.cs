using System.Collections;
using System.Collections.Generic;
using Common.Utils;
using Domain.Categories;

namespace Application.Interfaces
{
    public interface ICategoryRepository: IRepository<Category>
    {
        IList<Category> GetByPartialName(string categoryName);
        IList<Category> GetByExactName(string categoryName);
        IList<Category> GetAllParents();
        IList<Category> GetAllChilds(int categoryParentId);
    }
}