using System.Collections.Generic;
using Application.Interfaces;
using Common.Utils;
using NUnit.Framework;
using Persistance.Utils;

namespace Persistance.Common
{
    public abstract class Repository<T>: IRepository<T> where T : Entity
    {
        protected readonly UnitOfWork UnitOfWork;

        protected Repository(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public List<T> GetAll()
        {
            return UnitOfWork.Get<T>();
        }
        
        public T GetById(long id)
        {
            return UnitOfWork.Get<T>(id);
        }

        public void Add(T entity)
        {
            UnitOfWork.SaveOrUpdate(entity);
        }
        
        public void Delete(T entity)
        {
            UnitOfWork.Delete(entity);
        }
        
        public void Update(T entity)
        {
            UnitOfWork.SaveOrUpdate(entity);
        }
    }
}
