using System.Collections.Generic;
using System.Linq;
using Domain;
using Dtos;

namespace Application.Utils
{
    public static class MapService
    {
        #region Category

        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
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

        public static List<CategoryDto> Map(IEnumerable<Category> categories)
        {
            return categories.Select(Map).ToList();
        }

        public static Category Map(CategoryDto categoryDto)
        {
            if (categoryDto == null) return null;
            return new Category()
            {
                Id = categoryDto.Id,
                Description = categoryDto.Description,
                Name = categoryDto.Name,
                CategoryFields = Map(categoryDto.CategoryFieldValues.ToList())
            };
        }

        public static List<Category> Map(IEnumerable<CategoryDto> categoriesVM)
        {
            return categoriesVM.Select(Map).ToList();
        }

        public static Category Map(CategoryCreateCommand categoryCreateCommand)
        {
            if (categoryCreateCommand == null) return null;
            return new Category()
            {
                Description = categoryCreateCommand.Description,
                Name = categoryCreateCommand.Name
            };
        }

        public static Category Map(CategoryUpdateCommand categoryUpdateCommand)
        {
            if (categoryUpdateCommand == null) return null;
            return new Category()
            {
                Id = categoryUpdateCommand.Id,
                Description = categoryUpdateCommand.Description,
                Name = categoryUpdateCommand.Name
            };
        }

        public static Category Map(CategoryDeleteCommand categoryDeleteCommand)
        {
            if (categoryDeleteCommand == null) return null;
            return new Category()
            {
                Id = categoryDeleteCommand.Id
            };
        }

        public static CategoryCreateCommand Map(CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null) return null;
            return new CategoryCreateCommand(
                categoryCreateDto.Description,
                categoryCreateDto.Name
            );
        }

        public static CategoryUpdateCommand Map(CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null) return null;
            return new CategoryUpdateCommand(
                categoryUpdateDto.Id,
                categoryUpdateDto.Description,
                categoryUpdateDto.Name
            );
        }
        
        public static CategoryDeleteCommand Map(CategoryDeleteDto categoryDeleteDto)
        {
            if (categoryDeleteDto == null) return null;
            return new CategoryDeleteCommand(
                categoryDeleteDto.Id
            );
        }


        #endregion

        #region Field

        public static FieldDto Map(Field field)
        {
            return new FieldDto()
            {
                Id = field.Id,
                Name = field.Name,
                Description = field.Description
            };
        }

        public static Field Map(FieldDto fieldDto)
        {
            return new Field()
            {
                Id = fieldDto.Id,
                Name = fieldDto.Name,
                Description = fieldDto.Description
            };
        }

        public static List<Field> Map(List<FieldDto> fieldsVm)
        {
            return fieldsVm.Select(Map).ToList();
        }

        public static List<FieldDto> Map(List<Field> fields)
        {
            return fields.Select(Map).ToList();
        }

        #endregion

        #region CategoryField

        public static CategoryFieldValueDto Map(CategoryField categoryField)
        {
            return new CategoryFieldValueDto()
            {
                CategoryId = categoryField.Category.Id,
                FieldId = categoryField.Field.Id,
                FieldName = categoryField.Field.Name,
                Value = categoryField.Value
            };
        }

        public static CategoryField Map(CategoryFieldValueDto categoryFieldValueDto)
        {
            return new CategoryField()
            {
                Category = new Category() {Id = categoryFieldValueDto.CategoryId},
                Field = new Field() {Id = categoryFieldValueDto.FieldId},
                Value = categoryFieldValueDto.Value
            };
        }

        public static List<CategoryFieldValueDto> Map(List<CategoryField> categoryFields)
        {
            return categoryFields.Select(Map).ToList();
        }

        public static List<CategoryField> Map(List<CategoryFieldValueDto> categoryFieldsDto)
        {
            return categoryFieldsDto.Select(Map).ToList();
        }

        #endregion
    }
}