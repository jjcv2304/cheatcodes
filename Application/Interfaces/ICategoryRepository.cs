using System.Collections.Generic;
using Common.Utils;
using Domain.Categories;
using Persistance.Common;

namespace Application.Interfaces
{
    public interface ICategoryRepository: IRepository<Category>
    {
        IReadOnlyList<Category> GetList();
        
    }
}