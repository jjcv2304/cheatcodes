using System.Collections.Generic;
using Common.Utils;

namespace Domain
{
  public class Category : Entity
  {
    public Category()
    {
      ChildCategories = new List<Category>();
      CategoryFields = new List<CategoryField>();
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public IList<CategoryField> CategoryFields { get; set; }

    public Category ParentCategory { get; set; }

    //TOdo to be considered
    //[ForeignKey(nameof(ParentCategory))]
    //public int? ParentCategoryId { get; set; }

    public IList<Category> ChildCategories { get; set; }

  }
}