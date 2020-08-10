using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace HomeHealth.Web.Interfaces
{
    interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);

        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        
    }
    
}