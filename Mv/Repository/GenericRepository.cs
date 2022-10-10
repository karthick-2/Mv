using Mv.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Mv.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal HomeModel home;
        internal DbSet<TEntity> dbset;
        public GenericRepository(HomeModel home)
        {
            this.home = home;
            this.dbset = home.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, string inc = ""
            )
        {
            IQueryable<TEntity> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var i in inc.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(i);
            if (order != null)
            {
                return order(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual TEntity GetById(object obj)
        {
            return dbset.Find(obj);
        }
        public virtual void Insert(TEntity obj)
        {
            dbset.Add(obj);
        }
        public virtual void Update(TEntity obj)
        {
            dbset.Attach(obj);
            home.Entry(obj).State = EntityState.Modified;
        }
        public virtual void Delete(object obj)
        {
            TEntity entity = dbset.Find(obj);

        }
        public virtual void Delete(TEntity obj)
        {
            if (home.Entry(obj).State == EntityState.Detached) dbset.Attach(obj);
            dbset.Remove(obj);
        }
    }
}