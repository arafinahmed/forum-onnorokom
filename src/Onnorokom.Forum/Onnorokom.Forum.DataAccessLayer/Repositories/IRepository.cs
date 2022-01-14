using Onnorokom.Forum.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Onnorokom.Forum.DataAccessLayer.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task AddAsync(TEntity entity);
        void Remove(TEntity entityToDelete);
        IList<TEntity> Get(Expression<Func<TEntity, bool>> expression, string includeProperties = "");
        IList<TEntity> GetAll();
        TEntity GetById(TKey Id);
    }
}
