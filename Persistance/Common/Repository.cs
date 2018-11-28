using Common.Utils;
using Persistance.Utils;

namespace Persistance.Common
{
    public abstract class Repository<T>
        where T : Entity
    {
        protected readonly UnitOfWork _unitOfWork;

        protected Repository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T GetById(long id)
        {
            return _unitOfWork.Get<T>(id);
        }

        public void Add(T entity)
        {
            _unitOfWork.SaveOrUpdate(entity);
        }
    }
}
