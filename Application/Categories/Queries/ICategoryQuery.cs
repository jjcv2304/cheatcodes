using System.Collections.Generic;
using Dtos;
using NUnit.Framework;

namespace Application.Categories.Queries
{
    public interface ICategoryQuery
    {
        CategoryDto ById(long categoryId);
        List<CategoryDto> ByPartialName(string categoryName);
        List<CategoryDto> ByExactName(string categoryName);
        List<CategoryDto> All();
        List<CategoryDto> AllParents();
        List<CategoryDto> GetAllChilds(int categoryParentId);
        List<CategoryDto> GetSiblingsOf(int categoryChildId);
    }
}