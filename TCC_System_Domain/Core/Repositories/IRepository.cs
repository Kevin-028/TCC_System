using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TCC_System_Domain.Core
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(params object[] key);
        TEntity FindByID(params object[] key);
        TEntity FindDetachedByID(params object[] key);
        TEntity FindAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindListAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        System.Data.Common.DbConnection GetDbConnection();
    }
}
