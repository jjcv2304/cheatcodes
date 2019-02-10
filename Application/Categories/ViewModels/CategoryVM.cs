using System.Collections.Generic;
using System.Linq;
using Domain.Categories;

namespace Application.Categories.ViewModels
{
    public class CategoryVM
    {
        #region properties

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public long? ParentId { get; set; }
        public bool HasParent { get; set; }
        public bool HasChild { get; set; }

        #endregion

        #region transformMethods

        public static CategoryVM TransformToVM(Category category)
        {
            return new CategoryVM()
            {
                Id = category.Id,
                Description = category.Description,
                Name = category.Name,
                HasParent = category.ParentCategory != null,
                ParentId = category.ParentCategory?.Id,
                HasChild = category.ChildCategories.Any()
            };
        }

        public static List<CategoryVM> TransformToVM(IEnumerable<Category> categories)
        {
            return categories.Select(TransformToVM).ToList();
        }

        public static Category TransformFromVM(CategoryVM categoryVM)
        {
            if (categoryVM == null) return null;
            return new Category()
            {
                Id = categoryVM.Id,
                Description = categoryVM.Description,
                Name = categoryVM.Name,
            };
        }

        public static List<Category> TransformFromVM(IEnumerable<CategoryVM> categoriesVM)
        {
            return categoriesVM.Select(TransformFromVM).ToList();
        }

        #endregion
    }
}