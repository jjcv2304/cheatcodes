namespace CheatCodes.Search.DB.Models
{
    public class CategoryField
    {
        //public virtual Category Category { get; set; }
        //public virtual Field Field { get; set; }

        public  int CategoryId { get; set; }
        public  int FieldId { get; set; }

    public virtual string Value { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CategoryField;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.CategoryId == other.CategoryId &&
                   this.FieldId == other.FieldId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                    hash = (hash * 31) ^ CategoryId.GetHashCode();
                    hash = (hash * 31) ^ FieldId.GetHashCode();

                return hash;
            }
        }
    }
}