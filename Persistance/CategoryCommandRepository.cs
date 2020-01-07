using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Utils.Interfaces;
using Dapper;
using Domain;

namespace Persistance
{
    public class CategoryCommandRepository : RepositoryBase, ICategoryCommandRepository
    {
        public CategoryCommandRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Category GetById(int id)
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

        public void Delete(Category entity)
        {
            Remove(entity.Id);
        }

        public void Create(Category entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(
                "INSERT INTO Category(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
                param: new { Name = entity.Name },
                transaction: Transaction
            );
        }

        public void Update(Category entity)
        {
            Connection.Execute(
                "UPDATE Category SET Name = @Name, Description = @Description WHERE Id = @CategoryId",
                param: new {Name = entity.Name, Description = entity.Description, CategoryId = entity.Id},
                transaction: Transaction
            );
            foreach (var categoryField in entity.CategoryFields)
            {
                UpdateCategoryField(categoryField);
            }
        }

        private void UpdateCategoryField(CategoryField entity)
        {
            Connection.Execute(
                "UPDATE CategoryField SET Value = @Value  WHERE CategoryId = @CategoryId AND FieldId = @FieldId",
                param: new {Value = entity.Value, CategoryId = entity.Category.Id, FieldId = entity.Field.Id},
                transaction: Transaction
            );
        }

    }
}