using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Categories;
using Persistance.Common;
using Persistance.Utils;

namespace Persistance.Categories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
        public IReadOnlyList<Category> GetList()
        {
            return _unitOfWork.Query<Category>().ToList();
        }
    }
}