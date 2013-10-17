using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Application.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private Model1Container _datacontext;
        private DbSet<TEntity> _dbset;
        public Repository(Model1Container datacontext)
        {
            this._datacontext = datacontext;
            this._dbset = datacontext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Select()
        {
            IQueryable<TEntity> query = this._dbset;
            return query.ToList();
        }

        public virtual TEntity Select(object id)
        {
            return this._dbset.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            this._dbset.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            this._dbset.Attach(entity);
            this._datacontext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (this._datacontext.Entry(entity).State == EntityState.Detached)
            {
                this._dbset.Attach(entity);
            }
            this._dbset.Remove(entity);
        }
    }
}
