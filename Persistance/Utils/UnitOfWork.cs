using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Categories;
using NHibernate;
using NHibernate.Criterion;

namespace Persistance.Utils
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;
        private bool _isAlive = true;
        private bool _isCommitted;

        public UnitOfWork(SessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Dispose()
        {
            if (!_isAlive)
                return;

            _isAlive = false;

            try
            {
                if (_isCommitted)
                {
                    _transaction.Commit();
                }
            }
            finally
            {
                _transaction.Dispose();
                _session.Dispose();
            }
        }

        public void Commit()
        {
            if (!_isAlive)
                return;

            _isCommitted = true;
        }

        public List<T> Get<T>()
            where T : class
        {
            return _session.Query<T>().ToList();
        }

        public T Get<T>(long id)
            where T : class
        {
            return _session.Get<T>(id);
        }

        public void SaveOrUpdate<T>(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Delete<T>(T entity)
        {
            _session.Delete(entity);
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        public IList<Category> GetP()
        {
            var sql =
                "SELECT c.* FROM  Category c Left Join CategoryLinks cl ON c.CategoryID=cl.childId Where cl.parentId is null ";
            var result = _session.CreateSQLQuery(sql)
                .AddEntity(typeof(Category))
                .List<Category>();


            return  result;
            
            /*
             *var query = "SELECT TOP 10000 o.* "
            + " from ORDERS o where o.Year in (:orderYear));";

var session = sessionFactory.OpenSession();
var result =session.CreateSQLQuery(query)
                .AddEntity(typeof(Order))
                .SetInt32("orderYear",2012)
                .List<Order>();
             * 
             */
        }

//        public ISQLQuery CreateSQLQuery(string q)
//        {
//            return _session.CreateSQLQuery(q);
//        }
    }
}
