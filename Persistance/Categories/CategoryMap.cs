using Domain.Categories;
using Domain.Items;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Persistance.Categories
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Description).Nullable();
            
            HasManyToMany(x => x.ChildCategories)
                .ParentKeyColumn("ParentId")
                .ChildKeyColumn("ChildId")
                .Table("CategoryLinks")
                .Cascade.SaveUpdate();
         
        }
    }
}