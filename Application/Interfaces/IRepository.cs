using Common.Utils;

namespace Persistance.Common
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(long id);
        void Add(T entity);
    }
}