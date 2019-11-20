
using System.Collections.Generic;

namespace Dtos
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long? ParentId { get; set; }
        public bool HasParent { get; set; }
        public bool HasChild { get; set; }
        public List<CategoryFieldValueDto> CategoryFieldValues { get; set; }        
        
    }

    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CategoryDeleteDto
    {
        public long Id { get; set; }
    }
}