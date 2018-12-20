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
        public List<CategoryVM> ChildCategories { get; set; }

        #endregion

        #region transformMethods

        public static CategoryVM TransformToVM(Category category)
        {
            return new CategoryVM()
            {
                Id = category.Id,
                Description = category.Description,
                Name = category.Name,
                ChildCategories = TransformToVM(category.ChildCategories.ToList())
            };
        }

        public static List<CategoryVM> TransformToVM(IEnumerable<Category> categories)
        {
            return categories.Select(TransformToVM).ToList();
        }

        public static Category TransformFromVM(CategoryVM categoryVM)
        {
            return new Category()
            {
                Id = categoryVM.Id,
                Description = categoryVM.Description,
                Name = categoryVM.Name,
                ChildCategories = TransformFromVM(categoryVM.ChildCategories.ToList())
            };
        }

        public static List<Category> TransformFromVM(IEnumerable<CategoryVM> categoriesVM)
        {
            return categoriesVM.Select(TransformFromVM).ToList();
        }

        #endregion
    }
}