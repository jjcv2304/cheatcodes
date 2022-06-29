using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatCodesUI2022.Shared
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public bool HasParent { get; set; }
        public bool HasChild { get; set; }
      //  public List<CategoryFieldValueDto> CategoryFieldValues { get; set; }
    }
}
