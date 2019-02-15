using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Interfaces;
using Dapper;
using Domain;
using Persistance.Utils;

namespace Persistance
{
    public class CategoryRepository : RepositoryBase, ICategoryRepository
    {
        public CategoryRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public IEnumerable<Category> All()
        {
            return Connection.Query<Category>(
                "SELECT * FROM Category",
                transaction: Transaction
            ).ToList();
        }

        public Category Find(long id)
        {
            return Connection.Query<Category>(
                "SELECT * FROM Category WHERE CategoryId = @CategoryId",
                param: new {CategoryId = id},
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Remove(long id)
        {
            Connection.Execute(
                "DELETE FROM Category WHERE Id = @CategoryId",
                param: new {CategoryId = id},
                transaction: Transaction
            );
        }

        public void Remove(Category entity)
        {
            Remove(entity.Id);
        }

        public void Add(Category entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(
                "INSERT INTO Category(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
                param: new {Name = entity.Name},
                transaction: Transaction
            );
        }

        public void Update(Category entity)
        {
            Connection.Execute(
                "UPDATE Category SET Name = @Name WHERE Id = @CategoryId",
                param: new {Name = entity.Name, CategoryId = entity.Id},
                transaction: Transaction
            );
        }

        public Category FindByName(string name)
        {
            return Connection.Query<Category>(
                "SELECT * FROM Category WHERE Name = @Name",
                param: new {Name = name},
                transaction: Transaction
            ).FirstOrDefault();
        }

        #region previous calls

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
                         ch.Id , ch.Name, ch.Description, ch.ParentId
                FROM Category cp 
                left join Category ch on ch.ParentId=cp.Id
                where cp.ParentId is null";

            var res = Connection.Query<Category, Category, Category>(
                sql,
                (cp, ch) =>
                {
                    cp.ChildCategories = cp.ChildCategories ?? new List<Category>();
                    cp.ChildCategories.Add(ch);
                    return cp;
                },
                splitOn:"endOfType",
                transaction: Transaction
            ).GroupBy(cp => cp.Id)
                .Select(group =>
                {
                    var categoryParent = group.First();
                    categoryParent.ChildCategories = group.Select(cp => cp.ChildCategories.Single()).ToList();
                    return categoryParent;
                }).ToList();

            return res;
        }

        public IList<Category> GetAllChilds(int categoryParentId)
        {
            const string sql =
                @"SELECT c.Id, c.Name, c.Description, c.ParentId, c.ParentId as endOfType,
                         cp.Id, cp.Name, cp.Description, cp.ParentId
                FROM Category c
                left join Category cp on c.ParentId=cp.Id
                where c.ParentId = @categoryParentId";
            
            return Connection.Query<Category, Category, Category>(
                sql,
                (c, cp) =>
                {
                    c.ParentCategory = cp;
                    return c;
                },
                param: new {categoryParentId = categoryParentId},
                splitOn:"endOfType",
                transaction: Transaction
            ).ToList();
        }

        public IList<Category> GetSiblingsOf(int categoryChildId)
        {
            const string sql =
                "SELECT cc.* " +
                "FROM Category cp " +
                "inner join Category cc on " +
                "(CASE " +
                "WHEN cp.ParentId is null " +
                "THEN cc.ParentId is null " +
                "Else cp.ParentId = cc.ParentId " +
                "END) " +
                "Where cp.CategoryID = @categoryId ";

            return Connection.Query<Category>(
                sql,
                param: new {categoryId = categoryChildId},
                transaction: Transaction
            ).ToList();
        }

        #endregion
    }
}