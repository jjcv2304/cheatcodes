using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheatCodes.Search.ViewModels
{
  public class CategoryNameTreeVM
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }
    public CategoryNameTreeVM Child { get; set; }
  }
}
