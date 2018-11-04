using System.Collections.Generic;

namespace CheatCodes.WebApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }

        public ICollection<ItemCategory> ItemCategories { get; set; }
    }
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}