using System.Collections.Generic;
using Common.Utils;

namespace Domain
{
    public class Category: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public IList<CategoryField> CategoryFields { get; set; }

        public Category ParentCategory { get; set; }
        
        public IList<Category> ChildCategories { get; set; }

        public Category()
        {
            ChildCategories = new List<Category>();
            CategoryFields = new List<CategoryField>();
        }
    }
}
