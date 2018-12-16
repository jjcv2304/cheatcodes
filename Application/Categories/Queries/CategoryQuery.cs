using System.Collections.Generic;
using System.Linq;
using Application.Categories.ViewModels;
using Application.Interfaces;
using Domain.Categories;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using NUnit.Framework;

namespace Application.Categories.Queries
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryQuery(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        public List<CategoryVM> ByExactName(string categoryName)
        {
            var category = _categoryRepository.GetByExactName(categoryName);

            var categoryVM = CategoryVM.TransformToVM(category);

            return categoryVM;
        }
        
        public List<CategoryVM> ByPartialName(string categoryName)
        {
            var category = _categoryRepository.GetByPartialName(categoryName);

            var categoryVM = CategoryVM.TransformToVM(category);

            return categoryVM;
        }
        
        public CategoryVM ById(long categoryId)
        {
            var category = _categoryRepository.GetById(categoryId);

            var categoryVM = CategoryVM.TransformToVM(category);

            return categoryVM;
        }

        public List<CategoryVM> All()
        {
            var categories = _categoryRepository.GetAll();

            var categoriesVM = CategoryVM.TransformToVM(categories);

            return categoriesVM;
        }
    }
}