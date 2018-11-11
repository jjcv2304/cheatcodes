using Domain.Common;

namespace Domain.Items
{
    public class Item: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}