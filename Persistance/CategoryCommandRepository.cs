using System;
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

    // public Category GetById(int id)
    // {
    //     return Connection.Query<Category>(
    //         "SELECT * FROM Category WHERE Id = @CategoryId",
    //         param: new {CategoryId = id},
    //         transaction: Transaction
    //     ).FirstOrDefault();
    // }
    public Category GetById(int id)
    {
      const string sql =
        @"SELECT cp.Id, cp.Name, cp.Description, cp.ParentId as endOfType, 
                         ch.Id , ch.Name, ch.Description, ch.ParentId 
                FROM Category ch
                left join Category cp on ch.ParentId=cp.Id
                where ch.Id=@CategoryId";


      var res = Connection.Query<Category, Category, Category>(
        sql,
        (cp, ch) =>
        {
          ch.ParentCategory = cp;
          return ch;
        },
        new {CategoryId = id},
        Transaction,
        splitOn: "endOfType"
      ).Single();

      return res;
    }

    public void Delete(Category entity)
    {
      Remove(entity.Id);
    }

    public int Create(Category entity)
    {
      int? parentId;
      if (entity.ParentCategory.Id == 0) parentId = null;
      else parentId = entity.ParentCategory.Id;

      entity.Id = Connection.ExecuteScalar<int>(
        "INSERT INTO Category(Name, Description, ParentId) VALUES(@Name, @Description, @ParentId); SELECT last_insert_rowid()",
        new {entity.Name, entity.Description, ParentId = parentId},
        Transaction
      );
      return entity.Id;
    }

    public void Update(Category entity)
    {
      Connection.Execute(
        "UPDATE Category SET Name = @Name, Description = @Description WHERE Id = @CategoryId",
        new {entity.Name, entity.Description, CategoryId = entity.Id},
        Transaction
      );
      foreach (var categoryField in entity.CategoryFields) UpdateCategoryField(categoryField);
    }

    public void UpdateCategoryField(CategoryField entity)
    {
      Connection.Execute(
        "UPDATE CategoryField SET Value = @Value  WHERE CategoryId = @CategoryId AND FieldId = @FieldId",
        new {entity.Value, CategoryId = entity.Category.Id, FieldId = entity.Field.Id},
        Transaction
      );
    }

    public int CreateField(Field field)
    {
      var newFieldId = Connection.ExecuteScalar<int>(
        "INSERT INTO Field(Name, Description) VALUES(@Name, @Description); SELECT last_insert_rowid()",
        new {field.Name, field.Description},
        Transaction
      );
      return newFieldId;
    }

    public void LinkRecursive(int fieldId, int categoryRootId)
    {
      Connection.Execute(
        "WITH RECURSIVE cat_tree as (" +
        "SELECT id, name, ParentId     " +
        "FROM category    " +
        "WHERE id=@categoryRootId    " +
        "UNION ALL   " +
        "SELECT child.id, child.name, child.ParentId     " +
        "FROM category as child     " +
        "JOIN cat_tree as parent on parent.id = child.ParentId)" +
        "                                                                   " +
        "INSERT INTO CategoryField(CategoryId, FieldId, Value)" +
        "SELECT id, @newFieldId, '' FROM cat_tree;",
        new {categoryRootId, newFieldId = fieldId},
        Transaction
      );
    }

    public void LinkToFieldsFromSameLevel(int currentCategory, int parentCategoryId)
    {
      Connection.Execute(
        "INSERT INTO CategoryField(CategoryId, FieldId, Value)" +
        "SELECT DISTINCT @currentCategory, FieldId, '' " +
        "FROM CategoryField cf " +
        "INNER JOIN Category C ON COALESCE(cf.CategoryId,0) = COALESCE(C.Id,0) " +
        "WHERE COALESCE(c.ParentId,0)=COALESCE(@parentCategoryId,0)",
        new {currentCategory, parentCategoryId},
        Transaction
      );
    }

    public void LinkToCategoriesSameLevel(int newFieldId, int categoryId)
    {
      Connection.Execute(
        "INSERT INTO CategoryField(CategoryId, FieldId, Value)" +
        "SELECT cSiblings.id, @newFieldId, '' " +
        "  FROM Category c" +
        " INNER JOIN Category cSiblings on COALESCE(cSiblings.ParentId, 0) = COALESCE(c.ParentId, 0)" +
        " WHERE c.Id = @categoryId",
        new {newFieldId, categoryId},
        Transaction
      );
    }

    public void ChangeParent(int categoryId, int? newParentId)
    {
      Connection.Execute(
        "UPDATE Category SET ParentId=@ParentId WHERE Id = @CategoryId",
        new {ParentId = newParentId, CategoryId = categoryId},
        Transaction
      );
    }

    public void Remove(long id)
    {
      Connection.Execute(
        "DELETE FROM Category WHERE Id = @CategoryId",
        new {CategoryId = id},
        Transaction
      );
    }
  }
}