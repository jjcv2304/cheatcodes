using CheatCodes.Search.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CheatCodes.Search.DB
{
  public class CheatCodesDbContext : DbContext
  {
    public CheatCodesDbContext(DbContextOptions<CheatCodesDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    //public DbSet<Field> Fields { get; set; }
    //public DbSet<CategoryField> CategoryFields { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Category>().ToTable("Category");
      modelBuilder.Entity<Category>().HasKey(c => new {c.Id});
      modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.ChildCategories)
        .HasForeignKey(x => x.ParentId);

      modelBuilder.Entity<Field>().ToTable("Field");
      modelBuilder.Entity<Field>().HasKey(c => new {c.Id});

      modelBuilder.Entity<CategoryField>().ToTable("CategoryField");
      modelBuilder.Entity<CategoryField>().HasKey(cf => new {cf.CategoryId, cf.FieldId});
    }
  }
}