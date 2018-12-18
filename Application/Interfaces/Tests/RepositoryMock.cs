using System;
using System.Collections.Generic;
using System.Linq;
using Common.Mocks;
using Common.Utils;

namespace Application.Interfaces.Tests
{
    public class RepositoryMock<T> : IRepository<T> where T : Entity
    {
        private int m_currentIdValue = 0;

        public RepositoryMock()
        {
            Items = new List<T>();
        }

        protected List<T> Items { get; set; }

        public T GetById(long id)
        {
            return (from item in Items
                where item.Id == id
                select item).FirstOrDefault();
        }

        public void Add(T saveThis)
        {
            if (saveThis == null)
            {
                throw new ArgumentNullException(
                    "saveThis", "Argument cannot be null.");
            }

            if (saveThis.Id == 0)
            {
                saveThis.Id = ++m_currentIdValue;
            }

            if (Items.Contains(saveThis) == false)
            {
                Items.Add(saveThis);
            }
        }

        public List<T> GetAll()
        {
            return Items;
        }

        public void Delete(T saveThis)
        {
            Items.Remove(saveThis);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}