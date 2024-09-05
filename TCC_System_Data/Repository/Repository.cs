using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using TCC_System_Domain.Core;

namespace TCC_System_Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected TCC_Context Context;
        protected DbSet<TEntity> DbSet;

        public Repository(TCC_Context context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(params object[] key)
        {
            DbSet.Remove(DbSet.Find(key));
        }

        public virtual TEntity FindByID(params object[] key)
        {
            return DbSet.Find(key);
        }

        public TEntity FindDetachedByID(params object[] key)
        {
            var entity = DbSet.Find(key);
            if (entity != null)
                Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public TEntity FindAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).SingleOrDefault();
        }

        public IEnumerable<TEntity> FindListAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        DbConnection IRepository<TEntity>.GetDbConnection()
        {
            return Context.Database.GetDbConnection();
        }
    }
}
