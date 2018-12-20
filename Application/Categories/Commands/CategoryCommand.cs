using Application.Categories.ViewModels;
using Application.Interfaces;
using Domain.Categories;

namespace Application.Categories.Commands
{
    public class CategoryCommand : ICategoryCommand
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommand(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        public void Add(CategoryVM categoryVM)
        {
            var category = CategoryVM.TransformFromVM(categoryVM);
            _categoryRepository.Add(category);
        }
        
        public void Update(CategoryVM categoryVM)
        {
            var category = CategoryVM.TransformFromVM(categoryVM);
            _categoryRepository.Update(category);
        }
        
        public void Delete(CategoryVM categoryVM)
        {
            var category = CategoryVM.TransformFromVM(categoryVM);
            _categoryRepository.Delete(category);
        }
    }
}