using System.Collections.Generic;
using System.Linq;
using Application.Categories.Tests;
using Domain.Categories;

namespace Application.Interfaces.Tests
{
    public class InMemoryCategoryRepository: RepositoryMock<Category>, ICategoryRepository
    {
        public IList<Category> GetByPartialName(string categoryName)
        {
            return Items
                .Where(cat => cat.Name.Contains(categoryName))
                .ToList();
        }

        public IList<Category> GetByExactName(string categoryName)
        {
            return Items
                .Where(cat => cat.Name == categoryName)
                .ToList();
        }
    }
}