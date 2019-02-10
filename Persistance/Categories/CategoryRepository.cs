using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Categories;
using NHibernate.Criterion;
using Persistance.Common;
using Persistance.Utils;

namespace Persistance.Categories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IList<Category> GetByPartialName(string categoryName)
        {
            return UnitOfWork
                .Query<Category>()
                .Where(cat => cat.Name.Contains(categoryName))
                .ToList();
        }

        public IList<Category> GetByExactName(string categoryName)
        {
            return UnitOfWork
                .Query<Category>()
                .Where(cat => cat.Name == categoryName)
                .ToList();
        }

        public IList<Category> GetAllParents()
        {
            return UnitOfWork.GetParentCategories();
        }

        public IList<Category> GetAllChilds(int categoryParentId)
        {
            return UnitOfWork.GetCategoriesByParent(categoryParentId);
        }
        public IList<Category> GetSiblingsOf(int categoryChildId)
        {
            return UnitOfWork.GetSiblingsOSf(categoryChildId);
        }
        
    }
}