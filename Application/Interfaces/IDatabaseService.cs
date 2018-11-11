using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IDatabaseService
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

    }
}