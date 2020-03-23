using System;
using System.Data;
using System.Data.SQLite;
using Application.Utils.Interfaces;

namespace Persistance.Utils
{
  public class UnitOfWork : IUnitOfWork
  {
    private ICategoryCommandRepository _categoryCommandRepository;
    private IDbConnection _connection;
    private bool _disposed;
    private IDbTransaction _transaction;

    public UnitOfWork(DatabaseSetting databaseSetting)
    {
      _connection = new SQLiteConnection(databaseSetting.ConnectionString);
      _connection.Open();
      _transaction = _connection.BeginTransaction();
      var init = CategoryCommandRepository;
    }

    public ICategoryCommandRepository CategoryCommandRepository =>
      _categoryCommandRepository ?? (_categoryCommandRepository = new CategoryCommandRepository(_transaction));

    public void Commit()
    {
      try
      {
        _transaction.Commit();
      }
      catch
      {
        _transaction.Rollback();
        throw;
      }
      finally
      {
        _transaction.Dispose();
        // _transaction = _connection.BeginTransaction();
        resetRepositories();
      }
    }

    public void Dispose()
    {
      dispose(true);
      GC.SuppressFinalize(this);
    }

    private void resetRepositories()
    {
      _categoryCommandRepository = null;
    }

    private void dispose(bool disposing)
    {
      if (!_disposed)
      {
        if (disposing)
        {
          if (_transaction != null)
          {
            _transaction.Dispose();
            _transaction = null;
          }

          if (_connection != null)
          {
            _connection.Dispose();
            _connection = null;
          }
        }

        _disposed = true;
      }
    }

    ~UnitOfWork()
    {
      dispose(false);
    }
  }
}