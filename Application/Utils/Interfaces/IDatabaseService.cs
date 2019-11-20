using Microsoft.EntityFrameworkCore;

namespace Application.Utils.Interfaces
{
    public interface IDatabaseService
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

    }
}