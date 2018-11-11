using Domain.Categories;
using Domain.Items;

namespace Domain.ItemCategory
{
    public class ItemCategory//is a real IEntity?
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}