using Application.Interfaces;
using Dtos;

namespace Application.Categories.Commands
{
    public class CategoryCommand : ICategoryCommand
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommand(IUnitOfWork unitOfWork)
        {
            _categoryRepository = unitOfWork.CategoryRepository;
        }
        public void Add(CategoryDto categoryDto)
        {
            var category = MapService.Map(categoryDto);
            _categoryRepository.Add(category);
        }
        
        public void Update(CategoryDto categoryDto)
        {
            var category = MapService.Map(categoryDto);
            _categoryRepository.Update(category);
        }
        
        public void Delete(CategoryDto categoryDto)
        {
            var category = MapService.Map(categoryDto);
           _categoryRepository.Remove(category);
        }
    }
}