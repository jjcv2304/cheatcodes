using System.Collections.Generic;
using Common.Utils;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);
        IEnumerable<T> All();
        T Find(long id);
        void Remove(long id);
        void Remove(T entity);
        void Update(T entity);
    }
}