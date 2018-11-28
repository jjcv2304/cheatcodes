using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Categories;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Application.Categories.Queries
{
    public class GetCategories: IGetCategories
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategories(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        public List<CategoryVM> Execute()
        {
            var categories = _categoryRepository.GetList()
                .Select(c => new CategoryVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    Name = c.Name
                });
            return categories.ToList();
        }
    }
}