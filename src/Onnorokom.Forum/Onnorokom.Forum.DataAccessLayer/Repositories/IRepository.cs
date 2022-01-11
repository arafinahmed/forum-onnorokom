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
        void Add(TEntity entity);
        void Remove(TEntity entityToDelete);
        void Remove(TKey Id);
        void Remove(Expression<Func<TEntity, bool>> expression);
        void Edit(TEntity entityToUpdate);
        int GetCount(Expression<Func<TEntity, bool>> expression = null);
        IList<TEntity> Get(Expression<Func<TEntity, bool>> expression, string includeProperties = "");
        IList<TEntity> GetAll();
        TEntity GetById(TKey Id);
        (IList<TEntity> data, int total, int totalDisplay) Get(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10,
            bool isTrackingOff = false);
        //(IList<TEntity> data, int total, int totalDisplay) GetDynamic(
        //    Expression<Func<TEntity, bool>> expression = null,
        //    string orderBy = null,
        //    string includeProperties = "", int pageIndex = 1, int pageSize = 10,
        //    bool isTrackingOff = false);
        IList<TEntity> Get(Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool isTrackingOff = false);
        //IList<TEntity> GetDynamic(Expression<Func<TEntity, bool>> expression = null,
        //    string orderBy = null,
        //    string includeProperties = "", bool isTrackingOff = false);
    }
}
