using System;

namespace Domain
{
    public class CategoryField
    {
        public virtual Category Category { get; set; }
        public virtual Field Field { get; set; }
        public virtual string Value { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CategoryField;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Category == other.Category &&
                   this.Field == other.Field;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                if (Category != null)
                    hash = (hash * 31) ^ Category.GetHashCode();
                if (Field != null)
                    hash = (hash * 31) ^ Field.GetHashCode();

                return hash;
            }
        }
    }
}