using System.Collections.Generic;
using Application.Categories.ViewModels;
using Domain.Categories;
using NUnit.Framework;

namespace Application.Categories.Queries
{
    public interface ICategoryQuery
    {
        CategoryVM ById(long categoryId);
        List<CategoryVM> ByPartialName(string categoryName);
        List<CategoryVM> ByExactName(string categoryName);
        List<CategoryVM> All();
        List<CategoryVM> AllParents();
        List<CategoryVM> GetAllChilds(int categoryParentId);
    }
}