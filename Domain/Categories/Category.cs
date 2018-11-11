using System.Collections.Generic;
using Domain.Common;

namespace Domain.Categories
{
    public class Category: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }

        public ICollection<ItemCategory.ItemCategory> ItemCategories { get; set; }
    }
}