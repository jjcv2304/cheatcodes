using System.Collections.Generic;
using Common.Utils;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        List<T> GetAll();
        T GetById(long id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}