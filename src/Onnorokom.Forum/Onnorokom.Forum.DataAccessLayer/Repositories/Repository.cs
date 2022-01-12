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

        public virtual void Edit(TEntity entityToUpdate)
        {
            if (_dbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);
            }
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
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

        public virtual (IList<TEntity> data, int total, int totalDisplay) Get(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            var total = query.Count();
            var totalDisplay = total;
            if (expression != null)
            {
                query = query.Where(expression);
                totalDisplay = query.Count();
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                var result = orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);
                else
                    return (result.ToList(), total, totalDisplay);
            }
            else
            {
                var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);
                else
                    return (result.ToList(), total, totalDisplay);
            }
        }

        public virtual IList<TEntity> Get(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                var result = orderBy(query);
                if (isTrackingOff)
                {
                    return result.AsNoTracking().ToList();
                }
                else
                {
                    return result.ToList().ToList();
                }
            }
            else
            {
                var result = query;
                if (isTrackingOff)
                {
                    return result.AsNoTracking().ToList();
                }
                else
                {
                    return result.ToList().ToList();
                }
            }
        }

        public virtual IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity GetById(TKey Id)
        {
            return _dbSet.Find(Id);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = _dbSet;
            var count = 0;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            count = query.Count();
            return count;
        }

        //public (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
        //    Expression<Func<TEntity, bool>> expression = null,
        //    string orderBy = null, string includeProperties = "",
        //    int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //    var total = query.Count();
        //    var totalDisplay = query.Count();
        //    if (expression != null)
        //    {
        //        query = query.Where(expression);
        //        totalDisplay = query.Count();
        //    }
        //    foreach (var includeProperty in includeProperties.Split
        //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    if (orderBy != null)
        //    {
        //        var result = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //        if (isTrackingOff)
        //            return (result.AsNoTracking().ToList(), total, totalDisplay);
        //        else
        //            return (result.ToList(), total, totalDisplay);
        //    }
        //    else
        //    {
        //        var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //        if (isTrackingOff)
        //            return (result.AsNoTracking().ToList(), total, totalDisplay);
        //        else
        //            return (result.ToList(), total, totalDisplay);
        //    }
        //}

        //public IList<TEntity> GetDynamic(
        //    Expression<Func<TEntity, bool>> expression = null,
        //    string orderBy = null, string includeProperties = "",
        //    bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //    if (expression != null)
        //    {
        //        query = query.Where(expression);
        //    }
        //    foreach (var includeProperty in includeProperties.Split
        //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    if (orderBy != null)
        //    {
        //        var result = query.OrderBy(orderBy);

        //        if (isTrackingOff)
        //            return result.AsNoTracking().ToList();
        //        else
        //            return result.ToList();
        //    }
        //    else
        //    {
        //        if (isTrackingOff)
        //            return query.AsNoTracking().ToList();
        //        else
        //            return query.ToList();
        //    }

        //}

        public void Remove(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Remove(TKey Id)
        {
            var entityToDelete = _dbSet.Find(Id);
            Remove(entityToDelete);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> expression)
        {
            _dbSet.RemoveRange(_dbSet.Where(expression));
        }
    }
}
