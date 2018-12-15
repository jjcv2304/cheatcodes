using System.Collections.Generic;
using System.Linq;
using Common.Utils;

namespace Domain.Categories
{
    public class Category: Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        
        public virtual IList<Category> ChildCategories { get; set; }

        public Category()
        {
            ChildCategories = new List<Category>();
        }
    }
}
