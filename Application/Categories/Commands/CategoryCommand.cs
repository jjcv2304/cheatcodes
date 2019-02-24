using Application.Categories.Queries.ViewModels;
using Application.Interfaces;

namespace Application.Categories.Commands
{
    public class CategoryCommand : ICategoryCommand
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommand(IUnitOfWork unitOfWork)
        {
            _categoryRepository = unitOfWork.CategoryRepository;
        }
        
        public void Add(CategoryVM categoryVM)
        {
            var category = MapService.Map(categoryVM);
            _categoryRepository.Add(category);
        }
        
        public void Update(CategoryVM categoryVM)
        {
            var category = MapService.Map(categoryVM);
            _categoryRepository.Update(category);
        }
        
        public void Delete(CategoryVM categoryVM)
        {
            var category = MapService.Map(categoryVM);
           _categoryRepository.Remove(category);
        }
    }
}