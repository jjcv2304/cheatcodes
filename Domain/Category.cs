using System.Collections.Generic;
using Common.Utils;

namespace Domain
{
    public class Category: Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        
        public virtual IList<CategoryField> CategoryFields { get; set; }

        public virtual Category ParentCategory { get; set; }
        
        public virtual IList<Category> ChildCategories { get; set; }

        public Category()
        {
            ChildCategories = new List<Category>();
            CategoryFields = new List<CategoryField>();
        }
    }
}
