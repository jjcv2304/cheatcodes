using Microsoft.EntityFrameworkCore;

namespace CheatCodes.WebApi.Models
{
    public class CheatCodesDb: DbContext
    {

        public CheatCodesDb(DbContextOptions<CheatCodesDb> options)
            : base(options)
        {
 
            
        }
        
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemCategory>()
                .HasKey(ic => new { ic.ItemId, ic.CategoryId });

//            modelBuilder.Entity<ItemCategory>()
//                .HasOne(ic => ic.Item)
//                .WithMany(b => b.ItemCategories)
//                .HasForeignKey(ic => ic.ItemId);

            modelBuilder.Entity<ItemCategory>()
                .HasOne(ic => ic.Category)
                .WithMany(c => c.ItemCategories)
                .HasForeignKey(ic => ic.CategoryId);

        }
    }
}