using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Onnorokom.Forum.DataAccessLayer.Entities;

namespace Onnorokom.Forum.DataAccessLayer.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> expression,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            foreach (var property in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
            return query.ToList();
        }

        public virtual IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity GetById(TKey Id)
        {
            return _dbSet.Find(Id);
        }

        public void Remove(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
    }
}