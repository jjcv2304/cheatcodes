using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Categories;
using Microsoft.EntityFrameworkCore.Internal;
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

        public IList<Category> GetParentCategories()
        {
            var sql =
                "SELECT c.* FROM  Category c Where c.parentId is null ";
            var result = _session.CreateSQLQuery(sql)
                .AddEntity(typeof(Category))
                .List<Category>();
            return result;
        }

        public IList<Category> GetCategoriesByParent(int parentId)
        {
            var sql =
                "SELECT c.* FROM  Category c Where c.parentId = :parentId ";
            var result = _session.CreateSQLQuery(sql)
                .AddEntity(typeof(Category))
                .SetInt32("parentId", parentId)
                .List<Category>();
            return result;
        }

        public IList<Category> GetSiblingsOSf(int categoryId)
        {
            var sql =
                "SELECT cc.* " +
                "FROM Category cp " +
                "inner join Category cc on " +
                "(CASE " +
                "WHEN cp.ParentId is null " +
                "THEN cc.ParentId is null " +
                "Else cp.ParentId = cc.ParentId " +
                "END) " +
                "Where cp.CategoryID = :categoryId ";
            
            var result = _session.CreateSQLQuery(sql)
                .AddEntity(typeof(Category))
                .SetInt32("categoryId", categoryId)
                .List<Category>();

            return result;
        }

//        public ISQLQuery CreateSQLQuery(string q)
//        {
//            return _session.CreateSQLQuery(q);
//        }
    }
}