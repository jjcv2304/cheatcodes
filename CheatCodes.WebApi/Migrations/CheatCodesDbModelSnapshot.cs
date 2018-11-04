﻿// <auto-generated />
using System;
using CheatCodes.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CheatCodes.WebApi.Migrations
{
    [DbContext(typeof(CheatCodesDb))]
    partial class CheatCodesDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CheatCodes.WebApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CheatCodes.WebApi.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("CheatCodes.WebApi.Models.ItemCategory", b =>
                {
                    b.Property<int>("ItemId");

                    b.Property<int>("CategoryId");

                    b.HasKey("ItemId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ItemCategory");
                });

            modelBuilder.Entity("CheatCodes.WebApi.Models.Category", b =>
                {
                    b.HasOne("CheatCodes.WebApi.Models.Category", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("CheatCodes.WebApi.Models.ItemCategory", b =>
                {
                    b.HasOne("CheatCodes.WebApi.Models.Category", "Category")
                        .WithMany("ItemCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CheatCodes.WebApi.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
