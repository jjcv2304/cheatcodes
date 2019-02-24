using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application.Categories.Queries.ViewModels
{
    public static class MapService
    {
        #region Category

        public static CategoryVM Map(Category category)
        {
            return new CategoryVM()
            {
                Id = category.Id,
                Description = category.Description,
                Name = category.Name,
                HasParent = category.ParentCategory != null,
                ParentId = category.ParentCategory?.Id,
                HasChild = category.ChildCategories.Any(),
                CategoryFieldValues = Map(category.CategoryFields.ToList())
            };
        }

        public static List<CategoryVM> Map(IEnumerable<Category> categories)
        {
            return categories.Select(Map).ToList();
        }

        public static Category Map(CategoryVM categoryVM)
        {
            if (categoryVM == null) return null;
            return new Category()
            {
                Id = categoryVM.Id,
                Description = categoryVM.Description,
                Name = categoryVM.Name,
            };
        }

        public static List<Category> Map(IEnumerable<CategoryVM> categoriesVM)
        {
            return categoriesVM.Select(Map).ToList();
        }

        #endregion

        #region Field

        public static FieldVM Map(Field field)
        {
            return new FieldVM()
            {
                Id = field.Id,
                Name = field.Name,
                Description = field.Description
            };
        }

        public static Field Map(FieldVM fieldVM)
        {
            return new Field()
            {
                Id = fieldVM.Id,
                Name = fieldVM.Name,
                Description = fieldVM.Description
            };
        }

        public static List<Field> Map(List<FieldVM> fieldsVM)
        {
            return fieldsVM.Select(Map).ToList();
        }

        public static List<FieldVM> Map(List<Field> fields)
        {
            return fields.Select(Map).ToList();
        }

        #endregion

        #region CategoryField

        public static CategoryFieldValueVM Map(CategoryField categoryField)
        {
            return new CategoryFieldValueVM()
            {
                CategoryId = categoryField.Category.Id,
                FieldId = categoryField.Field.Id,
                FieldName = categoryField.Field.Name,
                Value = categoryField.Value
            };
        }

        public static CategoryField Map(CategoryFieldValueVM CategoryFieldValueVM)
        {
            return new CategoryField()
            {
                Category = new Category() {Id = CategoryFieldValueVM.CategoryId},
                Field = new Field() {Id = CategoryFieldValueVM.FieldId},
                Value = CategoryFieldValueVM.Value
            };
        }

        public static List<CategoryFieldValueVM> Map(List<CategoryField> categoryFields)
        {
            return categoryFields.Select(Map).ToList();
        }

        public static List<CategoryField> Map(List<CategoryFieldValueVM> categoryFieldsVM)
        {
            return categoryFieldsVM.Select(Map).ToList();
        }

        #endregion
    }
}