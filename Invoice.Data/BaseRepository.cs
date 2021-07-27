using Invoice.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Data
{
    public class BaseRepository<E> : IRepository<E>
        where E : class
    {
        internal ApplicationDbContext context;
        internal DbSet<E> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<E>();
        }
        public IQueryable<E> Get(
            Expression<Func<E, bool>> filter = null,
            Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null,
             Func<IQueryable<E>, IIncludableQueryable<E, object>> includeProperties = null
            )
        {
            IQueryable<E> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {

                query = includeProperties(query);
            }


            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public IEnumerable<D> GetWithRawSql<D>(string query, object[] parameters, Expression<Func<E, D>> selectFn = null)
        {
            IQueryable<E> queryEntity = dbSet.FromSqlRaw(query, parameters);
            if (selectFn != null)
            {
                return queryEntity.Select(selectFn);
            }

            return (IEnumerable<D>)queryEntity.ToList();
        }

        public void Insert(E entity)
        {
            dbSet.Add(entity);
        }

        public void Update(E entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public Task<int> Commit()
        {
            return context.SaveChangesAsync();
        }
        public void Delete(E entity)
        {
            dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            E e = dbSet.Find(new object[] { id });

            Delete(e);
        }
    }
}
