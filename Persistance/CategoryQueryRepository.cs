using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Application.Utils;
using Application.Utils.Interfaces;
using Castle.Core.Logging;
using Dapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistance
{
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly QueriesConnectionString _connectionString;
        private readonly ILogger<CategoryQueryRepository> _logger;

        public CategoryQueryRepository(QueriesConnectionString connectionString, ILogger<CategoryQueryRepository> logger)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        public Category GetById(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString.Value))
            {
                return connection.Query<Category>(
                    "SELECT * FROM Category WHERE Id = @CategoryId",
                    param: new { CategoryId = id }

                ).FirstOrDefault();
            }
        }

        public IList<Category> GetByPartialName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetByExactName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetAllParents()
        {
            const string sql =
                @"SELECT cp.Id, cp.Name, cp.Description, cp.ParentId as endOfType, 
                         ch.Id , ch.Name, ch.Description, ch.ParentId as endOfType2,
                           null as Category, null as Field, cf.Value, '' as endOfType3,
                           f.Id, f.Name, f.Description
                FROM Category cp 
                left join Category ch on ch.ParentId=cp.Id
                left join CategoryField cf on cp.Id=cf.CategoryId
                left join Field F on cf.FieldId = F.Id
                where cp.ParentId is null";

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString.Value))
            {
                var res = connection.Query<Category, Category, CategoryField, Field, Category>(
                        sql,
                        (cp, ch, cf, f) =>
                        {
                            cp.ChildCategories = cp.ChildCategories ?? new List<Category>();
                            if (ch.Id > 0)
                            {
                                cp.ChildCategories.Add(ch);
                            }
                            cp.CategoryFields = cp.CategoryFields ?? new List<CategoryField>();
                            cf.Field = f;
                            cf.Category = cp;
                            cp.CategoryFields.Add(cf);

                            return cp;
                        },
                        splitOn: "endOfType, endOfType2, endOfType3"
                    ).GroupBy(cp => cp.Id)
                    .Select(group =>
                    {
                        var categoryParent = group.First();
                        if (categoryParent.ChildCategories.Any())
                        {
                            categoryParent.ChildCategories =
                                group.Select(cp => cp.ChildCategories.Single()).Distinct().ToList();
                        }
                        categoryParent.CategoryFields = group.Select(cp => cp.CategoryFields.Single()).Distinct().ToList();
                        return categoryParent;
                    }).ToList();

                return res;
            }

        }



        public IList<Category> GetAllChilds(int categoryParentId)
        {
            const string sql =
                @"SELECT c.Id, c.Name, c.Description, c.ParentId, c.ParentId as endOfType,
                         cp.Id, cp.Name, cp.Description, cp.ParentId as endOfType2,
                         ch.Id , ch.Name, ch.Description, ch.ParentId as endOfType3, 
                         null as Category, null as Field, cf.Value, '' as endOfType4,
                         f.Id, f.Name, f.Description
                FROM Category c
                left join Category cp on c.ParentId=cp.Id
                left join Category ch on ch.ParentId=c.Id
                left join CategoryField cf on c.Id=cf.CategoryId
                left join Field F on cf.FieldId = F.Id
                where c.ParentId = @categoryParentId";
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString.Value))
            {
                return connection.Query<Category, Category, Category, CategoryField, Field, Category>(
                        sql,
                        (c, cp, ch, cf, f) =>
                        {
                            c.ParentCategory = cp;
                            c.ChildCategories = c.ChildCategories ?? new List<Category>();
                            if (ch.Id > 0)
                            {
                                c.ChildCategories.Add(ch);
                            }

                            c.CategoryFields = c.CategoryFields ?? new List<CategoryField>();
                            cf.Field = f;
                            cf.Category = c;
                            c.CategoryFields.Add(cf);

                            return c;
                        },
                        param: new { categoryParentId = categoryParentId },
                        splitOn: "endOfType, endOfType2, endOfType3, endOfType4")
                    .GroupBy(c => c.Id)
                    .Select(group =>
                    {
                        var category = group.First();
                        if (category.ChildCategories.Any())
                            category.ChildCategories = group.Select(c => c.ChildCategories.Single()).ToList();
                        category.CategoryFields = group.Select(c => c.CategoryFields.Single()).Distinct().ToList();
                        return category;
                    }).ToList();
            }

            
        }

        public IList<Category> GetSiblingsOf(int categoryId)
        {
            const string sql =
                @"SELECT c.Id, c.Name, c.Description, c.ParentId, c.ParentId as endOfType,
                         cp.Id, cp.Name, cp.Description, cp.ParentId as endOfType2,
                         ch.Id , ch.Name, ch.Description, ch.ParentId  as endOfType3, 
                         null as Category, null as Field, cf.Value, '' as endOfType4,
                         f.Id, f.Name, f.Description
                    FROM Category cproot
                    inner join Category cgproot on (CASE
                                                     WHEN cproot.ParentId is null
                                                       THEN cproot.ParentId is null
                                                     Else cgproot.Id = cproot.ParentId
                                                    END)
                    inner join Category c on (CASE
                                                   WHEN cproot.ParentId is null
                                                     THEN c.ParentId is null
                                                   Else c.ParentId = cproot.ParentId
                                                   END)
                   left join Category cp on c.ParentId = cp.Id
                   left join Category ch on ch.ParentId = c.Id
                   left join CategoryField cf on c.Id=cf.CategoryId
                   left join Field F on cf.FieldId = F.Id
              where cproot.Id = @categoryId";

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString.Value))
            {
                return connection.Query<Category, Category, Category, CategoryField, Field, Category>(
                        sql,
                        (c, cp, ch, cf, f) =>
                        {
                            if (cp.Id > 0)
                            {
                                c.ParentCategory = cp;
                            }

                            c.ChildCategories = c.ChildCategories ?? new List<Category>();
                            if (ch.Id > 0)
                            {
                                c.ChildCategories.Add(ch);
                            }

                            c.CategoryFields = c.CategoryFields ?? new List<CategoryField>();
                            cf.Field = f;
                            cf.Category = c;
                            c.CategoryFields.Add(cf);

                            return c;
                        },
                        param: new { categoryId = categoryId },
                        splitOn: "endOfType, endOfType2, endOfType3, endOfType4")
                    .GroupBy(c => c.Id)
                    .Select(group =>
                    {
                        var category = group.First();
                        if (category.ChildCategories.Any())
                            category.ChildCategories = group.Select(c => c.ChildCategories.Single()).ToList();
                        category.CategoryFields = group.Select(c => c.CategoryFields.Single()).Distinct().ToList();
                        return category;
                    }).ToList();
            }

            
        }
    }
}