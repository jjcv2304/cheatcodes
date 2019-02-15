using Common.Utils;

namespace Domain
{
    public class Field: Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}