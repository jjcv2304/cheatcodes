using System.Data;

namespace Persistance
{
  public abstract class RepositoryBase
  {
    public RepositoryBase(IDbTransaction transaction)
    {
      Transaction = transaction;
    }

    public RepositoryBase()
    {
    }

    protected IDbTransaction Transaction { get; }
    protected IDbConnection Connection => Transaction.Connection;
  }
}