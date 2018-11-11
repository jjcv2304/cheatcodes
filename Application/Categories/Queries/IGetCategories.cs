using System.Collections.Generic;

namespace Application.Categories.Queries
{
    public interface IGetCategories
    {
            List<CategoryVM> Execute();
    }
}