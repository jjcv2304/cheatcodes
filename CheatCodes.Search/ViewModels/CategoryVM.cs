using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheatCodes.Search.ViewModels
{
  public class CategoryBasicVM
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }

  public class CategoryNameTreeVM
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ParentId { get; set; }
    public IList<CategoryNameTreeVM> Childs { get; set; }

  }
}
