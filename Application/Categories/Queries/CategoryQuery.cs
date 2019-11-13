using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Dtos;

namespace Application.Categories.Queries
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryQuery(IUnitOfWork unitOfWork)
        {
            _categoryRepository = unitOfWork.CategoryRepository;
        }
        
        public List<CategoryDto> ByExactName(string categoryName)
        {
            var category = _categoryRepository.GetByExactName(categoryName);

            var categoryDto = MapService.Map(category);

            return categoryDto;
        }
        
        public List<CategoryDto> ByPartialName(string categoryName)
        {
            var category = _categoryRepository.GetByPartialName(categoryName);

            var categoryDto = MapService.Map(category);

            return categoryDto;
        }
        
        public CategoryDto ById(long categoryId)
        {
            var category = _categoryRepository.Find(categoryId);

            var categoryDto = MapService.Map(category);

            return categoryDto;
        }

        public List<CategoryDto> All()
        {
            var categories = _categoryRepository.All();

            var categorieDtos = MapService.Map(categories);

            return categorieDtos;
        }
        
        public List<CategoryDto> AllParents()
        {
            var categories = _categoryRepository.GetAllParents();

            var categorieDtos = MapService.Map(categories);

            return categorieDtos;
        }

        public List<CategoryDto> GetAllChilds(int categoryParentId)
        {
            var categories = _categoryRepository.GetAllChilds(categoryParentId);

            var categorieDtos = MapService.Map(categories);

            return categorieDtos;
        }
        public List<CategoryDto> GetSiblingsOf(int categoryChildId)
        {
            var categories = _categoryRepository.GetSiblingsOf(categoryChildId);

            var categorieDtos = MapService.Map(categories);

            return categorieDtos;
        }
        
    }
}