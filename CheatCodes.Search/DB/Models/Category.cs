using System.Collections.Generic;

namespace CheatCodes.Search.DB.Models
{
  public class Category
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    

    public ICollection<CategoryField> CategoryFields { get; set; }

    public int? ParentId { get; set; }
    public virtual Category ParentCategory { get; set; }

    public IList<Category> ChildCategories { get; set; }

    public Category()
    {
      ChildCategories = new List<Category>();
      CategoryFields = new List<CategoryField>();
    }
  }
}