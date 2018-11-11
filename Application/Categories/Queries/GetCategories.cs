using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Categories;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Application.Categories.Queries
{
    public class GetCategories: IGetCategories
    {
        private readonly IDatabaseService _databaseService;

        public GetCategories(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public List<CategoryVM> Execute()
        {
            var categories = _databaseService.Set<Category>()
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