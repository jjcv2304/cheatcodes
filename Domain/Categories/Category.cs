using System.Collections.Generic;
using Common.Utils;

namespace Domain.Categories
{
    public class Category: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }

    }
}