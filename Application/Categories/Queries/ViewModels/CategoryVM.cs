
using System.Collections.Generic;

namespace Application.Categories.Queries.ViewModels
{
    public class CategoryVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long? ParentId { get; set; }
        public bool HasParent { get; set; }
        public bool HasChild { get; set; }
        public List<CategoryFieldValueVM> CategoryFieldValues { get; set; }        
        
    }
}