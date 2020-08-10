
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using HomeHealth.Web.Interfaces;
using HomeHealth.Web.Data;
using Microsoft.EntityFrameworkCore;


namespace HomeHealth.Web.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly HomeHealthDbContext _context;

        protected readonly DbSet<TEntity> Entities;

        public Repository(HomeHealthDbContext context){
            _context = context;
            Entities = _context.Set<TEntity>();

        }
        
        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate){

            return Entities.Where(predicate);

        }
    }
    
}