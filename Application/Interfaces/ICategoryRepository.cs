using System.Collections.Generic;
using Domain.Categories;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        IReadOnlyList<Category> GetList();
    }
}